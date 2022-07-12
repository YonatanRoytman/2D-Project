using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    public float speed;
    public float stoppAtDistance;
   
    //Fire
    public GameObject Fire;
    public GameObject FireParent;
    public float fireRate = 1f;
    public float nextFireTime;
   

    //animator
    Animator anm;


    private Transform target;
    //wizard life
    int hearts;
    //EnemyBlood
    public GameObject enemyBlood;
    void Start()
    {
        //follow player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        //animator
        anm = GetComponent<Animator>();

       
        hearts = 2;

    }

    void Update()
    {
        if(target.transform.position.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector2(-8.231527f, 8.573158f);
        }
        if (target.transform.transform.position.x < gameObject.transform.position.x)
        {
            transform.localScale = new Vector2(8.231527f, 8.573158f);
        }
        //  stop at distance && Flying anim
        if (Vector2.Distance(transform.position, target.position) > stoppAtDistance && hearts > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anm.SetBool("isFlying", true);
        }
        if (Vector2.Distance(transform.position, target.position) <=stoppAtDistance && hearts > 0)
        {
            anm.SetBool("isFlying", false);
        }
        // Fire and timer
        if (Vector2.Distance(transform.position, target.position) <= stoppAtDistance && nextFireTime <Time.time && hearts > 0)
        {
            anm.SetBool("isFlying", false);
            Instantiate(Fire, FireParent.transform.position, FireParent.transform.rotation );
            anm.SetTrigger("shoot");
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Health
        if (collision.gameObject.tag == "PlayerKnife" && hearts > 0)
        {
            hearts--;
            Instantiate(enemyBlood, transform.position, transform.localRotation);
        }
        if(collision.gameObject.tag == "PlayerKnife" && hearts <= 0)
        {
            anm.SetBool("isFlying", false);
            anm.SetTrigger("dead");
            Destroy(gameObject, 1);
        }
    }
}
