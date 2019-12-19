using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FUNCIONAMIENTO DEL CONTROLADOR DE MOVIMIENTO DE HANZO
/// 
/// HEREDA DEL CONTROLADOR BASE DEL MOVIMIENTO
/// </summary>

public class HanzoMovementController : PlayerMovementController {

    private bool isVisionArrow = false;
    private bool isRapidFire = false;

    private bool isAimining = false;                                            //SI ESTA APUNTANDO O NO
    private bool isClimbing = false;                                            //SI ESTA TREPANDO O NO

    [SerializeField] private float dashForce = 5f;                              //FUERZA DEL DASH
    [SerializeField] private float dashCooldown;
    private float currentDashCooldown;
    private bool isOnCooldownDash;
    [SerializeField] private Image dashUI;


    protected override void Update()
    {
        
        if (isAimining)                                                         //MIENTRAS ESTÉ APUNTANDO, SE MUEVE MÁS LENTO
        {
            movementVelocity = initialMovementVelocity / 2f;
        }
        else                                                                    //SI DEJA DE APUNTAR, SE MUEVE A VELOCIDAD NORMAL
        {
            movementVelocity = initialMovementVelocity;
        }

        if (!isClimbing)                                                        //SI NO ESTA TREPANDO, SE MUEVE NORMAL
        {
            base.Update();
        }

        if (isOnCooldownDash)
        {
            currentDashCooldown += Time.deltaTime;
            if(currentDashCooldown > dashCooldown)
            {
                isOnCooldownDash = false;
                currentDashCooldown = 0f;
                dashUI.color = new Color(255f, 255f, 255f);
            }
        }
    }

    protected override void Jump()                                              //FUNCIONAMIENTO DEL SALTO DE HANZO
    {
        if (Input.GetKeyDown(KeyCode.Space))                                    //SI SE APRIETA EL TRIGGER, SE VERIFICA QUÉ SE HACE
        {
            if (characterController.isGrounded)                                 //SI ESTÁ EN EL PISO, SE SALTA
            {
                ResetImpactY();
                AddForce(Vector3.up, jumpForce);
            }
            else                                                               //SI ESTÁ EN EL AIRE, USA LA HABILIDAD DEL DASH
            {
                if (!isOnCooldownDash)
                {
                    Dash();
                }
            }
        }
    }

    private void Dash()                                                         //FUNCIONAMIENTO DEL DASH DE HANZO
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));      //SE OBTIENE EL INPUT DEL TECLADO, ESTO DETERMINADO LA DIRECCIÓN DEL DASH
        direction = transform.TransformDirection(direction);                                                    //SE TRANSFORMA LA DIRECCIÓN RELATIVA AL MUNDO

        if(direction == Vector3.zero)                                                                           //SI NO SE APRETÓ NADA, POR DEFAULT SE DASHEA PARA ADELANTE
        {
            direction = characterController.transform.forward;
        }

        AddForce(direction, dashForce);                                                                         //SE INDUCE LA FUERZA DEL DASH

        isOnCooldownDash = true;
        dashUI.color = new Color(0f, 0f, 0f);
    }


    public void SetVisionArrow(bool aux)
    {
        isVisionArrow = aux;
    }

    public bool IsVisionArrow()
    {
        return isVisionArrow;
    }

    public void SetRapidFire(bool aux)
    {
        isRapidFire = aux;
    }

    public bool IsRapidFire()
    {
        return isRapidFire;
    }

    //FUNCIONES AUXILIARES SOBREESCRITAS
    public override void SetClimbing(bool aux)                                  //SETEAR SI ESTA TREPANDO O NO
    {
        isClimbing = aux;
    }

    public override bool IsClimbing()                                           //VER SI ESTA TREPANDO O NO
    {
        return isClimbing;
    }

    public override void SetAiming(bool aux)                                    //SETEAR SI ESTA APUNTANDO O NO
    {
        isAimining = aux;
    }

}
