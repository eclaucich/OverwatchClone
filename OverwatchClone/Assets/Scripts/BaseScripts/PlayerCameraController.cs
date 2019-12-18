using UnityEngine;

/// <summary>
/// CONTROL BASE DE LA CAMARA DE LOS PERSONAJES
/// </summary>

public class PlayerCameraController : MonoBehaviour {

    [SerializeField] private float lookSensitivity;         
    [SerializeField] private float lookSmoothing;

    private Transform playerTransform;
    private Vector2 currentLookingDirection;
    private Vector2 smoothedVelocity;

    private bool isLocked = false;

    protected virtual void Awake()
    {
        playerTransform = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected virtual void Update()
    {
        RotateCamera();
    }

    protected virtual void RotateCamera()
    {
        if (isLocked)
        {
            return;
        }

        Vector2 cameraRotationInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        cameraRotationInput = Vector2.Scale(cameraRotationInput, new Vector2(lookSensitivity * lookSmoothing, lookSmoothing * lookSmoothing));

        smoothedVelocity = Vector2.Lerp(smoothedVelocity, cameraRotationInput, 1 / lookSmoothing);

        currentLookingDirection += smoothedVelocity;
   
        transform.localRotation = Quaternion.AngleAxis(-currentLookingDirection.y, Vector3.right);
        playerTransform.localRotation = Quaternion.AngleAxis(currentLookingDirection.x, playerTransform.up);
    }

    public void Lock(bool aux)
    {
        isLocked = aux;

        if (!isLocked)
        {
            smoothedVelocity = new Vector2();

            currentLookingDirection = new Vector2(playerTransform.eulerAngles.y, -transform.localEulerAngles.x);
        }
    }
}
