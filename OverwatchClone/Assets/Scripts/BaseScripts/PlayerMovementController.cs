using UnityEngine;

/// <summary>
/// CONTROL BASE DEL MOVIMIENTO DE LOS PERSONAJES
/// 
/// TODOS LOS PERSONAJES COMO MÍNIMO SE MUEVEN Y SALTAN
/// </summary>

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] protected float movementVelocity;              //VELOCIDAD INICIAL DEL PERSONAJE
    protected float initialMovementVelocity;                        //VELOCIDAD ACTUAL DEL PERSONAJE (ALGUNA INTERACCIÓN PUEDE CAMBIAR LA VELOCIDAD DEL PERSONAJE)
    [SerializeField] protected float jumpForce;                     //FUERZA DEL SALTO

    [SerializeField] protected float mass = 1f;                     //MASA DEL PERSONAJE
    [SerializeField] private float initialDamping = 2f;             //AMORTIGUACIÓN INICIAL DEL PERSONAJE
    private float currentDamping;                                   //AMORTIGUACIÓN ACTUAL DEL PERSONAJE (ALGUNA INTERACCIÓN PUEDE CAMBIARLA)

    private float velocityY;                                        //VELOCIDAD EN EL EJE Y, PARA CONTROLAR CAÍDA LIBRE

    protected Vector3 currentImpact;                                //VECTOR DE UNA FUERZA QUE AFECTA AL PERSONAJE

    private readonly float gravity = Physics.gravity.y;             //GRAVEDAD

    public GameObject firePointRight;                               //GAMEOBJECT QUE INDICA EL PUNTO DE DISPARO DEL PERSONAJE, DE DONDE SALEN PROYECTILES, ETC
    public GameObject firePointLeft;

    protected CharacterController characterController;              //CHARACTER CONTROLLER

    protected virtual void Awake()                                  //SOBREESCRIBIR SI HAY QUE INICIALIZAR ALGUNA OTRA VARIABLE
    {
        characterController = GetComponent<CharacterController>();
        initialMovementVelocity = movementVelocity;
        currentDamping = initialDamping;
    }

    protected virtual void Update()                                 //SOBREESCRIBIR SI SE QUIERE AFECTAR CUANDO MOVERSE Y SALTAR
    {
        Move();
        Jump();
    }

    protected virtual void Move()                                   //MOVIMIENTO DEL PERSONAJE, SOBREESCRIBIR PARA NUEVO MOVIMIENTO
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;       //OBTENER INPUT DEL TECLADO

        direction = transform.TransformDirection(direction);                                                                //TRANSFORMAR DIRECCIÓN RELATIVO A AL MUNDO

        if (characterController.isGrounded && velocityY < 0f)                                                               //CLAMP VELOCIDAD EN EJE Y EN CERO, NO PUEDE SER NEGATIVA
        {
            velocityY = 0f;
        }

        velocityY += gravity * Time.deltaTime;                                                                              //LA VELOCIDAD EN "Y" CAMBIA CON LA GRAVEDAD 

        Vector3 velocity = direction * movementVelocity + Vector3.up * velocityY;                                           //LA VELOCIDAD DEPENDE DE LA DIRECCIÓN, DE LA VELOCIDAD DEL PERSONAJE, Y DE LA VELOCIDAD EN EJE Y

        if (currentImpact.magnitude > 0.2f)                                                                                 //SI EL IMPACTO TIENE UNA MAGNITUD SIGNIFICANTE, SE TIENE EN CUENTA, SI NO, SE IGNORA (EL 0.2f ES RELATIVO, SE PUEDE HACER QUE DEPENDE EL PERSONAJE ESTE VALOR SEA DISTINTO)
        {
            velocity += currentImpact;
        }

        characterController.Move(velocity * Time.deltaTime);                                                                //MOVER EL PERSONAJE CON LA VELOCIDAD CALCULADA

        currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, currentDamping * Time.deltaTime);                         //EL IMPACTO SE VA DISMINUYENDO DE A POCO, HASTA CERO
    }

    protected virtual void Jump()                                   //SALTO DEL PERSONAJE, SOBREESCRIBIR PARA NUEVO SALTO
    {
        if (Input.GetKeyDown(KeyCode.Space))                        //PARA TODOS LOS PERSONAJES, SE SALTA CON ESPACIO (SE PUEDE CAMBIAR SOBREESCRIBIENDO)
        {
            if (characterController.isGrounded)                     //SI SE APRIETA EL TRIGGER, Y EL PERSONAJE ESTA EN EL PISO, SE SALTA
            {
                AddForce(Vector3.up, jumpForce);                    //APLICAR FUERZA HACIA ARRIBA
            }
        }
    }

    public void AddForce(Vector3 direction, float magnitude)        //FUNCIÓN PARA AGREGAR UNA FUERZA DE IMPACTO, SE NECESITA UNA DIRECCIÓN Y UNA MAGNITUDD
    {
        currentImpact += direction.normalized * magnitude / mass;   //SE MODIFICA EL IMPACTO ACTUAL CON LA DIRECCIÓN NORMALIZADA (SOLO IMPORTA LA DIRECCIÓN), LA MAGNITUD DE LA FUERZA,Y LA MASA DEL PERSONAJE (A MAYOR MASA, MAS DIFICIL ES MOVERLO) 
    }

    public void ResetImpact()                                       //RESETEA EL IMPACTO ACTUAL, Y LA AMORTIGUACIÓN
    {
        currentImpact = Vector3.zero;
        currentDamping = initialDamping;
    }

    public void ResetImpactY()                                      //RESETEA LA VELOCIDAD EN EL EJE Y
    {
        velocityY = 0f;
    }

    public void Move(Vector3 direction)                             //FUNCIÓN PARA PODER LLAMAR A LA FUNCIÓN "MOVE" DE CHARACTER CONTROLLER DESDE OTRA CLASE
    {
        characterController.Move(direction);
    }

    //FUNCIONES AUXILIARES PARA MODIFICAR VARIABLES

    public void SetDamping(float newDamping) {                      //SETEAR LA AMORTIGUACIÓN
        currentDamping = newDamping;
    }

    public virtual void SetClimbing(bool aux) { }                   //SI EL PERSONAJE TREPA, SOBREESCRIBE ESTAS FUNCIONES
    public virtual bool IsClimbing() { return false; }

    public virtual void SetAiming(bool aux) { }                     //SI EL PERSONAJE PUEDE APUNTAR, SOBREESCRIBE ESTA FUNCIÓN

    public virtual void SetDashing(bool aux) { }                    //SI EL PERSONAJE PUEDE DASHEAR, SOBREESCRIBE ESTAS FUNCIONES
    public virtual bool IsDashing() { return false; }
}
