using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL CLICK IZQUIERDO DE GENJI
/// 
/// SE DISPARAN TRES PROYECTILES UNO ATRAS DE OTRO
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class GenjiLeftClick : Ability {

    private int numberProyectile = 3;                                               //CANTIDAD DE PROYECTILES DE LA HABILIDAD
    private float timeBtwProjectiles = 0.1f;                                        //TIEMPO ENTRE CADA PROYECTIL

    [SerializeField] private GameObject proyectilePrefab;                           //PREFAB DEL PROYECTIL

    private GenjiMovementController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<GenjiMovementController>();
    }

    protected override void Update()
    {
        if (playerController.IsDashing() || playerMovementController.IsClimbing() || playerController.IsReflecting())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))                                            //SI SE APRIETA EL TRIGGER, SE CASTEA
        {
            StartCoroutine(Cast());
        }
    }

    protected override IEnumerator Cast()
    {
        for (int i = 0; i < numberProyectile; i++)                                  //SE CREAN LOS PROYECTILES SEGÚN LA CANTIDAD SETEADA
        {
            Instantiate(proyectilePrefab, playerMovementController.firePointLeft.transform.position, playerMovementController.firePointLeft.transform.rotation);    //SE CREA EL PROYECTIL, CON LA POSICIÓN Y ROTACIÓN DEL PUNTO DE DISPARO DEL PERSONAJE

            yield return new WaitForSeconds(timeBtwProjectiles);                                  //ESPERAR TANTO SEGUNDOS COMO TIEMPO ENTRE CADA PROYECTIL, PARA QUE NO SALGAN TODOS ENCIMADOS
        }
    }
}
