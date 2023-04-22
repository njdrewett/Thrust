using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustMagnitude = 500f;

    [SerializeField] float rotationMagnitude = 100f;

    [SerializeField] AudioClip enginesClip;


    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [SerializeField] ParticleSystem leftBoosterParticleSystem;
    [SerializeField] ParticleSystem rightBoosterParticleSystem;
    [SerializeField] ParticleSystem leftMainBoosterParticleSystem;
    [SerializeField] ParticleSystem rightMainBoosterParticleSystem;

    bool isAlive;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            ProcessLeft();
            return;
        }

        if (Input.GetKey(KeyCode.D)) {
            ProcessRight();
            return;
        }
        StopRotationThrusts();
    }

    private void StopRotationThrusts() {
        rightBoosterParticleSystem.Stop();
        leftBoosterParticleSystem.Stop();
    }

    private void ProcessRight() {
        Debug.Log("D pressed - Rotate Right");
        if (!leftBoosterParticleSystem.isPlaying) {
            leftBoosterParticleSystem.Play();
        }
        applyRotation(Vector3.back);
    }

    private void ProcessLeft() {
        Debug.Log("A pressed - Rotate left");
        if (!rightBoosterParticleSystem.isPlaying) {
            rightBoosterParticleSystem.Play();
        }

        applyRotation(Vector3.forward);
    }

    private void applyRotation(Vector3 rotation) {
        rigidBody.freezeRotation = true;
        transform.Rotate(rotation * rotationMagnitude * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

    private void processThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            PlayMainThrust();
            return;
        }
        audioSource.Stop();
        leftMainBoosterParticleSystem.Stop();
        rightMainBoosterParticleSystem.Stop();
    }

    private void PlayMainThrust() {
        Debug.Log("Space pressed - Thrust");
        rigidBody.AddRelativeForce(Vector3.up * thrustMagnitude * Time.deltaTime);
        audioSource.clip = enginesClip;
        if (!leftMainBoosterParticleSystem.isPlaying) {
            leftMainBoosterParticleSystem.Play();
        }
        if (!rightMainBoosterParticleSystem.isPlaying) {
            rightMainBoosterParticleSystem.Play();
        }

        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}
