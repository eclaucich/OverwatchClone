using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DE LA HITBOX DE LOS ATAQUES BÁSICOS DE REINHARDT
/// </summary>

public class ReinhardtLeftClickHitBox : MonoBehaviour {

    [SerializeField] private float maxTimeAlive;                //TIEMPO MÁXIMO DE VIDA
    private float currentTimeAlive;                             //TIEMPO ACTUAL DE VIDA

    private void Update()
    {
        currentTimeAlive += Time.deltaTime;                     //SE ACTUALIZA EL TIEMPO ACTUAL DE VIDA

        if(currentTimeAlive > maxTimeAlive)                     //SI SE SUPERA EL TIEMPO MÁXIMO DE VIDA, SE DESTRUYE
        {
            Destroy(gameObject);
        }
    }
}
