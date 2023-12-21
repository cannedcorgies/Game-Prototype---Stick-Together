using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{

    public bool activated;
    public GameObject target;

    [SerializeField] private Vector2 startPos;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (activated) {

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed);

        } else {

            transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed);

        }
        
    }
}
