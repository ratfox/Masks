using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    private Image fillImage;

    void Awake()
    {
        fillImage = GetComponent<Image>();
    }

    public void SetGaugeValue(float current)
    {
        fillImage.fillAmount = current;
    }
}