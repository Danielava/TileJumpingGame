using UnityEngine;

public enum BounceState
{
    ToField,
    ToBoss
}

public class Bounce : MonoBehaviour
{
    public BounceState state = BounceState.ToField;
    private Player Player;

    private Tile targetTile;
    private float t = 0;

    public Vector3[] Positions = new Vector3[3];

    private Boss Boss;

    public float BounceHeight = 5f;

    public float Speed = 0.3f;

    public GameObject Mark;
    private GameObject mark;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * Speed;
        gameObject.transform.position = CalculatePosition(Positions[0], Positions[1], Positions[2], t);
        if(t >= 1)
        {
            if(state == BounceState.ToField)
            {
                Land();
            } else
            {
                HitBoss();
            }
            t = 0;
        }
    }

    public void Init(Tile targetTile, Player player)
    {
        transform.position = player.transform.position;

        Player = player;

        MoveToTile(targetTile);
    }

    public void InitMark()
    {
        mark = Instantiate(Mark);
        mark.transform.position = Positions[2];
    }

    private void LaunchTowardsBoss()
    {
        state = BounceState.ToBoss;
        Positions[0] = Player.transform.position;
        Positions[2] = new Vector3(Boss.transform.position.x, Boss.transform.position.y);
        Positions[1] = (Positions[0] + Positions[2]) / 2;
        Speed = 2f;

    }

    private void HitBoss()
    {
        Boss.TakeDamage(10);

        MoveToTile(GameObject.Find("Board").GetComponent<GridTileBoard>().GetRandomTile());
    }

    private void MoveToTile(Tile tile)
    {
        targetTile = tile;
        state = BounceState.ToField;

        Positions[0] = transform.position;
        Positions[2] = new Vector3(tile.transform.position.x, tile.transform.position.y);
        Positions[1] = (Positions[0] + Positions[2]) / 2 + new Vector3(0, BounceHeight);
        Speed = 0.33f;
        InitMark();
    }

    private Vector3 CalculatePosition(Vector3 start, Vector3 middle, Vector3 end, float t)
    {
        return ((1 - t) * (1 - t) * start) + (2 * (1 - t) *  t * middle) + (t * t * end); 
    }

    private void Land()
    {
        if(Player.CurrentTile == targetTile)
        {
            LaunchTowardsBoss();
        } else
        {
            Destroy(gameObject);
        }
        Destroy(mark);
        mark = null;
    }
}
