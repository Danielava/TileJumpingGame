using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Hazard
{
    public PortalHandler PortalHandler { get; private set; }
    public Tile Tile { get; private set; }

    public Direction Direction { get; set; } = Direction.NONE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(PortalHandler portalHandler, Tile tile, Direction direction = Direction.NONE)
    {
        PortalHandler = portalHandler;
        Tile = tile;
        Direction = direction;
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
        if(collision.gameObject.tag == "TeleportableProjectile")
        {
            var portalBullet = collision.GetComponent<TeleportableProjectile>();

            if (!portalBullet.RecentlyTeleported)
                PortalHandler.Teleport(portalBullet, this);
        }
    }
}
