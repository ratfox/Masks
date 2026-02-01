using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    private bool isInputDetected = false;

    void Update()
    {
        // Check if any key, mouse button, or controller button is pressed
        if (!isInputDetected && Input.anyKeyDown)
        {
            isInputDetected = true;
            DismissTitleScreen();
        }
    }

    void DismissTitleScreen()
    {
        gameObject.SetActive(false);
    }
}