using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL CLICK DERECHO DE GENJI
/// 
/// SE DISPARÁN TRES PROYECTILES AL MISMO TIEMPO CON DIFERENTES ÁNGULOS
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class GenjiRightClick : Ability {

    [SerializeField] private float angleBtwProjectiles;                             //ÁNGULO ENTRE LOS PROYECTILES

    [SerializeField] private GameObject proyectilePrefab;                           //PREFAB DEL PROYECTIL

    private GenjiMovementController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<GenjiMovementController>();
    }

    protected override void Update()
    {
        if(playerController.IsDashing() || playerMovementController.IsClimbing() || playerController.IsReflecting())
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))                                            //SI SE APRIETA EL TRIGGER, CASTEAR
        {
            StartCoroutine(Cast());
        }
    }

    protected override IEnumerator Cast()                                           //SE CREAN LOS PROYECTILES CON LA POSICIÓN Y ROTACIÓN DEL PUNTO DE DISPARO DEL PERSONAJE, DESPUÉS SE ROTA SI ES NECESARIO
    {
        //PROYECCTIL DE LA DERECHA
        GameObject proyectileRight = Instantiate(proyectilePrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);
        proyectileRight.transform.Rotate(Vector3.up, angleBtwProjectiles);

        //PROYECTIL DEL MEDIO
        Instantiate(proyectilePrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);

        //PROYECTIL DE LA IZQUIERDA
        GameObject proyectileLeft = Instantiate(proyectilePrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);
        proyectileLeft.transform.Rotate(Vector3.up, -angleBtwProjectiles);

        yield return null;
    }
}
