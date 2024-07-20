using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip sucessSound;

    AudioSource audioSource;

    //If this state is true rockect would not perform its usual actions
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return; //If the scene is transionting it won't perform the the switch statement and would stop on collision enetr method
        }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                StartSucessSquence();
                break;
            default:
                Debug.Log("unFriendly");
                StartCrashSequence();
                break; 
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);

    }

    void StartSucessSquence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(sucessSound);
        
    }
    //To load the scene we are currently on when the rocket crashes to an unfriendly object 
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //To load the next scene when the rocket landa on the landing point/pad.
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

}
