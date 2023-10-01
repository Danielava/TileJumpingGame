using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Hazard
{
    public PortalHandler PortalHandler { get; private set; }
    public Tile Tile { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(PortalHandler portalHandler, Tile tile)
    {
        PortalHandler = portalHandler;
        Tile = tile;
    }

    public void OnDestroy()
    {
        Tile.RemoveHazard(this);
    }

    public override void TriggerHazard()
    {
        PortalHandler.EnterPortal(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hit");
        if(collision.gameObject.tag == "PortalBullet")
        {
            var portalBullet = collision.GetComponent<EnemyProjectile>();

            if (!portalBullet.RecentlyTeleported)
                PortalHandler.Teleport(portalBullet, this);
        }
    }
}
