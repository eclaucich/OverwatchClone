using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL TREPAR DE HANZO
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class HanzoClimb : Ability {

    [SerializeField] private float climbingForce = 10f;                                                 //FUERZA DEL TREPAR
    [SerializeField] private float climbingEdgeForce = 10f;                                             //FUERZA ADICIONAL PARA CUANDO SE LLEGA A UN BORDE
    [SerializeField] private float climbingTime = 2f;                                                   //TIEMPO MÁXIMO PARA TREPAR
    private float climbableDistance = 1f;                                                               //DISTANCIA MÍNIMA A UN OBJECTO TREPABLE PARA TREPARLO

    RaycastHit hit;                                                                                     //TIENE LA INFORMACIÓN DEL OBJETO COLISIONADO

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))                                                            //SI SE APRIETA EL TRIGGER, SE ANALIZA SI SE CASTEA
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, climbableDistance))     //SI SE COLISIONA CON UN OBJETO
            {
                if (hit.collider.GetComponent<Climbable>() != null)                                     //SI EL OBJETO ES TREPABLE, SE CASTEA LA HABILIDAD
                {
                    StartCoroutine(Cast());
                }
            }
        }
    }

    protected override IEnumerator Cast()
    {
        playerMovementController.SetClimbing(true);
        float timeClimbing = 0f;

        while (Input.GetKey(KeyCode.Space) && timeClimbing < climbingTime)
        {
            RaycastHit newHit;
            if (Physics.Raycast(transform.position, transform.forward, out newHit, climbableDistance))
            {
                if (hit.collider == newHit.collider)
                {
                    playerMovementController.Move(new Vector3(0f, climbingForce * Time.deltaTime, 0f));
                    timeClimbing += Time.deltaTime;
                    yield return null;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }

        playerMovementController.ResetImpactY();
        playerMovementController.AddForce(Vector3.up, climbingEdgeForce);

        playerMovementController.SetClimbing(false);
    }
    
}
