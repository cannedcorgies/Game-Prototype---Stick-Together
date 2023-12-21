using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public bool activated = true;
    public bool player01 = true;

    private Rigidbody2D rb;
    private BoxCollider2D myCol;

    public float speed = 100f;
    public bool flying = false;
    public float flyingTimestamp = 0f;
    public Vector2 flyingDir;

    public GameObject merge;
    public MergedCube mc;

    // Start is called before the first frame update
    void Start()
    {

        name = gameObject.name;

        rb = gameObject.GetComponent<Rigidbody2D>();
        mc = merge.GetComponent<MergedCube>();

        myCol = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (flying) {

            rb.gravityScale = 0;

            rb.velocity = rb.velocity.normalized * speed;
            flyingDir = rb.velocity;

        }
        
        Move();

    }

    public void Move() {

        if (!flying) {

            if (player01) {

                if (Input.GetKeyDown(KeyCode.W)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (0, 1) * speed;

                }

                if (Input.GetKeyDown(KeyCode.A)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (-1, 0) * speed;

                }

                if (Input.GetKeyDown(KeyCode.S)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (0, -1) * speed;

                }

                if (Input.GetKeyDown(KeyCode.D)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (1, 0) * speed;

                }

            } else {

                if (Input.GetKeyDown(KeyCode.UpArrow)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (0, 1) * speed;

                }

                if (Input.GetKeyDown(KeyCode.LeftArrow)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (-1, 0) * speed;

                }

                if (Input.GetKeyDown(KeyCode.DownArrow)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (0, -1) * speed;

                }

                if (Input.GetKeyDown(KeyCode.RightArrow)) {

                    flying = true;
                    flyingTimestamp = Time.time;

                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2 (1, 0) * speed;

                }

            }

        }

    }

    public void MoveInDir(Vector2 dir) {

        Debug.Log("WHOOOOOOOOOOOO");
        Debug.Log(" -- " + dir);

        flying = true;

        rb.velocity = Vector2.zero;
        rb.velocity = dir * speed;

    }

    void OnCollisionEnter2D( Collision2D col ) {
        
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;

        if (flying && col.gameObject.tag == "Player") {

            mc.activated = true;

            Debug.Log("hey, we bumped");

            var average = (transform.position + col.transform.position) / 2;

            if (player01) {

                mc.p1_vel = flyingDir;
                mc.p1_time = flyingTimestamp;

            } else {

                mc.p2_vel = flyingDir;
                mc.p2_time = flyingTimestamp;

            }
            merge.SetActive(true);

            merge.transform.position = average;

            col.gameObject.SetActive(false);
            gameObject.SetActive(false);

        }
        
        flying = false;

    }

}
