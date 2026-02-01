using UnityEngine;
using System.Collections.Generic;

public class RandomObject : MonoBehaviour
{
    // Drag your object sprites into this list in the Inspector
    
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (GameManager.Instance.objectOptions.Count > 0)
        {
            // Pick a random number between 0 and the end of the list
            int randomIndex = Random.Range(0, GameManager.Instance.objectOptions.Count);
            
            // Assign that sprite to the renderer
            sr.sprite = GameManager.Instance.objectOptions[randomIndex];
        }
        else
        {
            Debug.LogWarning("Hey! You forgot to add object sprites to the list on " + gameObject.name);
        }
    }
}