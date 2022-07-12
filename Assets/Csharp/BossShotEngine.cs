using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotEngine : MonoBehaviour
{
    private Transform Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Boss.transform.localScale.x < 0)
        {
            transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
            gameObject.transform.localScale = new Vector2(-3.547169f, 5.100058f);

        }
        if (Boss.transform.localScale.x > 0)
        {
            transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y);
        }
        Destroy(gameObject,2);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
