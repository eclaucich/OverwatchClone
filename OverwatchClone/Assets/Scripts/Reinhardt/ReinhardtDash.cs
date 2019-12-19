using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL DASH DE REINHARDT
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class ReinhardtDash : Ability {

    [SerializeField] private float castTime;                                                            //TIEMPO DE CASTEO DE LA HABILIDAD
    [SerializeField] private float dashForce;                                                           //FUERZA DEL DASH
    [SerializeField] private float dashDuration;                                                        //DURACIÓN DEL DASH
    private float collisionDistance = 1f;
    private Pinable objectPin = null;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !playerMovementController.IsDashing())                       //SI SE APRIETA EL TRIGGER, Y NO SE ESTÁ EJECUTANDO LA HABILIDAD
        {
            StartCoroutine(Cast());
        }

        if(playerMovementController.IsDashing())
        {
            CheckForCollision();
        }
    }

    protected override IEnumerator Cast()
    {
        yield return new WaitForSeconds(castTime);                                                      //ESPERAR EL TIEMPO DE CASTEO

        playerMovementController.SetDamping(0);                                                         //SE QUITA LA AMORTIGUACIÓN DEL PERSONAJE, PARA QUE LA FUERZA DEL DASH Y LA DURACIÓN TENGAN MAYOR RELACIÓN

        playerMovementController.SetDashing(true);                                                      //SE SETEA TRUE QUE EL PERSONAJE ESTA DASHEANDO

        playerMovementController.AddForce(playerMovementController.transform.forward, dashForce);       //SE INDUCE LA FUERZA DEL DASH HACIA ADELANTE

        yield return new WaitForSeconds(dashDuration);                                                  //LA FUERZA SE APLICA TANTO TIEMPO COMO DURE LA HABILIDAD

        StopDash();
    }

    private void StopDash()
    {
        playerMovementController.ResetImpact();                                                         //CUANDO TERMINA EL DASH SE RESETA LA FUERZA DE IMPACTO
        playerMovementController.SetDashing(false);                                                     //SE SETEA FALSE EL DASH DEL PERSONAJE
        if(objectPin!=null)
        {
            objectPin.StopPin();
            objectPin = null;
        }
    }

    private bool CheckForCollision()
    {
        RaycastHit hit;        

        if (Physics.Raycast(transform.position, transform.forward, out hit, collisionDistance))     //SI SE COLISIONA CON UN OBJETO
        {
            if(hit.collider.GetComponent<Obstacle>() != null)
            {
                Debug.Log("PARED!");
                StopDash();
                return true;
            }

            if (objectPin==null && hit.collider.GetComponent<Pinable>() != null)                                     //SI EL OBJETO ES TREPABLE, SE CASTEA LA HABILIDAD
            {
                //hit.GetComponent<Pinable>().Pin();
                objectPin = hit.collider.GetComponent<Pinable>();
                objectPin.StartPin(this.transform);
                Debug.Log("PIN");
            }
                
        }

        return false;
    }
}
