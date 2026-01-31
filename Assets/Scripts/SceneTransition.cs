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
            PlayerController.Instance.shouldSetDestination = true;
            PlayerController.Instance.lastTransitionSide = entranceSide;
            
            // Start the fade sequence
            StartCoroutine(SceneFader.Instance.FadeOutAndLoad(sceneToLoad));
        }
    }
}
    