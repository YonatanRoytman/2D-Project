using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpawner : MonoBehaviour
{
    bool x = true;
    public GameObject Wizard;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && x == true)
        {
            Instantiate(Wizard, new Vector3(transform.position.x + 18f, transform.position.y), transform.rotation);
            x = false;
        }
    }
}
