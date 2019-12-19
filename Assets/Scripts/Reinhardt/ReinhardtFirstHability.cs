using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DE LA PRIMERA HABILIDAD DE REINHARDT
/// 
/// SE DISPARA UN PROYECTIL A DONDE SE ESTÉ MIRANDO
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class ReinhardtFirstHability : Ability {

    [SerializeField] private float castTime = 2f;                                                       //TIEMPO DE CASTEO

    [SerializeField] private GameObject proyectilePrefab;                                               //PREFAB DEL PROYECTIL

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !playerMovementController.IsDashing())                       //SI SE APRIETA EL TRIGGER, Y NO SE ESTÁ DASHEANDO, CASTEAR
        {
            StartCoroutine(Cast());
        }
    }

    protected override IEnumerator Cast()
    {
        yield return new WaitForSeconds(castTime);                                                      //SE ESPERA EL TIEMPO DE CASTEO
                                                                                                        //SE CREA EL PROYECTIL CON LA POSICIÓN Y ROTACIÓN DEL PUNTO DE DISPARO DEL PERSONAJE
        Instantiate(proyectilePrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);
    }
}
