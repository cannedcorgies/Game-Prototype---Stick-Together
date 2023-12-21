using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatformMove : MonoBehaviour
{

    public bool activated;
    public bool heavy;

    public GameObject[] platforms;

    [SerializeField] private Vector2 startPos;
    public float moveSpeed;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        var ray = Physics2D.Raycast(transform.position, Vector2.up, distance);

        if (ray) {

            if (heavy) {

                if (ray.collider.gameObject.tag == "Heavy" || ray.collider.gameObject.GetComponent<MergedCube>()) {

                    foreach (GameObject platform in platforms) {

                        var move = platform.gameObject.GetComponent<MoveTowardsTarget>();
                        move.activated = true;

                    }

                } else {

                    foreach (GameObject platform in platforms) {

                        var move = platform.gameObject.GetComponent<MoveTowardsTarget>();
                        move.activated = false;

                    }

                }

            } else { foreach (GameObject platform in platforms) {

                    var move = platform.gameObject.GetComponent<MoveTowardsTarget>();
                    move.activated = true;

                } }

        } else {

            foreach (GameObject platform in platforms) {

                var move = platform.gameObject.GetComponent<MoveTowardsTarget>();
                move.activated = false;

            }

        }

    }

}
