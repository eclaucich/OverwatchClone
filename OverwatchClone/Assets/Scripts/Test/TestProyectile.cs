using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProyectile : MonoBehaviour {

    [SerializeField] private float velocity = 40f;
    [SerializeField] private float maxAliveTime = 2f;
    private float timeAlived;

    private Vector3 direction;

    private void Awake()
    {
        timeAlived = 0f;
        direction = transform.forward;
    }

    private void Update()
    {
        transform.position += direction * velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.2f))
        {
            if(hit.collider.GetComponent<Reflective>() != null)
            {
                direction = Camera.main.transform.forward;
            }
            else if(hit.collider.GetComponent<Shield>() != null)
            {
                Destroy(gameObject);
            }
        }

        timeAlived += Time.deltaTime;

        if(timeAlived > maxAliveTime)
        {
            Destroy(gameObject);
        }
    }
}
