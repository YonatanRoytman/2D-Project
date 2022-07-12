using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossEngine : MonoBehaviour
{
    public float speed;
    public float stoppAtDistance;
    bool x = false;

    //Fire
    public GameObject Fire;
    public GameObject FireParent;
    public float fireRate = 1f;
    public float nextFireTime;
    //animator
    Animator anm;
    private Transform target;
    //Boss life
    public BossHealthBar healthBar;
    public int maxHearts = 12;
    public int currentHearts;
    //BossBlood
    public GameObject bossblood;

    public GameObject Victory;


    void Start()
    {
        //follow player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //animator
        anm = GetComponent<Animator>();
        healthBar.setHealth(maxHearts);
        currentHearts = maxHearts;



    }

    void Update()
    {
        if (target.transform.position.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector2(-11.25676f, 11.30819f);
        }
        if (target.transform.position.x < gameObject.transform.position.x)
        {
            transform.localScale = new Vector2(11.25676f, 11.30819f);
        }
        //  stop at distance
        if (Vector2.Distance(transform.position, target.position) > stoppAtDistance && currentHearts > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
          
        }
        // Fire and timer    when shooting 
        if (Vector2.Distance(transform.position, target.position) <= stoppAtDistance && nextFireTime < Time.time && currentHearts > 0)
        {
            Instantiate(Fire, FireParent.transform.position, FireParent.transform.rotation);
          //  anm.SetTrigger("shoot");
            nextFireTime = Time.time + fireRate;
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Health
        if (collision.gameObject.tag == "PlayerKnife" && currentHearts > 0)
        {
            TakeDamage(1);
            Instantiate(bossblood, transform.position, transform.localRotation);
        }
        if (collision.gameObject.tag == "PlayerKnife" && currentHearts <= 0)
        {
             anm.SetTrigger("Dead");

            Destroy(gameObject, 3);
            {
                Victory.SetActive(true);
            }

        }
    }

    void TakeDamage(int damage)
    {
        currentHearts -= damage;
        healthBar.setHealth(currentHearts);
    }
    
}
