using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq; // Required for the search logic

public class UIGauge : MonoBehaviour, IResettable
{
    public static UIGauge Instance; // Singleton so enemies can find it
    private Image fillImage;
    public bool isGameOver = false;
    public float currentFear = 0.0f;
    public GameObject gameOverPanel;
    void Awake()
    {
        // If an instance already exists, destroy this one to avoid duplicates
        if (Instance == null)
        {
            Instance = this;
            fillImage = GetComponent<Image>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetGaugeValue(float currentDelta)
    {
        currentFear += currentDelta * 0.01f;
        currentFear = Mathf.Clamp01(currentFear);        
        fillImage.fillAmount = currentFear;        
        if (currentFear >= 1.0f) {
            TriggerGameOver();
        }
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
        isGameOver = false;
        var resettables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                            .OfType<IResettable>();

        // 2. Loop through each one and tell it to reset
        foreach (IResettable item in resettables)
        {
            item.ResetObject();
        }
        SceneManager.LoadScene("startscene");
        Time.timeScale = 1f; // Reset time
        gameOverPanel.SetActive(false);
    }

    public void ResetObject()
    {
        currentFear = 0.0f;
        fillImage.fillAmount = 0.0f;
    }
}