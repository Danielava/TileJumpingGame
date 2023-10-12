using UnityEngine;

public class FireBall : Spell
{
    public Projectile m_FireballProjectile;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 1);
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
    }

    public override void CastSpell()
    {
        //TODO: Perform your spell here!
        Vector3 pos = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, -0.5f);
        Instantiate(m_FireballProjectile, pos, Quaternion.identity).Init(10f, Vector2.up);
        base.CastSpell();
    }
}
