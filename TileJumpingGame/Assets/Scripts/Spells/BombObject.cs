using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    private Tile m_CurrentTile; //The tile this bomb is placed on.
    private float m_LifeTime = 3.0f;

    public Sprite m_ExplosionSprite;

    //Init should just initialize the m_CurrentTile object and run the bomb timer.
    public void Init(Tile playerTile)
    {
        m_CurrentTile = playerTile;
        StartCoroutine(RunBombTimer());
    }

    IEnumerator RunBombTimer()
    {
        yield return new WaitForSeconds(m_LifeTime);
        Explode();
    }

    public void Explode()
    {
        //TODO: Trigger the explosion animation
        //TODO: Run some function in AttackHandler that will hurt anyone standing in the area of the bomb (or we could make it collision based)
        GameObject.FindWithTag("Boss").GetComponent<Boss>().TakeDamage(20); //Temporary hurt boss once bomb explodes
        Destroy(gameObject);
    }
}
