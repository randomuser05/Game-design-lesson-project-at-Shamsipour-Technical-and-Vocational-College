using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 

    void Start()
    {
        
      
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
       
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;
        }
    }
}