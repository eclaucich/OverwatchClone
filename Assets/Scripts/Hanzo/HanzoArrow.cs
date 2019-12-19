using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DE LAS FLECHAS DE HANZO
/// </summary>

public class HanzoArrow : Projectile {

    [SerializeField] private float bulletDrop = 0.1f;                               //BULLET DROP DE LAS FLECHAS
    [SerializeField] private float speedRate = 10f;                                 //VARIABLE PARA CALCULAR LA VELOCIDAD DEL PROYECTIL

    protected override void Move()
    { 
        transform.position += transform.forward * speed * Time.deltaTime;           //SE MUEVE EL PROYECTIL HACIA ADELANTE
        transform.Rotate(Vector3.right, bulletDrop);                                //SE APLICA EL BULLET DROP
    }

    public void SetSpeed(float holdingFire)                                         //SE SETEA LA VELOCIDAD DEL PROYECTIL, SEGÚN CUANTO SE MANTUVO APRETADO EL BOTÓN DE DISPARO
    {
        speed = (1 + holdingFire) * speedRate;
    }
}
