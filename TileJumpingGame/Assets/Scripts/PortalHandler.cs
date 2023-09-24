using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    public GameObject PortalObject;
    public Queue<Portal> Portals;
    public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        Portals = new Queue<Portal>();
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPortal(Tile currentTile)
    {
        if (Portals.Count == 2)
        {
            var oldestPortal = Portals.Dequeue();
            Destroy(oldestPortal.gameObject);
        }

        Vector3 pos = currentTile.transform.position + new Vector3(0, 0, -0.5f);
        var newPortal = Instantiate(PortalObject, pos, Quaternion.identity).GetComponent<Portal>();

        newPortal.Init(this, currentTile);

        Portals.Enqueue(newPortal);
        currentTile.AddHazard(newPortal);
    }

    public void EnterPortal(Portal entered)
    {
        var nextPortal = Portals.FirstOrDefault(p => p != entered);
        if(nextPortal != null)
        {
            Player.EnterTile(nextPortal.Tile);
        }

    }
}
