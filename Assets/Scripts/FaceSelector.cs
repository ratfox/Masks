using UnityEngine;
using System.Collections.Generic;

public class FaceSelector : MonoBehaviour, IResettable
{
    public List<Sprite> faceOptions; 
    private SpriteRenderer sr;

    void Start()
    {
        Transform faceTransform = transform.Find("PlayerFace");
        sr = faceTransform.GetComponent<SpriteRenderer>();
        // Default to the first face in the list
        if (faceOptions.Count > 0) sr.sprite = faceOptions[0];
        sr.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        // Check for keys 1 through 6
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeFace(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeFace(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeFace(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeFace(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeFace(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeFace(5);
    }

    void ChangeFace(int index)
    {
        // Safety check: make sure the list has a sprite at that index
        if (index < faceOptions.Count && faceOptions[index] != null)
        {
            sr.color = new Color(1, 1, 1, 1);
            sr.sprite = faceOptions[index];
        }
    }

    public void ResetObject()
    {
        sr.color = new Color(1, 1, 1, 0);
    }
}