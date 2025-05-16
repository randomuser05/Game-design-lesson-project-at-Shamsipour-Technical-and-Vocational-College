using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // سرعت حرکت دشمن
    [SerializeField] private float attackRange = 1f; // فاصله لازم برای حمله
    [SerializeField] private float attackDelay = 5f; // تاخیر بین حملات (5 ثانیه)
    [SerializeField] private float damageRadius = 0.5f; // شعاع دایره آسیب
    [SerializeField] private LayerMask playerLayer; // لایه پلیر برای Overlap
    private Transform player; // رفرنس به پلیر
    private float lastAttackTime; // زمان آخرین حمله
    private Vector3 originalScale; // مقیاس اصلی دشمن
    private Animator animator; // رفرنس به Animator
    
public AudioClip attack_triger;
            public AudioClip attack;

     private AudioSource audioSource;

    void Start()
    {
        // پیدا کردن پلیر
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // ذخیره مقیاس اصلی
        originalScale = transform.localScale;
        // گرفتن کامپوننت Animator
        animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return; // اگر پلیر پیدا نشد، ادامه نده

        // فاصله تا پلیر
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // حرکت به سمت پلیر
        if (distanceToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }

        // چرخاندن دشمن به سمت پلیر با تغییر localScale
        Vector2 direction = (player.position - transform.position).normalized;
        if (direction.x > 0) // پلیر در سمت راست
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (direction.x < 0) // پلیر در سمت چپ
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // حمله
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackDelay)
        {
              animator.SetTrigger("AttackTrigger"); 
            Attack();
        }
    }

    void Attack()
    {    animator.SetTrigger("AttackTrigger"); 
        Debug.Log("دشمن حمله کرد!");
         // فعال کردن انیمیشن حمله
        lastAttackTime = Time.time; // به‌روزرسانی زمان آخرین حمله

        // بررسی Overlap برای آسیب به پلیر
        Collider2D hit = Physics2D.OverlapCircle(transform.position, damageRadius, playerLayer);
        if (hit != null)
        {
            player_helth playerHealth = hit.GetComponent<player_helth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(); // کاهش یک قلب
                Debug.Log("پلیر یک قلب از دست داد!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // نمایش محدوده حرکت به سمت پلیر
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        // نمایش محدوده آسیب
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

     public void attack_triger_sfx(){
                 audioSource.PlayOneShot(attack_triger);

    }
     public void attack_sfx(){
                 audioSource.PlayOneShot(attack);

    }
}