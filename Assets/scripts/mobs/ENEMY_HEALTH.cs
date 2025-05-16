using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
            public AudioClip die;
                 private AudioSource audioSource;


    [SerializeField] private int maxHealth = 2; // حداکثر جان دشمن (3 واحد)
    private int currentHealth; // جان فعلی

    void Start()
    {
        currentHealth = maxHealth; // مقداردهی اولیه
         audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--; // کاهش یک واحد جان
            Debug.Log($"دشمن یک واحد جان از دست داد! جان باقی‌مانده: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die(); // مرگ دشمن
            }
        }
    }

    void Die()
    {
      GetComponent<Animator>().SetTrigger("DEAD");
      audioSource.PlayOneShot(die);
      
    }
    public void scraps_out()
    {
        Destroy(gameObject);
    }
}