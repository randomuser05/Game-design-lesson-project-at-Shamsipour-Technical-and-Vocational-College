using UnityEngine;
using UnityEngine.UI;

public class player_helth : MonoBehaviour
{
    public int maxHearts = 3;
   public int currentHearts;
    public Image[] heartImages; 
    public AudioClip dmg;
    private AudioSource audioSource;

    public GameObject lose_page;



    

    void Start()
    {
        currentHearts = maxHearts;
        UpdateHeartsUI();
                      audioSource = GetComponent<AudioSource>();

    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy_hit"))
        {
            TakeDamage();
        }
    }

   public void  TakeDamage()
    {
        currentHearts--;
        audioSource.PlayOneShot(dmg);



        UpdateHeartsUI();

        if (currentHearts <= 0)
        {
            Die();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentHearts;
        }
    }

    void Die()
    {    
        Time.timeScale = 0f; // متوقف کردن بازی
        Debug.Log("Game Over");
        lose_page.SetActive(true);
        // یا: SceneManager.LoadScene(SceneManager.GetActiveScene().name); برای ریست کردن
    }
}
