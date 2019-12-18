using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL PROYECTIL CREADO POR EL CLICK IZQUIERDO DE GENJI
/// 
/// HEREDA DE PROYECTIL, FUNCIONA COMO CUALQUIER PROYECTIL, SOLO QUE LA DIRECCIÓN ES A DONDE MIRE LA CÁMARA
/// </summary>

public class GenjiShurikenLeft : Projectile {

    protected override void Start()
    {
        direction = Camera.main.transform.forward;                           //LA DIRECCIÓN INICIAL ES IGUAL A LA DIRECCIÓN DE LA CÁMARA
    }


}
