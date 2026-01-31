using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq; // Required for the search logic

public class GameManager : MonoBehaviour
{
    // This is the "static" reference that other scripts use to find this one
    public static GameManager Instance { get; private set; }
    public bool isGameOver = false;
    public GameObject gameOverPanel;

    void Awake()
    {
        // 1. Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If one exists and it's not me, destroy myself
            Destroy(gameObject);
            return;
        }

        // 2. Set this object as the unique Instance
        Instance = this;

        // 3. (Optional) Make this object persist between scene loads
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerGameOver()
        {
            if (isGameOver) return; // Prevent multiple triggers

            isGameOver = true;
            gameOverPanel.SetActive(true);
            
            // Optional: Freeze the game time
            Time.timeScale = 0f; 
        }

    public void RestartGame()
    {
        StartCoroutine(RestartRoutine());
    }

private IEnumerator RestartRoutine()
{
    SceneFader.Instance.FadeOut();

    // 2. Wait for the duration of the fade
    // Replace '1f' with the actual time your animation takes
    yield return new WaitForSecondsRealtime(1f); 

    // 3. NOW Reset the UI and Faces while the screen is black
        isGameOver = false;
        var resettables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                            .OfType<IResettable>();

        // 2. Loop through each one and tell it to reset
        foreach (IResettable item in resettables)
        {
            item.ResetObject();
        }
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Reset time
        SceneManager.LoadScene("startscene");
    }
}