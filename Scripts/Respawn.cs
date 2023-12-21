using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject respawnPoint;
    //private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        //rb = gameObject.GetComponent<Rigidbody2D>();

    }

    public void ToPoint(){

        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH WHY");
        //rb.velocity = new Vector2 (0, 0);
        transform.position = respawnPoint.transform.position;

    }

}
