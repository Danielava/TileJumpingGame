using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoot : MonoBehaviour
{
    public int m_HP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        m_HP -= damage;
        if(damage <= 0)
        {
            //TODO: Play some animation before killing enemy
            Destroy(gameObject);
        }
    }
}
