using System.Collections;
using UnityEngine;

public class TracerDash : Ability{

    [SerializeField] private int numberOfDashes;
    private int dashesLeft;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    private float currentCooldown = 0f;

    protected override void Awake()
    {
        base.Awake();
        dashesLeft = numberOfDashes;
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dashesLeft > 0 && !isOnCooldown)
        {
            StartCoroutine(Cast());
        }
        
        if(dashesLeft < numberOfDashes)
        {
            currentCooldown += Time.deltaTime;
            if(currentCooldown >= cooldown)
            {
                dashesLeft++;
                currentCooldown = 0f;
            }
        }
    }

    protected override IEnumerator Cast()
    {
        Vector3 directionInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        directionInput = transform.TransformDirection(directionInput);

        if(directionInput == Vector3.zero)
        {
            directionInput = transform.forward;
        }

        playerMovementController.AddForce(directionInput, dashForce);

        yield return new WaitForSeconds(dashDuration);

        dashesLeft--;

        playerMovementController.ResetImpact();
    }

}