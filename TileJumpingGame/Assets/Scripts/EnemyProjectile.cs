using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public bool RecentlyTeleported { get; set; }

    private float Speed { get; set; }
    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(float speed, Vector2 direction)
    {
        Speed = speed;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * Speed;
        var rotation = calculateRotation(direction);

        transform.Rotate(new Vector3(0, 0, rotation));
    }

    public void Teleport(Vector3 position, Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * Speed;
        transform.position = position;
        var rotation = calculateRotation(direction);

        transform.Rotate(new Vector3(0, 0, rotation));
        RecentlyTeleported = true;
        StartCoroutine(ClearRecentlyTeleported(0.5f));
    }

    private IEnumerator ClearRecentlyTeleported(float delay)
    {
        yield return new WaitForSeconds(delay);
        RecentlyTeleported = false;
    }
    

    private float calculateRotation(Vector2 direction)
    {
        var angle = Vector2.SignedAngle(direction, Vector2.up);
        return angle;
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
