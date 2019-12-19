using System.Collections;
using UnityEngine;

public class HanzoPrimaryAbility : Ability {

    [SerializeField] private float abilityTime = 5f;
    private float currentAbilityTime;

    [SerializeField] private int maxRapidFireArrows = 5;
    private int currentRapidFireArrows = 5;

    private float timeBtwRapidFire = 0.2f;
    private float currentTimeBtwFire = 0f;

    private HanzoMovementController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<HanzoMovementController>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q) && !isOnCooldown)
        {
            playerController.SetRapidFire(true);
            currentRapidFireArrows = maxRapidFireArrows;
            currentAbilityTime = 0f;
        }

        if (playerController.IsRapidFire())
        {
            currentAbilityTime += Time.deltaTime;
            if(currentAbilityTime > abilityTime)
            {
                playerController.SetRapidFire(false);
                SetOnCooldown(true);
            }

            currentTimeBtwFire += Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                StartCoroutine(Cast());
            }
        }

        if (isOnCooldown)
        {
            playerController.SetRapidFire(false);
        }

    }

    protected override IEnumerator Cast()
    {
        if (currentRapidFireArrows > 0)                                                 //MIENTRAS QUE SE TENGAN FLECHAS PARA DISPARAR Y NO SE ACABÓ EL TIEMPO DE LA HABILIDAD
        {
            if (currentTimeBtwFire >= timeBtwRapidFire)                                 //COMPROBAR SI SE SUPERÓ EL MÍNIMO DE TIEMPO ENTRE EL DISPARO ANTERIOR
            {
                GetComponent<HanzoLeftClick>().Fire(2f);                                //EL "2" HACE REFERENCIA A LA TENSIÓN MÁXIMA, NO HARDCODEARLO!!!!

                currentRapidFireArrows--;                                               //SE DISPARÓ, ENTONCES QUEDA UNA FLECHA MENOS

                currentTimeBtwFire = 0f;                                                //SE DISPARÓ, ENTONCES SE RESETEA EL TIEMPO ENTRE DISPAROS
            }
        }
        else                                                                            //SI SE ACABARON LAS FELCHAS O EL TIEMPO DE HABILIDAD, ENTONCES CANCELAR HABILIDAD
        {
            SetOnCooldown(true);
            yield return null;
        }

        yield return null;
    }

}
