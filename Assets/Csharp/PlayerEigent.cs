using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEigent : MonoBehaviour
{
    float x;
    float speed;
    //animator
    Animator anm;

    //jumping
    Rigidbody2D rb;
    bool isFloor;

    //shooting
    public GameObject KnifePlayer;

    //Shooting Timer
    public float shootingTimer;
    public float timer; 
    //health
    public Healthbarscript healthBar;
    public int maxHealth = 4;
    public int currentHealth;
    Collider2D Box;
    Collider2D Capsule;
    public GameObject GameOver;
    //Blood
    public GameObject blood;

    //Respawn Point
    private Vector2 respawnPoint;
    public GameObject fallDitector;

    //Sound
    public AudioSource audioSource;
    public AudioClip backGroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        

        speed = 5f;
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFloor = true;
        timer = Time.deltaTime;
        Box = GetComponent<BoxCollider2D>();
        Capsule = GetComponent<CapsuleCollider2D>();
        healthBar.setHealth(maxHealth);
        currentHealth = maxHealth;
        respawnPoint = transform.position;

        //music

        audioSource.PlayOneShot(backGroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
       
        //Movemant

        x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(x, 0, 0);
        //idle
        if(x == 0)
        {
            Capsule.enabled = true;
            anm.SetBool("isWalking", false);
            anm.SetBool("isRunning", false);
            anm.SetBool("Crouch", false);
            Box.enabled = false;


        }
        //Walking
        else if (x > 0 && Input.GetKey(KeyCode.LeftShift) == false)
            {
            Capsule.enabled = true;
            anm.SetBool("isWalking", true);
            transform.localScale = new Vector2(2.882717f, transform.localScale.y);
            anm.SetBool("isRunning", false);
            anm.SetBool("Crouch", false);
            
            Box.enabled = false;

        }
        else if (x < 0 && Input.GetKey(KeyCode.LeftShift) == false)
        {
            Capsule.enabled = true;
            anm.SetBool("isWalking", true);
            transform.localScale = new Vector2(-2.790123f, transform.localScale.y);
            anm.SetBool("isRunning", false);
            anm.SetBool("Crouch", false);
            
            Box.enabled = false;

        }
        //Runing
        else if (x < 0 && Input.GetKey(KeyCode.LeftShift) == true)
        {
            Capsule.enabled = true;
            anm.SetBool("isWalking", false);
            transform.localScale = new Vector2(-2.790123f, transform.localScale.y);
            anm.SetBool("isRunning", true);
            anm.SetBool("Crouch", false);
            speed = 10;
            
            Box.enabled = false;
        }
        else if (x > 0 && Input.GetKey(KeyCode.LeftShift) == true)
        {
            Capsule.enabled = true;
            anm.SetBool("isWalking", false);
            transform.localScale = new Vector2(2.882717f, transform.localScale.y);
            anm.SetBool("isRunning", true);
            anm.SetBool("Crouch", false);
            speed = 10;
            
            Box.enabled = false;
        }
        
        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isFloor == true)
        {
            Capsule.enabled = true;
            anm.SetBool("isWalking", false);
            rb.AddForce(new Vector2(0, 550));
            isFloor = false;
            anm.SetBool("isRunning", false);
            anm.SetBool("Crouch", false);
            anm.SetTrigger("Jump");
            



        }
        //Crouch
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Capsule.enabled = false;
            Box.enabled = true;
            anm.SetBool("isWalking", false);
            anm.SetBool("isRunning", false);
            anm.SetBool("Crouch", true);
        }
        
        //Shooting + Timer 
        timer += Time.deltaTime;
        if (timer >= shootingTimer && Input.GetKeyDown(KeyCode.X))
        {   
            Instantiate(KnifePlayer, transform.position, KnifePlayer.transform.rotation);
            anm.SetBool("isWalking", false);
            anm.SetBool("isRunning", false);
            anm.SetTrigger("Throw");

            timer = 0;
        }

        fallDitector.transform.position = new Vector2(transform.position.x, fallDitector.transform.position.y);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isFloor = true;
        }
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Magic" || collision.gameObject.tag == "Traps" || collision.gameObject.tag == "BossShot")
        {
            //currentHealth--;
           // healthBar.MaxHealth(currentHealth);
            anm.SetTrigger("Damage");
            TakeDamage(1);
            Instantiate(blood, transform.position, transform.localRotation);
        }
        
    }

    void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        
       // dead anim
        if (currentHealth == 0)
        {
            anm.SetTrigger("dead");
            Destroy(gameObject, 2);
            GameOver.SetActive(true);
           

            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heart" && currentHealth < 8)
        {
            currentHealth++;
            healthBar.setHealth(currentHealth);

            Destroy(collision.gameObject);
        }
        else if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
      
    }

}
