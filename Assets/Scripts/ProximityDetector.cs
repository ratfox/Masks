using System;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public float detectionRadius = 10f; // Maximum distance to start filling the gauge
    public float dangerRadius = 2f;    // Distance where gauge is 100% full
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

        if (enemyFaceSR != null &&
            playerFaceSR.sprite.name == enemyFaceSR.sprite.name)
        {
            proximity = -0.5f;
        }

        if (UIGauge.Instance != null)   
        {
            UIGauge.Instance.SetGaugeValue(proximity);
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

     void ResetObject()
    {
        proximity = 0.0f;
    }

}