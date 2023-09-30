using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBoss : Boss
{
    public PortalHandler PortalHandler;
    // Start is called before the first frame update
    void Start()
    {
        PortalHandler = GameObject.Find("PortalHandler").GetComponent<PortalHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackInterval)
        {
            
            attackHandler.DamageRandomRow(damage, attackDelay);
        }
    }

    private void SpawnPortals()
    {
        //PortalHandler.CreatePortalPair();
    }
}
