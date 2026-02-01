using UnityEngine;
using System.Collections.Generic;

public class PlayerProximity : MonoBehaviour
{
    public float exchangeRadius = 1f;    // Distance where exchange is possible
    public float visibilityRadius = 2f;    // Distance where visibility starts
    public string playerTag = "Player";
    GameObject playerObj;
    bool hasWish = false;
    void Start()
    {
        playerObj = PlayerController.Instance.gameObject;
        hasWish = Random.Range(0, 2) == 0;
    }

    private void FixedUpdate()
    {
        if (hasWish)
        {
            float distance = Vector2.Distance(transform.position, playerObj.transform.position);
            float alpha = Mathf.Clamp01(visibilityRadius - distance) / (visibilityRadius - exchangeRadius);
            transform.Find("EnemyBubble").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            transform.Find("EnemyBubble/EnemyWish").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        } else {
            transform.Find("EnemyBubble").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            transform.Find("EnemyBubble/EnemyWish").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }
}