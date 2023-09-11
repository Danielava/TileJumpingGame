using UnityEngine;

public class LaserSpell : ChanneledSpell
{
    public Laser m_Laser;

    private Laser laser_obj;
    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 3);
        m_ID = (int)GameVariables.SPELL_NAMES.Laser;
    }

    public override void CastSpell()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Vector3 pos = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, -0.5f);
        laser_obj = Instantiate(m_Laser, pos, Quaternion.identity);
        laser_obj.Init(pos, new Unity.Mathematics.int2(player.CurrentTile.xPos, player.CurrentTile.yPos), SpawnSide.Down, 5000, false);
        
        base.CastSpell();

        player.GetComponent<CharacterController>().BeginChanneling(this);
    }

    public override void StopChanneling()
    {
        if(laser_obj != null)
        {
            laser_obj.DisableLaser();
            laser_obj = null;
        }
    }
}
