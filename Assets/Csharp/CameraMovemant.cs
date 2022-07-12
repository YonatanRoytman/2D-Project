using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemant : MonoBehaviour
{
    public Transform playerTrn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTrn.position.x, playerTrn.position.y, -10);

    }
}
