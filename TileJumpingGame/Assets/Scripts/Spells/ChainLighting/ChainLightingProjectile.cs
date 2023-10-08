using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChainLightingProjectile : MonoBehaviour
{
    public float m_DistanceThreshold;
    public GameObject m_ChainLightingVFX;

    public void InitiateChainLighting(Vector2 playerPos)
    {
        //Gather all objects that can be attached to the chain lighting
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (enemies.Length == 0 && boss == null)
        {
            //Destroy(gameObject);
            return;
        }

        List<GameObject> attachPoints = new List<GameObject>(enemies);
        attachPoints.Add(boss);

        Vector2 startPoint = playerPos;
        while(true)
        {
            if(attachPoints.Count <= 0)
            {
                break;
            }
            //Sort the list
            attachPoints.Sort((t1, t2) => {
                float dist1 = Vector2.Distance(t1.transform.position, startPoint);
                float dist2 = Vector2.Distance(t2.transform.position, startPoint);
                return dist1.CompareTo(dist2);
            });

            GameObject closestEnemy = attachPoints[0];

            if(Vector2.Distance(closestEnemy.transform.position, startPoint) > m_DistanceThreshold)
            {
                //The closest enemy is too far away so chain lighting won't work!
                break;
            }

            LightingProjectile lightingProj = Instantiate(m_ChainLightingVFX, startPoint, Quaternion.identity).GetComponent<LightingProjectile>();
            lightingProj.Init(closestEnemy.transform.position);

            startPoint = closestEnemy.transform.position;
            attachPoints.RemoveAt(0);
        }


    }
}
