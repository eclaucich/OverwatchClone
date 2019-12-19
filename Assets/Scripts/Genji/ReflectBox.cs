using UnityEngine;

/// <summary>
/// FUNCIONAMIENTO DE LA HITBOX DEL REFLECT DE GENJI
/// </summary>

public class ReflectBox : MonoBehaviour {

    private float maxTimeAlive;                                 //MÁXIMO TIEMPO DE VIDA DE LA HITBOX
    private float currentTimeAlive = 0f;                        //TIEMPO ACTUAL DE VIDA DE LA HITBOX           

    private void Update()
    {
        if(currentTimeAlive > maxTimeAlive)                     //SI SE SUPERA EL TIEMPO MÁXIMO DE VIDA, SE DESTRUYE
        {
            Destroy(gameObject);
        }

        currentTimeAlive += Time.deltaTime;                     //SE ACTUALIZA EL TIEMPO ACTUAL DE VIDA DE LA HITBOX
    }

    public void SetAliveTime(float timeAlive)
    {
        maxTimeAlive = timeAlive;
    }
}
