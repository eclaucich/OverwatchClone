using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL DISPARO DE FLECHAS DE HANZO
/// 
/// DISPARO NORMAL:         SE MANTIENE APRETADO EL BOTÓN DE DISPARO, MIENTRAS MAS CARGADO, MAS LEJOS Y MAS DAÑO HACE LA FLECHA
/// HABILIDAD PRIMARIA:     POSIBILIDAD DE DISPARAR UNA DETERMINADA CANTIDAD DE FLECHAS AUTOMATICAMENTE CARGADAS AL MÁXIMO, EN UN TIEMPO DETERMINADO
/// HABILIDAD SECUNDARIA:   LA PROXIMA FLECHA, AL COLISIONAR, REVELA OBJETIVOS DENTRO DE UN RADIO
/// 
/// FALTA IMPLEMENTAR LA OPCIÓN DE CANCELAR UN DISPARO CARGADO
/// </summary>
/// 

public class HanzoLeftClick : Ability {

    //FLECHA NORMAL
    [SerializeField] private float maxHoldingFire = 5f;                                 //TENSIÓN (TIEMPO) MÁXIMO PARA CARGAR EL DISPARO (SE PUEDE SEGUIR CARGANDO PERO LA FUERZA NO SUBE)
    private float currentHoldingFire = 0f;                                              //TENSIÓN (TIEMPO) ACTUAL DEL CARGADO DEL DISPARO

    //PREFABS DE LAS FLECHAS
    [SerializeField] private GameObject proyectilePrefab;                               //PREFAB DE LA FLECHA NORMAL
    [SerializeField] private GameObject proyectileVisionPrefab;                         //PREFAB DE LA FLECHA DE VISIÓN

    private HanzoMovementController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<HanzoMovementController>();
    }

    protected override void Update()
    {
        if (playerMovementController.IsClimbing())
        {
            return;
        }

        //CONTROL DE SALIDA DE LOS ESTADOS
        if (!playerController.IsRapidFire())                                                           //SI NO ESTA ACTIVADA LA HABILIDAD DE DISPARO RÁPIDO
        {
            if (Input.GetMouseButton(0))                                                //FLECHA NORMAL, SI SE MANTIENE APRETADO SE CARGA EL DISPARO
            {
                playerMovementController.SetAiming(true);                               //SE SETEA TRUE EL APUNTADO
                StartCoroutine(Cast());
            }

            if (Input.GetMouseButtonUp(0))                                          //CUANDO SE SUELTA EL TRIGGER, SE DISPARA LA FLECHA
            {
                Fire(currentHoldingFire);                                           //FUNCIÓN DE DISPARO
                currentHoldingFire = 0f;                                            //SE RESETEA LA TENSIÓN 

                playerMovementController.SetAiming(false);                          //SE DEJA DE APUNTAR, SE SETEA FALSE
            }
        }
    }

    /*FLECHA NORMAL*/
    protected override IEnumerator Cast()
    {
        if (currentHoldingFire < maxHoldingFire)                                        //CARGANDO EL TIRO, TODAVÍA NO SE ALCANZÓ LA TENSIÓN MÁXIMA
        {
            currentHoldingFire += Time.deltaTime;                                       //ACTUALIZAR LA TENSIÓN
        }
        else                                                                            //LA TENSIÓN YA ES LA MÁXIMA, ENTONCES SE FIJA EN ESE MÁXIMO
        {
            currentHoldingFire = maxHoldingFire;
        }

        yield return null;
    }

    /*DISPARAR UNA FLECHA CON UNA TENSIÓN DETERMINADA*/
    public void Fire(float holdingFire)                                                 //SE CREA CON LA POSICIÓN Y ROTACIÓN DEL PUNTO DE DISPARO DEL PERSONAJE
    {
        GameObject proyectile;                                                          //OBJETO QUE REFERENCIA EL PROYECTIL
        if (playerController.IsVisionArrow())                                           //SI ESTABA ACTIVA LA HABILIDAD DE VISIÓN, EL SIGUIENTE DISPARO ES CON EL EFECTO DE VISIÓN
        {
            proyectile = Instantiate(proyectileVisionPrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);
            GetComponent<HanzoSecondAbility>().SetOnCooldown(true);
        }
        else                                                                            //SI NO ESTABA ACTIVADA NINGUNA HABILIDAD, EL SIGUIENTE DISPARO ES NORMAL
        {
            proyectile = Instantiate(proyectilePrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);
        }

        proyectile.GetComponent<HanzoArrow>().SetSpeed(holdingFire);                    //SETEAR LA VELOCIDAD DE LA FLECHA SEGÚN LA TENSIÓN
    }

}
