using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    bool x = true;
    public GameObject Boss;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && x == true)
        {
            Instantiate(Boss, new Vector3(transform.position.x + 27, transform.position.y), transform.rotation);
            x = false;
        }
    }
}
