using System.Collections;
using UnityEngine;

public class TracerLeftClick : Ability {

    [SerializeField] private int bullets;
    private int currentBullets;
    
    [SerializeField] private float fireRate;
    private float currentFireRate;

    [SerializeField] private float reloadTime;
    private float currentReloadTime;
    private bool reloading = false;

    private float rotation = 4f;

    [SerializeField] private GameObject bulletPrefab;

    protected override void Awake()
    {
        base.Awake();
        currentBullets = bullets;
    }

    protected override void Update()
    {
        if (Input.GetMouseButton(0) && currentBullets > 0 && currentFireRate >= fireRate && !reloading)
        {
            StartCoroutine(Cast());
        }

        currentFireRate += Time.deltaTime;

        if(currentBullets <= 0)
        {
            reloading = true;
            currentReloadTime += Time.deltaTime;
            if(currentReloadTime >= reloadTime)
            {
                Reload();
            }
        }

    }

    protected override IEnumerator Cast()
    {
        GameObject bulletRight = Instantiate(bulletPrefab, playerMovementController.firePointRight.transform.position, playerMovementController.firePointRight.transform.rotation);
        bulletRight.transform.Rotate(Vector3.up, -rotation);
        Instantiate(bulletPrefab, playerMovementController.firePointLeft.transform.position, playerMovementController.firePointLeft.transform.rotation);
 
        currentBullets-=2;

        currentFireRate = 0f;

        yield return null;
    }

    private void Reload()
    {
        currentBullets = bullets;
        currentReloadTime = 0f;
        reloading = false;
    }


}
