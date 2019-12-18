using UnityEngine;

/// <summary>
/// CONTROLADOR DEL MOVIMIENTO DE GENJI
/// 
/// HEREDA DE LA BASE DEL MOVIMIENTO DE LOS PERSONAJES
/// </summary>
/// 
public class GenjiMovementController : PlayerMovementController {

    private bool isReflecting = false;
    private bool isDashing = false;
    private bool isClimbing = false;                        //AUXILIAR PARA SABER SE ESTA TREPANDO O NO

    private int totalJumps = 2;                             //SALTOS MÁXIMOS DEL PERSONAJE
    private int jumpRemainings = 2;                         //SALTOS QUE ACTUALMENTE LE QUEDAN AL PERSONAJE

    protected override void Update()                        //SE SOBREESCRIBE EL UPDATE PARA CONTROLAR EL TREPAR DE GENJI
    {
        if (!isClimbing)                                    //SI NO ESTA TREPANDO, SE MUEVE NORMAL
        {
            base.Update();
        }

        if(characterController.isGrounded)                  //SI EL PERSONAJE TOCA EL SUELO, SE LE RESETEAN LOS SALTOS RESTANTES
        {
            jumpRemainings = totalJumps;
        }
    }

    protected override void Jump()                          //EL SALTO DE GENJI ES DIFERENTE, ENTONCES SE SOBREESCRIBE
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isClimbing)                                 //SI ESTA TREPANDO, SALIR DE LA FUNCIÓN, NO SE PUEDE SALTAR MIENTRAS SE TREPA
            {
                return;
            }

            if (jumpRemainings > 0)                         //SI LE QUEDAN SALTOS PARA USAR, SALTA
            {
                ResetImpactY();                             //SE RESETEA LA VELOCIDAD EN EL EJE Y, ENTONCES TODOS LOS SALTOS SON SIEMPRE IGUALES, SIN IMPORTAR QUE ESTES CAYENDO O NO
                AddForce(Vector3.up, jumpForce);            //INDUCIR FUERZA HACIA ARRIBA PARA QUE SALTE
            }

            //LÓGICA PARA RESTAR LOS SALTOS QUE LE QUEDAN
            if (jumpRemainings == 2)                        //SI LE QUEDAN LOS DOS SALTOS, HAY QUE REVISAR SI PARTIÓ DEL SUELO, O DEL AIRE
            {
                if (characterController.isGrounded)         //SI PARTIÓ DESDE EL SUELO -> LE QUEDA UN SALTO MÁS
                {
                    jumpRemainings = 1;
                }
                else                                        //SI PARTIÓ DESDE EL AIRE -> NO PUEDE SALTAR MÁS
                {
                    jumpRemainings = 0;
                }
            }
            else if(jumpRemainings == 1)                    //SI LE QUEDA UN SALTO, NO IMPORTA DONDE ESTA, SE LO GASTA 
            {
                jumpRemainings = 0;
            }
        }
    }

    public override void SetClimbing(bool aux)              //COMO GENJI TREPA, SE SOBREESCRIBE ESTA FUNCIÓN
    {
        isClimbing = aux;
    }

    public override void SetDashing(bool aux)
    {
        isDashing = aux;
    }

    public override bool IsDashing()
    {
        return isDashing;
    }

    public void SetReflecting(bool aux)
    {
        isReflecting = aux;
    }

    public bool IsReflecting()
    {
        return isReflecting;
    }
}
