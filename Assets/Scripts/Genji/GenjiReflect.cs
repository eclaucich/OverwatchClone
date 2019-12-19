using System.Collections;
using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DEL REFLECTAR DE GENJI
/// 
/// HEREDA DE HABILIDAD
/// </summary>

public class GenjiReflect : Ability {

    [SerializeField] private GameObject reflectPrefab;                                  //PREFAB DEL HIT BOX DEL REFLECTAR
    [SerializeField] private float maxTimeReflect;

    private GenjiMovementController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<GenjiMovementController>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldown && !playerController.IsDashing())                                                //SI SE APRIETA EL TRIGGER, CASTEAR
        {
            StartCoroutine(Cast());
            playerController.SetReflecting(true);
        }
    }

    protected override IEnumerator Cast()
    {
        Vector3 position = playerMovementController.transform.position + playerMovementController.transform.forward * 1f;       //LA POSICIÓN PARA CREAR EL HITBOX ES UN POCO MÁS ADELANTE QUE EL PERSONAJE
        GameObject reflectBox = Instantiate(reflectPrefab, position, playerMovementController.transform.rotation);              //SE CREA EL HITBOX CON LA POSITION CALCULADA, Y LA ROTACIÓN DEL PERSONAJE
        reflectBox.transform.parent = Camera.main.transform;                                                                    //SE PONE EL HITBOX COMO HIJO DE LA CAMARÁ, ENTONCES UNA VEZ CREADO SE PUEDE MOVER CON LA CÁMARA

        reflectBox.GetComponent<ReflectBox>().SetAliveTime(maxTimeReflect);

        yield return new WaitForSeconds(maxTimeReflect);

        playerController.SetReflecting(false);
        SetOnCooldown(true);
    }
}
