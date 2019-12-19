using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DE UN PROYECTIL BASE
/// 
/// TIENE UNA VELOCIDAD DE MOVIMIENTO, UN TIEMPO MÁXIMO DE VIDA, Y UNA DIRECCIÓN DE MOVIMIENTO
/// 
/// EL START SETEA LA DIRECCIÓN DE MOVIMIENTO
/// 
/// EL UPDATE MUEVE EL PROYECTIL EN LA DIRECCIÓN SETEADA, Y SI SUPERA EL TIEMPO MÁXIMO DE VIDA, SE DESTRUYE
/// 
/// COMO MÍNIMO TODO PROYECTIL HACE ESTO
/// </summary>

public class Projectile : MonoBehaviour {

	[SerializeField] protected float speed;                                         //VELOCIDAD DE MOVIMIENTO
    [SerializeField] protected float maxAliveTime;                                  //TIEMPO MÁXIMO DE VIDA DEL PROYECTIL
    private float currentAliveTime;                                                 //TIEMPO ACTUAL DE VIDA DEL PROYECTIL
    protected Vector3 direction;                                                    //DIRECCIÓN DE MOVIMIENTO DEL PROYECTIL

    protected virtual void Start()
    {
        direction = transform.forward;                                              //AL CREARSE LA DIRECCIÓN SERÁ HACIA ADELANTE
    }

    protected virtual void Update()
    {
        Move();
        UpdateAlive();
    }

    protected virtual void Move()
    {
        transform.position += direction * speed * Time.deltaTime;                   //SE MUEVE EL PROYECTIL SEGÚN LA DIRECCIÓN DETERMINADA Y LA VELOCIDAD
    }

    protected virtual void UpdateAlive()
    {
        currentAliveTime += Time.deltaTime;                                          //ACTUALIZA EL TIEMPO ACTUAL DE VIDA DEL PROYECTIL

        if (currentAliveTime > maxAliveTime)                                         //SI SE SUPERA EL TIEMPO MÁXIMO DE VIDA, SE DESTRUYE
        {
            Destroy(gameObject);
        }
    }
}
