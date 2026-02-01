using System;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public float victoryRadius = 4f;    // Distance where visibility starts
    public string playerTag = "Player";
    GameManager gameManager;
    GameObject playerObj;
    void Start()
    {
        playerObj = PlayerController.Instance.gameObject;
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        transform.Find("GoalWish").GetComponent<SpriteRenderer>().sprite = gameManager.goalObject;
        float distance = Vector2.Distance(transform.position, playerObj.transform.position);
        if (distance < victoryRadius) {
            SpriteRenderer playerHandsSR = playerObj.transform.Find("PlayerHands").GetComponent<SpriteRenderer>();
            SpriteRenderer wish = transform.Find("GoalWish").GetComponent<SpriteRenderer>();
            if (playerHandsSR.sprite.name == wish.sprite.name)
            {
                GameManager.Instance.TriggerVictory();
            }
        }
    }
}