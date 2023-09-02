using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private Sprite m_ExplosionSprite;
    private BombObject m_RootBombObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(float lifetime, BombObject bombParent)
    {
        m_ExplosionSprite = gameObject.GetComponent<Sprite>();
        Destroy(gameObject, lifetime);
        m_RootBombObject = bombParent;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boss")
        {
            col.GetComponent<Boss>().TakeDamage(10);
        }

        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(10);
        }

        if(col.tag == "Bomb")
        {
            col.GetComponent<BombObject>().Explode();
        }
    }
}
