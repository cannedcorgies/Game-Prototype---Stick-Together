using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    public GameObject NextLevel;
    public NextLevelQuota nlq;

    // Start is called before the first frame update
    void Start()
    {
        
        nlq = NextLevel.gameObject.GetComponent<NextLevelQuota>();

    }

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.tag == "Player") {

            if (col.gameObject.GetComponent<MergedCube>()) {

                nlq.activationCount += 2;
                Debug.Log("biggo cube!");

            } else {

                nlq.activationCount += 1;
                Debug.Log("one cube!");

            }

            Debug.Log("eh?");

            col.gameObject.SetActive(false);
        
        }

    }

}
