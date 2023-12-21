using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelQuota : MonoBehaviour
{

    public bool activated;

    public string nextScene;
    public int activationCount;

    // Start is called before the first frame update
    void Start()
    {
        
        activated = false;
        activationCount = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (activated) {

            SceneManager.LoadScene(nextScene);
            
        }
        
        if (activationCount >= 2) {

            activated = true;

        }

    }
}
