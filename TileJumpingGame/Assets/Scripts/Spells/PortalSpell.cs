using UnityEngine;

public class PortalSpell : Spell
{
    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 2);
        m_ID = (int)GameVariables.SPELL_NAMES.Portal;
    }

    public override void CastSpell()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();

        var currentTile = player.CurrentTile;

        var portalHandler = GameObject.Find("PortalHandler").GetComponent<PortalHandler>();
        if (portalHandler)
        {
            portalHandler.SpawnPortal(currentTile);
        }

        base.CastSpell();
    }
}
