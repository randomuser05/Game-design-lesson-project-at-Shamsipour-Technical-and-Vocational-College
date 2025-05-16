using System;
using System.Collections;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour

{
    //general...............................................//
public Rigidbody2D RB;
public Joystick move_joy;
public float moveSpeed = 5f;
public bool is_moving;
    //general...............................................//

   
//jump...............................................//
            public float jumpForce = 15f;
            public Transform groundCheck;
            public float groundCheckRadius = 0.2f;
            public LayerMask groundLayer;
            private bool isGrounded;
            private bool duble_jump;
 //jump...............................................


    //dashe...............................................//

      public float dash_power;
      public float dash_time; 

      public float dash_cd;

      private bool can_dashe = true;
      private bool is_dasheing;









    //dash...............................................//


    
  

    //attack...............................................//
    [SerializeField] private float attackRadius = 0.5f; // شعاع دایره حمله
    [SerializeField] private LayerMask enemyLayer; // لایه دشمن برای Overlap

      //attack...............................................//

    //sfx...............................................//

     public AudioClip jumpClip;
    public AudioClip hitClip;
    private AudioSource audioSource;
        //sfx...............................................//

  














    void Start()
    {
              audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
 

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(is_dasheing){
                       


            return;
        }
        player_move();
                

    }

 
        public void jump(){


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

                          if( isGrounded){
                                  

                             RB.linearVelocity = new Vector2(RB.linearVelocity.x, jumpForce);
                             duble_jump = true;
                               audioSource.PlayOneShot(jumpClip);

                          }
                          else if ( duble_jump){
                             RB.linearVelocity = new Vector2(RB.linearVelocity.x, jumpForce);
                             duble_jump = false;
                           }
                          else{
                            duble_jump = false;
                          }
                        

        }


        public void dashe(){


       if (can_dashe)
{
    StartCoroutine(Dash());
}
          

         }

    void player_move(){

        Vector2 moveInput = new Vector2(move_joy.Horizontal , 0);
         RB.linearVelocity = new Vector2(move_joy.Horizontal * moveSpeed *Time.deltaTime, RB.linearVelocity.y);

         if (moveInput.x > 0) {


            GetComponent<Animator>().SetBool("is_walking",true);

             transform.localScale = new Vector3(0.5f,0.5f,0.5f);


         }
          else if (moveInput.x < 0) {

           
           transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
             GetComponent<Animator>().SetBool("is_walking",true);
         }
         else
         {
              GetComponent<Animator>().SetBool("is_walking",false);
         }


    }


    private IEnumerator     Dash(){

         can_dashe = false;
        is_dasheing = true;
          audioSource.PlayOneShot(jumpClip);

        float originalGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        RB.linearVelocity = new Vector2(transform.localScale.x * dash_power, 0f);
         GetComponent<Animator>().SetBool("is_dashing",true);

        yield return new WaitForSeconds(dash_time);

        RB.gravityScale = originalGravity;
        RB.linearVelocity = Vector2.zero;
        is_dasheing = false;
         GetComponent<Animator>().SetBool("is_dashing",false);

        // Cooldown: 5 ثانیه
        yield return new WaitForSeconds(dash_cd);
        can_dashe = true;
        



   


    }


   public void attack(){
    
         GetComponent<Animator>().SetBool("is_attacking",true);
                audioSource.PlayOneShot(hitClip);
                 Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRadius, enemyLayer);
        if (hit != null)
        {
            EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(); // کاهش یک واحد جان دشمن
                Debug.Log("ENEMY_HITED");
            }
        }


   }


      public void end_attack1(){




         GetComponent<Animator>().SetBool("is_attacking",false);


      }
      



              
        
      }

     



  
  

















