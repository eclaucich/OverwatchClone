using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL TREPAR DE GENJI
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class GenjiClimb : Ability {

    [SerializeField] private float climbingForce = 10f;                 //FUERZA DEL TREPAR
    [SerializeField] private float climbingEdgeForce = 10f;             //FUERZA ADICIONAL PARA CUANDO SE LLEGA A UN BORDE, PARA QUE TE DISPARE HACIA ARRIBA
    [SerializeField] private float climbingTime = 2f;                   //TIEMPO MÁXIMO PARA TREPAR
    private float climbableDistance = 1f;                               //DISTANCIA MÍNIMA A UN OBJECTO TREPABLE PARA PODER TREPAR
    private RaycastHit hit;                                             //RAYCAST QUE TENDRÁ LA INFORMACIÓN DEL OBJETO COLISIONADO

    protected override void Update()                                               
    {
        if (Input.GetKeyDown(KeyCode.Space))                                                            //SI SE APRIETA ESPACIO, SE ANALIZA SI SE EJECUTA LA HABILIDAD
        {                                                        
            if (Physics.Raycast(transform.position, transform.forward, out hit, climbableDistance))     //SI SE COLISIONÓ CON UN OBJETO EL RAYCAST ES VERDADERO
            {
                if (hit.collider.GetComponent<Climbable>() != null)                                     //SI EL OBJETO CON EL QUE SE COLISIONÓ, ES TREPABLE, SE EJECUTA LA HABILIDAD
                {
                    StartCoroutine(Cast());
                }
            }
        }
    }

    protected override IEnumerator Cast()                                                                   
    {
        playerMovementController.SetClimbing(true);                                                         //COMIENZA A TREPAR ENTONCES SE SETEA TRUE EL TREPAR
        float timeClimbing = 0f;                                                                            //TIEMPO ACTUAL TREPANDO COMIENZA EN CERO

        while (Input.GetKey(KeyCode.Space) && timeClimbing < climbingTime)                                  //MIENTRAS SE MANTENGA APRETADO ESPACIO Y QUEDE TIEMPO PARA TREPAR, SEGUIR TREPANDO
        {
            RaycastHit newHit;                                                                              //RAYCAST CON INFORMACIÓN DE UNA NUEVA COLISIÓN
            if (Physics.Raycast(transform.position, transform.forward, out newHit, climbableDistance))      //SI SE COLISIONÓ CON ALGO
            {
                if (hit.collider == newHit.collider)                                                        //SI SEGUIMOS TREPANDO EN EL MISMO OBJETO TREPABLE, SE SIGUE TREPANDO
                {
                    playerMovementController.Move(new Vector3(0f, climbingForce * Time.deltaTime, 0f));     //SE MUEVE EL PERSONAJE HACIA ARRIBA
                    timeClimbing += Time.deltaTime;                                                         //SE INCREMENTA EL TIEMPO DE TREPAR
                    yield return null;                                                                      //SE SALE DE LA FUNCIÓN
                }
                else                                                                                        //SI EL OBJETO COLISONADO NO ES EL MISMO CON EL QUE SE COMENZÓ A TREPAR, SE DEJA DE TREPAR
                {
                    break;
                }
            }
            else                                                                                            //SI NO SE COLISIONÓ CON NADA, NO TREPAR, SALIR DEL WHILE
            {
                break;
            }

        }

        playerMovementController.ResetImpactY();                                                            //SE RESETEA LA VELOCIDAD EN EL EJE Y
        playerMovementController.AddForce(Vector3.up, climbingEdgeForce);                                   //SE LE DA UNA FUERZA ADICIONAL PARA TREPAR LOS BORDES DE OBJETOS, O CUANDO SE TERMINA DE TREPAR PARA QUE QUEDE MÁS FLUIDO
          
        playerMovementController.SetClimbing(false);                                                        //SE DEJA DE TREPAR, SETEAR FALSE EL TREPAR DEL PERSONAJE
    }
}
