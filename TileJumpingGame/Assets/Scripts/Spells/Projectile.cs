using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
            col.GetComponent<Enemy>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
