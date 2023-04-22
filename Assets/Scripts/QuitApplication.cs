using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Quitting application Update");
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Quitting application");
            Application.Quit();
        }
    }
}
