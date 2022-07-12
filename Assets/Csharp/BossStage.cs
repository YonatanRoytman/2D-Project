using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStage : MonoBehaviour
{
    public GameObject BossFight;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerKnife")
        {
            BossFight.SetActive(true);

        }
    }
}
