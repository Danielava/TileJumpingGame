using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class PortalPair
{
    public Portal[] Portals { get; set; } = new Portal[2];
    public bool PlayerPortal { get; set; }

    public int LatestIndex { get; set; }
    public PortalPair(Portal portal1, Portal portal2)
    {
        Portals[0] = portal1;
        Portals[1] = portal2;
    }
    
}

public class PortalHandler : MonoBehaviour
{
    public GameObject PortalObject;
    public Player Player;

    public List<PortalPair> PortalPairs;

    public Color[] PortalColors = new Color[]
    {
        new Color(1, 1, 1),
        new Color(1, 1, 1),
        new Color(1, 1, 1),
        new Color(1, 1, 1),
        new Color(1, 1, 1),
        new Color(1, 1, 1),
    };

    // Start is called before the first frame update
    void Start()
    {
        PortalPairs = new List<PortalPair>();
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPortal(Tile currentTile)
    {
        Vector3 pos = currentTile.transform.position + new Vector3(0, 0, -0.5f);
        var newPortal = Instantiate(PortalObject, pos, Quaternion.identity).GetComponent<Portal>();

        newPortal.Init(this, currentTile);

        var portalPair = PortalPairs.FirstOrDefault(x => x.PlayerPortal);
        if (portalPair != null)
        {
            if(portalPair.Portals[1] == null)
            {
                portalPair.Portals[1] = newPortal;
            } else
            {
                var oldestPortal = portalPair.Portals[portalPair.LatestIndex % 2];
                Destroy(oldestPortal.gameObject);

                portalPair.Portals[portalPair.LatestIndex % 2] = newPortal;
                portalPair.LatestIndex += 1;
            }
        } else
        {
            PortalPairs.Add(new PortalPair(newPortal, null) { PlayerPortal = true });
        }

        currentTile.AddHazard(newPortal);
    }

    public void EnterPortal(Portal entered)
    {
        var nextPortal = PortalPairs.First(p => p.Portals.Any(x => x == entered)).Portals.FirstOrDefault(p => p != entered);
        if(nextPortal != null)
        {
            Player.EnterTile(nextPortal.Tile);
        }
    }

    public void CreatePortalPair(Tile tile1, Tile tile2, Direction direction1, Direction direction2)
    {
        var color = PortalColors[PortalPairs.Count];


        Vector3 pos1 = tile1.transform.position + new Vector3(0, 0, -0.5f);
        var firstPortal = Instantiate(PortalObject, pos1, Quaternion.identity).GetComponent<Portal>();

        firstPortal.GetComponent<SpriteRenderer>().color = color;
        firstPortal.GetComponent<Portal>().Init(this, tile1);

        Vector3 pos2 = tile2.transform.position + new Vector3(0, 0, -0.5f);
        var secondPortal = Instantiate(PortalObject, pos2, Quaternion.identity).GetComponent<Portal>();

        secondPortal.GetComponent<SpriteRenderer>().color = color;
        secondPortal.GetComponent<Portal>().Init(this, tile2);

        PortalPairs.Add(new PortalPair(firstPortal, secondPortal) { PlayerPortal = false });
    }
}
