using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBot : MonoBehaviour {

    [SerializeField] private GameObject proyectilePrefab;
    private float fireRate = 1f;
    private float timePast = 0f;

    private void Update()
    {
        timePast += Time.deltaTime;
        if(timePast > fireRate)
        {
            Instantiate(proyectilePrefab, transform.position, transform.rotation);
            timePast = 0;
        }
    }
}
