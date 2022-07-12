using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEngine : MonoBehaviour
{
    Vector3 KnifeL;
    public GameObject Player;
    public Transform PlayerKnife;
    float speed = 0.4f;
    bool dir;

    private void Awake()
    {
        PlayerKnife = transform.Find("PlayerKnife");
        Player = GameObject.Find("Player");
        if (Player.transform.localScale.x < 0)
        {
            dir = true;
            transform.Rotate(0, 180, 0);

            speed = -0.3f;
        }
        else
        {
            dir = false;
            KnifeL = new Vector3(0, 180, 0);
            speed = 0.3f;
        }
    }
 

    // Update is called once per frame
    void Update()
    {
       if (dir == true)
        {
            transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        }
       else if(dir == false)
        {
            transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
        }

       // Destroy knife after time
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" ||collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
