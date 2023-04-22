using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip successClip;

    [SerializeField] ParticleSystem successParticleSystem;
    [SerializeField] ParticleSystem explosionParticleSystem;


    AudioSource audioSource;

    bool transitioning = false;
    bool collisionsEnabled = true;

    public void Start() {
        audioSource  = GetComponent<AudioSource>();
    }

    public void Update() {
        processDebugKeys();
    }

    private void processDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            loadNextScene();
            return;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            // disable collisions
            collisionsEnabled = !collisionsEnabled;
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (transitioning || !collisionsEnabled) { return; }
    
        GameObject gameObject = collision.gameObject;
        switch (gameObject.tag) {
            case "Safe":
                log("Safe collision with ", gameObject);
                break;
            case "Finish":
                log("Finish collision with ", gameObject);
                StartFinishSequence();
                 break;
            default:
                log("Default collision with ", gameObject);
                StartCrashSequence();
                break;
        }
    }

    private void log(string message, GameObject gameObject) {
        Debug.Log(message + gameObject.name);
    }


    void StartCrashSequence() {
        GetComponent<Movement>().enabled = false;
        Debug.Log("starting crash sequence");

        explosionParticleSystem.Play();
        PlayAudioClip(crashClip);
        transitioning = true;
        Invoke("reloadScene", levelLoadDelay);
    }

    private void PlayAudioClip(AudioClip audioClip) {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    void StartFinishSequence() {
        GetComponent<Movement>().enabled = false;
        Debug.Log("starting Finish sequence");

        successParticleSystem.Play();
        transitioning = true;
        PlayAudioClip(successClip);
        Invoke("loadNextScene", levelLoadDelay);
    }

    private void reloadScene() {
        Debug.Log("Reloading Scene");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
    private void loadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }


}
