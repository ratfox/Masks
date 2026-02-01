using UnityEngine;
using System.Collections.Generic;

public class RandomObject : MonoBehaviour
{
    // Drag your object sprites into this list in the Inspector
    public List<Sprite> objectOptions; 
    
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (objectOptions.Count > 0)
        {
            // Pick a random number between 0 and the end of the list
            int randomIndex = Random.Range(0, objectOptions.Count);
            
            // Assign that sprite to the renderer
            sr.sprite = objectOptions[randomIndex];
        }
        else
        {
            Debug.LogWarning("Hey! You forgot to add object sprites to the list on " + gameObject.name);
        }
    }
}