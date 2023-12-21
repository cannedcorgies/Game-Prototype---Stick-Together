using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class TwoTargetsCamera : MonoBehaviour
{

    public List<GameObject> targets;
    
    public Camera cam;
    [SerializeField] private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        cam = Camera.main;

        targetPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z
        );

    }

    // Update is called once per frame
    void Update()
    {
        
        targetPosition.x = targetPosition.y = 0f;

        for(int i = 0; i < targets.Count; i++) {

            var target = targets[i];

            if (target.gameObject.activeSelf) {

                targetPosition.x += target.transform.position.x;
                targetPosition.y += target.transform.position.y;

            }

        }

        targetPosition.x = targetPosition.x / targets.Count;
        targetPosition.y = targetPosition.y / targets.Count;

        cam.transform.position = Vector3.Lerp(

            cam.transform.position,
            targetPosition, // + offset;
            Time.deltaTime

        );

        var positionsX = targets.Select(t => t.transform.position.x).ToArray();
        var positionsY = targets.Select(t => t.transform.position.y).ToArray();
        var minX = Mathf.Min(positionsX);
        var maxX = Mathf.Max(positionsX);
        var minY = Mathf.Min(positionsY);
        var maxY = Mathf.Max(positionsY);
        var sizeX = Mathf.Abs(maxX - minX);
        var sizeY = Mathf.Abs(maxY - minY);
        var diagonal = Mathf.Sqrt((sizeX * sizeX) + (sizeY * sizeY));

        targetPosition.z = -diagonal;

    }
}
