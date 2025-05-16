using UnityEngine;

public class witch : MonoBehaviour
{
    public float moveSpeed = 3f; 
    public float changeDirectionTime = 2f; 
        public float attackInterval = 5f; 
    public GameObject projectilePrefab; 
    public float projectileSpeed = 10f; 
    public Transform player; 
        public Transform firePoint; 
    public Animator animator; 

    private Vector2 moveDirection;
    private float directionTimer;     private float attackTimer; 
    void Start()
    {
        
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

 
        directionTimer = changeDirectionTime;
        attackTimer = attackInterval;


        ChangeDirection();
    }

    void Update()
    {

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

 
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            ChangeDirection();
            directionTimer = changeDirectionTime;
        }

   
        FlipTowardsPlayer();

 
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            Attack();
            attackTimer = attackInterval;
        }
    }

    void ChangeDirection()
    {
      
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;
    }

    void FlipTowardsPlayer()
    {
       
        if (player.position.x > transform.position.x)
        {
            
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
           
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

   public void Attack()
    {
   
        if (animator != null)
            animator.SetTrigger("Attack");

      
      
        }

    public void dmg()
    {
          if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        
            Vector2 directionToPlayer = (player.position - firePoint.position).normalized;

            
            if (transform.localScale.x < 0)
                directionToPlayer.x = -Mathf.Abs(directionToPlayer.x);
            else
                directionToPlayer.x = Mathf.Abs(directionToPlayer.x);

          
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = directionToPlayer * projectileSpeed;
            }
        }
        
    }
 }



