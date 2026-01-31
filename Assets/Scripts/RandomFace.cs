using UnityEngine;
using System.Collections.Generic;

public class RandomFace : MonoBehaviour
{
    // Drag your face sprites into this list in the Inspector
    public List<Sprite> faceOptions; 
    
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (faceOptions.Count > 0)
        {
            // Pick a random number between 0 and the end of the list
            int randomIndex = Random.Range(0, faceOptions.Count);
            
            // Assign that sprite to the renderer
            sr.sprite = faceOptions[randomIndex];
        }
        else
        {
            Debug.LogWarning("Hey! You forgot to add face sprites to the list on " + gameObject.name);
        }
    }
}