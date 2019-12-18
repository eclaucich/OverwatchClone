using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL CONTROLADOR DE MOVIMIENTO DE REINHARDT
/// 
/// HEREDA DEL CONTROLADOR DE MOVIMIENTO BASE
/// </summary>

public class ReinhardtMovementController : PlayerMovementController {

    private bool isDashing = false;                                                 //SI ESTA DASHEANDO O NO

    protected override void Update()
    {
        if (!isDashing)                                                             //SI NO ESTA DASHEANDO, ACTUALIZAR NORMAL
        {
            base.Update();
        }
        else                                                                        //SI ESTA DASHEANDO, SOLAMENTE MOVER EL PERSONAJE (NO SE PUEDE HACER NADA MIENTRAS SE DASHEA)
        {
            characterController.Move(currentImpact * Time.deltaTime);
        }
    }

    //FUCNIONES AUXILIARES SOBREESCRITAS
    public override void SetDashing(bool aux)                                       //SETEAR SI ESTA DASHEANDO O NO
    {
        isDashing = aux;
    }

    public override bool IsDashing()                                                //SABER SI ESTA DASHEANDO O NO
    {
        return isDashing;
    }

}
