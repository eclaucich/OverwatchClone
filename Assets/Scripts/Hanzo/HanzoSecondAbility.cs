using System.Collections;
using UnityEngine;

public class HanzoSecondAbility : Ability {

    private HanzoMovementController playerController;

    protected override void Awake()
    {
        playerController = GetComponent<HanzoMovementController>();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.E) && !isOnCooldown)
        {
            StartCoroutine(Cast());
        }

        if (isOnCooldown)
        {
            playerController.SetVisionArrow(false);
        }
    }

    protected override IEnumerator Cast()
    {
        playerController.SetVisionArrow(true);

        yield return null;
    }

}
