using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Spell
{
    public Projectile m_FireballProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 3);
        m_ID = (int)GameVariables.SPELL_NAMES.TripleShot;
    }

    public override void CastSpell()
    {
        //TODO: Perform your spell here!
        Vector3 pos = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, -0.5f);

        for(int i = 0; i < 16; i++)
        {
            Instantiate(m_FireballProjectile, pos, Quaternion.identity).Init(6f, new Vector2(Mathf.Sin(Mathf.PI / 8 * i), Mathf.Cos(Mathf.PI / 8 * i)), i * -22.5f);
        }
        base.CastSpell();
    }
}
