using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustMagnitude = 500f;

    [SerializeField] float rotationMagnitude = 100f;
    private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
    }

    private void processInput() {
        processThrust();

        processRotation();
    }

    private void processRotation() {

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            return;
        }

        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("A pressed - Rotate left");
            applyRotation(Vector3.forward);
            return;
        }

        if (Input.GetKey(KeyCode.D)) {
            Debug.Log("A pressed - Rotate Right");
            applyRotation(Vector3.back);
            return;
        }
    }

    private void applyRotation(Vector3 rotation) {
        rigidBody.freezeRotation = true;
        transform.Rotate(rotation * rotationMagnitude * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

    private void processThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log("Space pressed - Thrust");
            rigidBody.AddRelativeForce(Vector3.up*thrustMagnitude*Time.deltaTime);
            return;
        }
    }
}
