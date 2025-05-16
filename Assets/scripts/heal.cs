using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 5f; // زمان نابودی آیتم (5 ثانیه)

    void Start()
    {
        Debug.Log($"HealItem spawned: {gameObject.name}, will destroy in {destroyDelay} seconds");
        // نابودی آیتم پس از 5 ثانیه
        Destroy(gameObject, destroyDelay);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision detected with: {other.gameObject.name}, Tag: {other.tag}");

        // بررسی برخورد با پلیر
        if (other.CompareTag("Player"))
        {
            // دسترسی به اسکریپت PlayerHealth
            player_helth playerHealth = other.GetComponent<player_helth>();
            if (playerHealth != null)
            {
                Debug.Log($"Player found, setting health to 3. Previous health: {playerHealth.currentHearts}");
                // تنظیم سلامتی پلیر به 3
                playerHealth.currentHearts = 3;
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on Player!");
            }

            // نابودی فوری آیتم پس از برخورد
            Debug.Log("Destroying HealItem after collision with Player");
            Destroy(gameObject);
        }
    }
}