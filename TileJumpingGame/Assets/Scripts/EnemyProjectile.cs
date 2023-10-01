using UnityEngine;

public class EnemyProjectile : TeleportableProjectile
{
    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
