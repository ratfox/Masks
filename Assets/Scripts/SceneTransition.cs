using UnityEngine;
using UnityEngine.SceneManagement; // Required for switching scenes

public class SceneTransition : MonoBehaviour
{
    [Header("Configuration")]
public string sceneToLoad;
    
    // Create an enum to pick the entrance side in the Inspector
    public enum TransitionSide { Left, Right, Top, Bottom }
    public TransitionSide entranceSide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Store the side we are coming from in a static variable 
            // so the player knows where to go in the next scene.
            PlayerController.Instance.shouldSetDestination = true;
            PlayerController.Instance.lastTransitionSide = entranceSide;
            SceneManager.LoadScene(sceneToLoad);
        }
    }}