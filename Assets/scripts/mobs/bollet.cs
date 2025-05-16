using UnityEngine;

public class bollet : MonoBehaviour
{
     private float destroyDelay = 5f; 
    
        void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

}
