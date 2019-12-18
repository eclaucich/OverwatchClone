using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerRecall : Ability {

    private bool canRecallData = true;

    [SerializeField] private float timeBtwData;
    private float currentTimeBtwData = 0f;
    [SerializeField] private int numberOfPoints;

    [SerializeField] private float recallDuration;

    private struct RecallData
    {
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Quaternion cameraRotation;
    }

    List<RecallData> recallData;
    
    private void Start()
    {
        recallData = new List<RecallData>();
    }
    
    protected override void Update()
    {
        base.Update();

        currentTimeBtwData += Time.deltaTime;

        if (canRecallData)
        {
            if(currentTimeBtwData > timeBtwData)
            {
                if(recallData.Count >= numberOfPoints)
                {
                    recallData.RemoveAt(0);
                }

                recallData.Add(GetRecallData());
                currentTimeBtwData = 0f;
            }
        }

        for (int i = 0; i < recallData.Count-1; i++)
        {
            Debug.DrawLine(recallData[i].playerPosition, recallData[i + 1].playerPosition);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Cast());
        }

    }

    private RecallData GetRecallData()
    {
        return new RecallData
        {
            playerPosition = playerMovementController.transform.position,
            playerRotation = playerMovementController.transform.rotation,
            cameraRotation = playerCameraController.transform.rotation,
        };
    }


    protected override IEnumerator Cast()
    {

        canRecallData = false;

        playerCameraController.Lock(true);

        float timeForEachData = recallDuration / numberOfPoints;

        while(recallData.Count > 0)
        {
            float t = 0f;

            while (t < timeForEachData)
            {
                transform.position = Vector3.Lerp(transform.position,
                    recallData[recallData.Count - 1].playerPosition,
                    t / timeForEachData);

                transform.rotation = Quaternion.Lerp(transform.rotation,
                    recallData[recallData.Count - 1].playerRotation,
                    t / timeForEachData);

                playerCameraController.transform.rotation = Quaternion.Lerp(playerCameraController.transform.rotation,
                    recallData[recallData.Count - 1].cameraRotation,
                    t / timeForEachData);

                t += Time.deltaTime;

                yield return null;
            }
            
            recallData.RemoveAt(recallData.Count - 1);
        }

        canRecallData = true;

        playerCameraController.Lock(false);
    }
}
