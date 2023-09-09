using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    private bool m_IsThisEnemyLaser;
    
    public void SetIsEnemyLaser(bool value)
    {
        m_IsThisEnemyLaser = value;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!m_IsThisEnemyLaser && col.tag == "Boss")
        {
            col.GetComponent<Boss>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (!m_IsThisEnemyLaser && col.tag == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
                Destroy(gameObject);
            }
        }

        if (m_IsThisEnemyLaser && col.tag == "Player")
        {
            col.GetComponent<Player>().TakeDamage(1);
        }

        if (col.tag == "Boulder")
        {
            col.GetComponent<Boulder>().GetTile().GetComponent<BoulderTile>().TakeDamage(10);
        }
    }
}
