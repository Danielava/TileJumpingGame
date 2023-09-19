using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintExplosion : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

        StartCoroutine(CreateExplosion(0.3f));
        Destroy(gameObject, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boss")
        {
            col.GetComponent<Boss>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (col.tag == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
                Destroy(gameObject);
            }
        }
    }


    private IEnumerator CreateExplosion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
