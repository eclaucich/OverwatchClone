using UnityEngine;

public class Pinable : MonoBehaviour
{
    

    public void StartPin(Transform parentTransform)
    {
        this.transform.parent = parentTransform;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void StopPin()
    {
        GetComponent<BoxCollider>().enabled = true;
        this.transform.parent = null;  
    }
}
