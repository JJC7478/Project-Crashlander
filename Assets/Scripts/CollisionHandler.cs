using UnityEngine.SceneManagement;
using UnityEngine;


public class CollisionHandler : MonoBehaviour
{
    // Script for handling different collision scenarios
    [SerializeField] float loadLevelDelay = 1f;

    [SerializeField] AudioClip levelSuccess;

    [SerializeField] AudioClip levelFail;

    AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
      switch(other.gameObject.tag) // switch depending on the tag of the object 
      {
          case "Friendly":
          Debug.Log("Start"); // print "Start" in console whenever bumping into object w/ Friendly tag
          break;

          case "Finish":
          Debug.Log("Reached Finish"); // print "Reached Finish" in console whenever bumping into object w/ Finish tag
          StartAdvanceLevelSequence(); // advances to the next level
          if(!audioSource.isPlaying)
          {
              audioSource.PlayOneShot(levelSuccess);
          }
          break;

          default:
          Debug.Log("Bumped into obstacle, restarting level"); // print "Bumped into obstacle" in console whenever bumping into other objects
          StartCrashSequence();  // Reloads level with 1 second delay
          if(!audioSource.isPlaying)
          {
              audioSource.PlayOneShot(levelFail);
          }
          break;


      }
    }  
      
      void StartCrashSequence()
      {
          // todo add SFX upon crash
          // todo add particle effect upon crash
          GetComponent<Movement>().enabled = false;
          Invoke (nameof(ReloadLevel), loadLevelDelay);
      }

      void StartAdvanceLevelSequence()
      {
          GetComponent<Movement>().enabled = false;
          Invoke (nameof(LoadNextLevel), loadLevelDelay);
      }
      
      void ReloadLevel()
      {
         // SceneManager.LoadScene("Sandbox"); // can put in build index "0" or string for level "Sandbox"
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // variable that stores the build index of the current scene
         SceneManager.LoadScene(currentSceneIndex); // returns the build index of the scene/level we're in
      }

      void LoadNextLevel()
      {
          int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // variable that stores the build index of the current scene
          int nextSceneIndex = currentSceneIndex + 1;

          if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
          {
              nextSceneIndex = 0;
          }

          SceneManager.LoadScene(nextSceneIndex);
      }
    
}