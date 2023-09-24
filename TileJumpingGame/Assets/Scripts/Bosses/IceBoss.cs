using UnityEngine;

public class IceBoss : Boss
{
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackInterval)
        {
            attackTimer = 0;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void AdvancePhase()
    {
        if (Phase == 1)
        {
            FreezeMiddleOfMap();
        }
    }

    private void FreezeMiddleOfMap()
    {

    }
}
