using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEngine : MonoBehaviour
{
    private Transform wizard;
    // Start is called before the first frame update
    void Start()
    {
        wizard = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
       if(wizard.transform.localScale.x < 0)
        {
            transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);

        }
        if (wizard.transform.localScale.x > 0)
        {
            transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y);
        }
        Destroy(gameObject, 2);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
