using System;
using UnityEngine;

public class ProximityDetector : MonoBehaviour, IResettable
{
    public float detectionRadius = 10f; // Maximum distance to start filling the gauge
    public float dangerRadius = 2f;    // Distance where gauge is 100% full
    public float exchangeRadius = 1f;    // Distance where exchange is possible
    public string enemyTag = "Enemy";
    public float proximity = 0.0f;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        SpriteRenderer playerFaceSR = transform.Find("PlayerFace").GetComponent<SpriteRenderer>();
        SpriteRenderer enemyFaceSR = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                enemyFaceSR = enemy.transform.Find("EnemyFace").GetComponent<SpriteRenderer>();
            }
        }

        // Calculate proximity amount: 1 at dangerRadius, 0 at detectionRadius
        proximity = 1 - ((closestDistance - dangerRadius) / (detectionRadius - dangerRadius));
        proximity = Mathf.Clamp(proximity, -0.5f, 1.0f);

        if (enemyFaceSR != null && playerFaceSR.color.a == 1 &&
            playerFaceSR.sprite.name == enemyFaceSR.sprite.name)
        {
            proximity = -0.5f;
        }

        if (UIGauge.Instance != null)   
        {
            UIGauge.Instance.SetGaugeValue(proximity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            float closestDistance = Mathf.Infinity;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            SpriteRenderer enemyHandsSR = null;
            SpriteRenderer enemyWishSR = null;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    enemyHandsSR = enemy.transform.Find("EnemyHands").GetComponent<SpriteRenderer>();
                    enemyWishSR = enemy.transform.Find("EnemyBubble/EnemyWish").GetComponent<SpriteRenderer>();
                }
            }
            if (closestDistance > exchangeRadius)
            {
                return;
            }
            SpriteRenderer playerHandsSR = transform.Find("PlayerHands").GetComponent<SpriteRenderer>();
            if (enemyWishSR.color.a > 0.5 && 
                enemyWishSR.sprite.name != playerHandsSR.sprite.name)
            {
                return;  // Enemy wants other object
            }

            if (enemyHandsSR != null)
            {
                Sprite playerObject = playerHandsSR.sprite;
                bool playerHasObject = playerHandsSR.color.a == 1;
                if (enemyHandsSR.color.a == 1)  // Enemy has object
                {
                    playerHandsSR.sprite = enemyHandsSR.sprite;
                    playerHandsSR.color = new Color(1, 1, 1, 1);                    
                } else
                {
                    playerHandsSR.color = new Color(1, 1, 1, 0);                    
                }
                if (playerHasObject)
                {
                    enemyHandsSR.sprite = playerObject;
                    enemyHandsSR.color = new Color(1, 1, 1, 1);
                } else
                {
                    enemyHandsSR.color = new Color(1, 1, 1, 0);
                }
            }
        }
        
    }

    // Visual aid in the Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }

    public void ResetObject()
    {
        proximity = 0.0f;
        transform.Find("PlayerHands").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

}