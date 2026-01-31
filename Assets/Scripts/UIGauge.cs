using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour, IResettable
{
    public static UIGauge Instance; // Singleton so enemies can find it
    private Image fillImage;
    public float currentFear = 0.0f;
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
            GameManager.Instance.TriggerGameOver();
        }
    }

    public void ResetObject()
    {
        currentFear = 0.0f;
        fillImage.fillAmount = 0.0f;
    }
}