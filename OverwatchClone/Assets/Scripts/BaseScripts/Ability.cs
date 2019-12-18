using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CLASE BASE DE HABILIDAD
/// 
/// TODAS LAS HABILIDADES PUEDEN USAR, SI ES NECESARIO, EL CONTROLADOR DE MOVIMIENTO DEL PERSONAJE
/// 
/// TODAS LAS HABILIDADES TIENEN QUE TENER UN CAST
/// </summary>

public abstract class Ability : MonoBehaviour {

    protected PlayerMovementController playerMovementController;
    protected PlayerCameraController playerCameraController;

    [SerializeField] protected float cooldown;
    protected float currentCooldownTimer = 0f;
    protected bool isOnCooldown = false;
    [SerializeField] protected Image imageUI;

    protected virtual void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraController = GetComponentInChildren<PlayerCameraController>();
    }

    protected virtual void Update()
    {
        if (isOnCooldown)
        {
            currentCooldownTimer += Time.deltaTime;
            if(currentCooldownTimer > cooldown)
            {
                SetOnCooldown(false);
                currentCooldownTimer = 0f;
            }
        }
    }

    public void SetOnCooldown(bool aux)
    {
        isOnCooldown = aux;

        if (isOnCooldown)
        {
            imageUI.color = new Color(0f, 0f, 0f);
        }
        else{
            imageUI.color = new Color(255f, 255f, 255f);
        }

    }

    public bool IsOnCooldown()
    {
        return isOnCooldown;
    }

    protected abstract IEnumerator Cast();
}
