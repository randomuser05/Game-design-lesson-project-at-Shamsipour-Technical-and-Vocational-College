using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public Vector2 minBounds; 
    public Vector2 maxBounds; 

    void LateUpdate()
    {
        // محدود کردن موقعیت پلیر به مرزهای مپ
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}