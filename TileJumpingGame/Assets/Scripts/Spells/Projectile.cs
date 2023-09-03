using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 8.0f); //Automatically destroy the object after 8 seconds
    }

    public void Init(float speed, Vector2 direction, float rotation)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
        transform.Rotate(new Vector3(0, 0, rotation));
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
}
