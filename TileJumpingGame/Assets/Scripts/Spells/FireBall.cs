using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : Spell
{
    public Projectile m_FireballProjectile;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 3);
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
        m_SpellImage = Resources.Load<GameObject>("FireBallImage").GetComponent<Image>();
    }

    public override void CastSpell(Inventory inventory)
    {
        //TODO: Perform your spell here!
        Instantiate(m_FireballProjectile, inventory.transform.position + new Vector3(0,0,-0.5f), Quaternion.identity);
        //GameObject.FindWithTag("Boss").GetComponent<Boss>().TakeDamage(10);
        base.CastSpell(inventory);
    }
}
