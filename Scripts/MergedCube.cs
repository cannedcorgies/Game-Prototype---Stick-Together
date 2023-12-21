using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedCube : MonoBehaviour
{

    public bool activated = false;
    public float scaleUp = 0.01f;
    public float scaleTarget = 4f;

    public float speed;
    public float jump;
        [SerializeField] private bool grounded;
        [SerializeField] private bool liftOff;
        public float coyoteTime;
        [SerializeField] private bool p1_jump;
        [SerializeField] private bool p2_jump;

    public float launch;
        public Vector2 p1_vel;
        public Vector2 p2_vel;
        public float p1_time;
        public float p2_time;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    // JOINING
    public bool joining;
    public GameObject player01;
    public GameObject player02;
    private Movement player01_move;
    private Movement player02_move;
    [SerializeField] private Vector2 dir_player01;
    [SerializeField] private Vector2 dir_player02;

    // SEPARATING JUICE AN STUFF
    [SerializeField] bool separating;
    public float timeToSeparate;
        [SerializeField] private float timePressed;
        [SerializeField] private float currTimePress;
    public float separateScale;
        public float oppScale;
        [SerializeField] private Vector3 savedScale;
        public float restoreScaleUp;

    // SCALE CHECKS
    public GameObject detectBottom;
        public GameObject detectTop;
        public GameObject detectLeft;
        public GameObject detectRight;
    private SurfaceDetect sd_bottom;
        private SurfaceDetect sd_top;
        private SurfaceDetect sd_left;
        private SurfaceDetect sd_right;
    public bool touchBottom;
        public bool touchTop;
        public bool touchLeft;
        public bool touchRight;
    public bool squishVert;
    public bool squishHoriz;

    // Start is called before the first frame update
    void Start()
    {

        name = gameObject.name;

        joining = false;
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();

        player01_move = player01.gameObject.GetComponent<Movement>();
            player02_move = player02.gameObject.GetComponent<Movement>();

        sd_bottom = detectBottom.gameObject.GetComponent<SurfaceDetect>();
            sd_top = detectTop.gameObject.GetComponent<SurfaceDetect>();
            sd_left = detectLeft.gameObject.GetComponent<SurfaceDetect>();
            sd_right = detectRight.gameObject.GetComponent<SurfaceDetect>();

    }

    // Update is called once per frame
    void Update()
    {

        SquishCheck();
        ScaleTo();
        GroundCheck();
        
        if (activated) {

            ScaleMe();

        }

        Movement();

        if (!activated) { Separate(); }
        

    }

    public void ScaleMe() {

        if (!joining) {

            LaunchMe();

        }

        joining = true;
        
        if ((transform.localScale.x + transform.localScale.y) < scaleTarget) {

            Debug.Log("scaling up!");

            if (!squishHoriz) {
                transform.localScale = new Vector2 (transform.localScale.x + scaleUp, transform.localScale.y);
            }

            if (!squishVert) {
                transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y + scaleUp);
            }

        } else {

            activated = false;
            joining = false;
            savedScale = transform.localScale;
            Debug.Log("DONE");

        }

    }

    public void ScaleTo() {

        if (!transform.localScale.Equals(savedScale) && !separating && !joining) {

            Debug.Log("scaling to!");

            var scaleDirX = (savedScale.x - transform.localScale.x) / Mathf.Abs(savedScale.x - transform.localScale.x);
            var scaleDirY = (savedScale.y - transform.localScale.y) / Mathf.Abs(savedScale.y - transform.localScale.y);

            if (((scaleDirX > 0 && !squishHoriz) || scaleDirX < 0) && Mathf.Abs(savedScale.x - transform.localScale.x) > 0.01) {
                transform.localScale = new Vector2 (transform.localScale.x + (restoreScaleUp * scaleDirX), transform.localScale.y);
            }

            if (((scaleDirY > 0 && !squishVert) || scaleDirY < 0) && Mathf.Abs(savedScale.y - transform.localScale.y) > 0.01) {
                transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y + (restoreScaleUp * scaleDirY));
            }

        }

    }

    public void SquishCheck() {

        if (sd_top.activated) {

            touchTop = true;

        } else { touchTop = false; }
        if (sd_bottom.activated) {

            touchBottom = true;

        } else { touchBottom = false; }
        if (sd_right.activated) {

            touchRight = true;

        } else { touchRight = false; }
        if (sd_left.activated) {

            touchLeft = true;

        } else { touchLeft = false; }

        if (touchLeft && touchRight) { squishHoriz = true; }
            else { squishHoriz = false; }
        if (touchBottom && touchTop) { squishVert = true; }
            else { squishVert = false; }

    }

    public void LaunchMe() {

        Debug.Log("LAAAAAUNCH");

        if (p1_time > p2_time) {

            rb.AddForce(p1_vel.normalized * launch);
            Debug.Log(" -- " + p1_vel.normalized * launch);

        } else {

            rb.AddForce(p2_vel.normalized * launch);
            Debug.Log(" -- " + p2_vel.normalized * launch);

        }

    }

    public void Separate() {

        if (((dir_player01.x == -1 * (dir_player02.x) && dir_player01.x != 0) ||
            (dir_player01.y == -1 * (dir_player02.y) && dir_player01.y != 0))) {
            
            if (!separating) { 
                
                timePressed = Time.time; 
                
            }
            
            separating = true;

            currTimePress = Time.time;

            var timeRatio = 1 - (currTimePress - timePressed) / timeToSeparate;

            if (dir_player01.x == -1 * (dir_player02.x) && dir_player01.x != 0) {
                transform.localScale = new Vector3 (transform.localScale.x + (separateScale * timeRatio), transform.localScale.y - (separateScale * timeRatio * oppScale), transform.localScale.z);
            }
            if (dir_player01.y == -1 * (dir_player02.y) && dir_player01.y != 0) {
                transform.localScale = new Vector3 (transform.localScale.x - (separateScale * timeRatio * oppScale), transform.localScale.y + (separateScale * timeRatio), transform.localScale.z);
            }

        } else {

            separating = false;

        }

        if (separating && (currTimePress - timePressed) >= timeToSeparate) {

            Deactivate();

        }

    }

    public void Deactivate() {

        separating = false;

        player01.SetActive(true);
        player01.transform.position = new Vector2 (transform.position.x + dir_player01.x, transform.position.y + dir_player01.y);
        player01_move.MoveInDir(dir_player01);
            

        player02.SetActive(true);
        player02.transform.position = new Vector2 (transform.position.x + dir_player02.x, transform.position.y + dir_player02.y);
        player02_move.MoveInDir(dir_player02);

        transform.localScale = new Vector3 (1f, 1f, 1f);
        gameObject.SetActive(false);

    }

    public void GroundCheck() {

        if (touchBottom && !grounded) {

            grounded = true;
            liftOff = false;

            p1_jump = false;
            p2_jump = false;

        } else if (!touchBottom && !liftOff) {

            liftOff = true;
            StartCoroutine(GroundMe());

        }

    }

    private IEnumerator GroundMe()
    {

        yield return new WaitForSeconds(coyoteTime);
        grounded = false;

    }

    public void Movement() {

        if (Input.GetKey(KeyCode.W)) {

            dir_player01 = new Vector2 (0, 1);

            if (Input.GetKeyDown(KeyCode.W) && grounded && !p1_jump) {

                p1_jump = true;

                rb.velocity = new Vector2 (rb.velocity.x, 0);
                rb.AddForce(new Vector2 (0, 1) * jump);

            }

        } else if (Input.GetKey(KeyCode.A)) {

            Debug.Log("going left 1");
            rb.AddForce(new Vector2 (-1, 0) * speed);
            dir_player01 = new Vector2 (-1, 0);

        } else if (Input.GetKey(KeyCode.D)) {

            rb.AddForce(new Vector2 (1, 0) * speed);
            dir_player01 = new Vector2 (1, 0);

        } else if (Input.GetKey(KeyCode.S)) {

            dir_player01 = new Vector2 (0, -1);

            if (Input.GetKeyDown(KeyCode.S)) {

                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2 (0, -1) * jump);

            }

        } else {

            dir_player01 = Vector2.zero;

        }

        
        if (Input.GetKey(KeyCode.UpArrow)) {

            dir_player02 = new Vector2 (0, 1);
            
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded && !p2_jump) {

                p2_jump = true;

                new Vector2 (rb.velocity.x, 0);
                rb.AddForce(new Vector2 (0, 1) * jump);

            }

        } else if (Input.GetKey(KeyCode.LeftArrow)) {

            Debug.Log("going left 2");
            rb.AddForce(new Vector2 (-1, 0) * speed);
            dir_player02 = new Vector2 (-1, 0);

        } else if (Input.GetKey(KeyCode.RightArrow)) {

            rb.AddForce(new Vector2 (1, 0) * speed);
            dir_player02 = new Vector2 (1, 0);

        } else if (Input.GetKey(KeyCode.DownArrow)) {

            dir_player02 = new Vector2 (0, -1);

            if (Input.GetKeyDown(KeyCode.DownArrow)) {

                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2 (0, -1) * jump);

            }

        } else {

            dir_player02 = Vector2.zero;

        }
        

    }

}
