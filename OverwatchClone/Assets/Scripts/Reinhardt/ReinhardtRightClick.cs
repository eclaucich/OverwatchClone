using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL CLICK DERECHO DE REINHARDT
/// 
/// SE CREA UN ESCUDO
/// 
/// SIEMPRE ESTA CREADO EL ESCUDO, CUANDO SE CASTEA SE HACE ACTIVA EL OBJECTO, CUANDO SE DEJA DE CASTEAR, SE DESACTIVA
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class ReinhardtRightClick : Ability {

    [SerializeField] private GameObject shieldPrefab;                   //PREFAB DEL ESCUDO

    private void Update()
    {
        if (Input.GetMouseButton(1))                                    //SI SE APRIETA EL TRIGGER, SE CASTEA
        {
            StartCoroutine(Cast());
        }

        if (Input.GetMouseButtonUp(1))                                  //SE SE SUELTA EL TRIGGER, SE DEJA DE CASTEAR
        {
            shieldPrefab.SetActive(false);
        }
    }

    protected override IEnumerator Cast()
    {
        if (!shieldPrefab.activeSelf)                                   //SI EL ESCUDO NO ESTABA ACTIVADO
        {
            shieldPrefab.SetActive(true);                               //ACTIVARLO
        }

        yield return null;
    }
}
