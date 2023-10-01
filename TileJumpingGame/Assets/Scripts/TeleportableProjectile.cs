using System.Collections;
using UnityEngine;

public class TeleportableProjectile : MonoBehaviour
{
    public bool RecentlyTeleported { get; set; }
    private float Speed { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
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
        var rotation = calculateRotation(direction);

        //transform.rotation.Set(0, 0, 0, 0);
        //transform.Rotate(new Vector3(0, 0, rotation));
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        Teleport(position);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        RecentlyTeleported = true;
        StartCoroutine(ClearRecentlyTeleported(0.2f));
    }

    private IEnumerator ClearRecentlyTeleported(float delay)
    {
        yield return new WaitForSeconds(delay);
        RecentlyTeleported = false;
    }

    private float calculateRotation(Vector2 direction)
    {
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        return angle;
    }

}
