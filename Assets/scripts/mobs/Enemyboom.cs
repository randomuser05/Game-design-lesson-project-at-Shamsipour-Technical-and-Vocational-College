using UnityEngine;

public class Enemyboom : MonoBehaviour
{
    public float speed = 3f; 
    public float triggerDistance = 0.5f; 
    private Transform player; 
    private SpriteRenderer spriteRenderer; 
    private Animator animator; 
    private bool hasExploded = false; 
    private static readonly int IsWalking = Animator.StringToHash("IsWalking"); // نام پارامتر انیمیشن
    
    public int boom_hp;

    public float explosionRadius = 5f; // شعاع انفجار (۵ واحد)
    
     public int maxHearts = 2;
     private int currentHearts;

         public float knockbackForce = 5f;

           public AudioClip boom;
               public AudioClip boomfuse;
    private AudioSource audioSource;


    void Start()
    {

        currentHearts = maxHearts;
                      audioSource = GetComponent<AudioSource>();

      
        // پیدا کردن پلیر با تگ "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // گرفتن کامپوننت SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // گرفتن کامپوننت Animator
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hit"))
        { 
          
            TakeDamage();
        }
    }
    void TakeDamage()
    {
     if (currentHearts > 0 ){
        
        currentHearts--;

     }


       else if (currentHearts <= 0)
        {
            Boom();
        }
    }




    void Update()
    {    
        
        
        if (player == null || hasExploded) return; // اگر پلیر وجود ندارد یا انفجار رخ داده، ادامه نده

        // حرکت به سمت پلیر
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // تنظیم جهت نگاه به سمت پلیر
        Vector3 enemyScale = transform.localScale;
        if (player.position.x < transform.position.x)
        {
            // پلیر در سمت چپ دشمن است، به چپ نگاه کن
            enemyScale.x = -Mathf.Abs(enemyScale.x);
        }
        else
        {
            // پلیر در سمت راست دشمن است، به راست نگاه کن
            enemyScale.x = Mathf.Abs(enemyScale.x);
        }
        transform.localScale = enemyScale;

        // فعال کردن انیمیشن راه رفتن
        animator.SetBool(IsWalking, true);

        // بررسی فاصله با پلیر
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= triggerDistance)
        {
            Boom(); 
        }
    }

    void Boom()
    {
               

        hasExploded = true; 
        animator.SetBool(IsWalking, false);
         GetComponent<Animator>().SetBool("boom",true);
         Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
         

        Debug.Log("Boom! دشمن به پلیر رسید!");
        
                   GetComponent<Animator>().SetBool("boom",true);

    }

    public void destroy(){
               Destroy(gameObject); 



    }
    public void sfx(){
         audioSource.PlayOneShot(boom);
    }
        public void sfx_fuse(){
         audioSource.PlayOneShot(boomfuse);
    }
 


    






}