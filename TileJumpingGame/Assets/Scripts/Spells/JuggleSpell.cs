using UnityEngine;

public class JuggleSpell : Spell
{
    public GameObject BounceObj;
    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 2);
        m_ID = (int)GameVariables.SPELL_NAMES.Juggle;
    }

    public override void CastSpell()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();

        var currentTile = player.CurrentTile;

        var board = GameObject.Find("Board").GetComponent<GridTileBoard>();

        Instantiate(BounceObj).GetComponent<Bounce>().Init(board.GetRandomTile(), player);


        base.CastSpell();
    }
}
