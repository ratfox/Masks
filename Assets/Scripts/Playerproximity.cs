using System;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    public float exchangeRadius = 1f;    // Distance where exchange is possible
    public string playerTag = "Player";
    GameObject playerObj;
    void Start()
    {
        playerObj = PlayerController.Instance.gameObject;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, playerObj.transform.position);
        if (distance < exchangeRadius)
        {
            transform.Find("EnemyBubble").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        } else
        {
            transform.Find("EnemyBubble").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }
}