using System;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    public float exchangeRadius = 1f;    // Distance where exchange is possible
    public float visibilityRadius = 2f;    // Distance where visibility starts
    public string playerTag = "Player";
    GameObject playerObj;
    void Start()
    {
        playerObj = PlayerController.Instance.gameObject;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, playerObj.transform.position);
        float alpha = Mathf.Clamp01(visibilityRadius - distance) / (visibilityRadius - exchangeRadius);
        transform.Find("EnemyBubble").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        transform.Find("EnemyBubble/EnemyWish").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
    }
}