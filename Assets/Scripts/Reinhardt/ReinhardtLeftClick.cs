using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL CLICK IZQUIERDO DE REINHARDT
/// 
/// ATAQUES BÁSICOS
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class ReinhardtLeftClick : Ability {

    [SerializeField] private float timeBtwAttack;                                               //TIEMPO MÍNIMO ENTRE ATAQUES
    private float currentTimeBtwAttack;                                                         //TIEMPO ACTUAL ENTRE ATAQUES

    private float castTime = 0.2f;                                                              //TIEMPO DE CASTEO
    private float currentCastTime;                                                              //TIEMPO ACTUAL DE CASTEO

    [SerializeField] private GameObject hitBoxPrefab;                                           //PREFAB DE LA HITBOX

    protected override void Awake()
    {
        base.Awake();                                                                           //HACER LO QUE HACE EL AWAKE DEL PADRE
        currentTimeBtwAttack = timeBtwAttack;                                                   //SETEAR EL TIEMPO ACTUAL ENTRE ATAQUES
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !playerMovementController.IsDashing())                   //SI SE APRIETA EL TRIGGER, Y NO SE ESTÁ DASHEANDO, SE CASTEA
        {
            StartCoroutine(Cast());
        }

        if(Input.GetMouseButtonUp(0))                                                           //SI SE SUELTA EL TRIGGER, SE RESETEA EL TIEMPO DE CASTEO
        {
            currentCastTime = 0f;
        }

        currentTimeBtwAttack += Time.deltaTime;                                                 //SE ACTUALIZA EL TIEMPO ENTRE ATAQUES
    }

    protected override IEnumerator Cast()
    {
        if(currentTimeBtwAttack >= timeBtwAttack && currentCastTime >= castTime)                                        //SI EL TIEMPO ACTUAL ENTRE ATAQUES ES MAYOR QUE EL MÍNIMO Y SE SUPERÓ EL TIEMPO DE CASTEO
        {
            Vector3 hitBoxPosition = playerMovementController.firePointRight.transform.position;                        //SETEAR POSICIÓN DE LA HITBOX
            hitBoxPosition += playerMovementController.firePointRight.transform.forward;        

            Instantiate(hitBoxPrefab, hitBoxPosition, playerMovementController.firePointRight.transform.rotation);      //CREAR HITBOX
            currentTimeBtwAttack = 0f;                                                                                  //RESETEAR TIEMPO ENTRE ATAQUES

            currentCastTime = 0f;                                                                                       //RESETEAR TIEMPO DE CASTEO
        }

        currentCastTime += Time.deltaTime;                                                                              //ACTUALIZAR TIEMPO DE CASTEO

        yield return null;
    }
}
