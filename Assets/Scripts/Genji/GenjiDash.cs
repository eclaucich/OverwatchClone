using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL DASH DE GENJI
/// 
/// NECESITA DE UN PLAYER MOVEMENT CONTROLLER PARA USARSE
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class GenjiDash : Ability {

    [SerializeField] private float dashForce = 20f;                                         //FUERZA DEL DASH
    [SerializeField] private float dashDuration = 2f;                                       //DURACIÓN DEL DASH

    private GenjiMovementController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<GenjiMovementController>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q) && !isOnCooldown && !playerController.IsReflecting())                                                    //SI SE APRIETA EL TRIGGER, SE CASTEA
        {
            StartCoroutine(Cast());
            playerController.SetDashing(true);
        }
    }

    protected override IEnumerator Cast()
    {
        SetOnCooldown(true);
        playerMovementController.AddForce(Camera.main.transform.forward, dashForce);        //SE INDUCE UNA FUERZA HACIA DONDE SE ESTÉ MIRANDO EN ESE MOMENTO

        yield return new WaitForSeconds(dashDuration);                                      //SE ESPERAN TANTOS SEGUNDOS COMO LA DURACIÑON DEL DASH

        playerMovementController.ResetImpact();                                             //TERMINÓ EL DASH, SE RESETEA EL IMPACTO DEL PERSONAJE
        playerController.SetDashing(false);
    }
}
