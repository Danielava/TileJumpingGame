using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Assets.Scripts.Board;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    PreparingSpell,
    CastingSpell,
    Channeling
}

public class Player : BoardEntity
{
    public PlayerHealthUI playerHealthUI;
    public int maxHealth;
    private int currentHealth;
    public Inventory inventory;
    public PlayerState PlayerState = PlayerState.Idle;
    private Spell PreparedSpell;
    private ChanneledSpell channeledSpell;

    public Direction bufferedMove = Direction.NONE;
    public float bufferTime = 0.2f;

    private GameObject m_CounterCharged; //E.g charged with counter spell

    // Start is called before the first frame update
    override public void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        playerHealthUI.SetMaxHealth(maxHealth);
        playerHealthUI.SetCurrentHealth(currentHealth);
    }


    override public void EnterTile(Tile tile, bool direct = false)
    {
        base.EnterTile(tile);

        //pickups and shit
        tile.EnterTile(inventory, direct);
    }

    public void TakeDamage(int damage)
    {
        if (InCounterState()) //
        {
            Debug.Log("Countered!!");
            Destroy(m_CounterCharged);
            m_CounterCharged = null;
            return;
        }
        currentHealth -= damage;
        playerHealthUI.SetCurrentHealth(currentHealth);
        //Debug.Log("oowie noo i got hit :(");

        StopChanneling();
    }

    public void CastSpell(Spell spell, Direction? direction = null)
    {
        if (PlayerState == PlayerState.Channeling)
        {
            StopChanneling();
        }
        if (direction.HasValue)
        {
            spell.CastDirectionalSpell(direction.Value);
        }
        else
        {
            spell.CastSpell();
        }

        foreach (var elementCost in spell.GetSpellCastCost())
        {
            inventory.IncrementElement(elementCost.Key, -elementCost.Value);
        }
        inventory.UpdateSpellPanelNumbers();
    }

    public void SetCounterState(GameObject charge)
    {
        m_CounterCharged = charge;
    }

    public bool InCounterState()
    {
        return (m_CounterCharged != null);
    }


    public void PrepareSpell(Spell spell)
    {
        StopChanneling();
        PlayerState = PlayerState.PreparingSpell;
        //m_DirectionalArrows.SetActive(true);
        PreparedSpell = spell;
    }

    public void PlayerCastPreparedSpell(Direction direction)
    {
        StopChanneling();
        CastSpell(PreparedSpell, direction);
        PlayerState = PlayerState.Idle;
        //m_DirectionalArrows.SetActive(true);
    }

    public void BeginChanneling(ChanneledSpell spell)
    {
        PlayerState = PlayerState.Channeling;
        channeledSpell = spell;
    }

    public void StopChanneling()
    {
        if (channeledSpell != null && PlayerState == PlayerState.Channeling)
        {
            PlayerState = PlayerState.Idle;
            channeledSpell.StopChanneling();

            channeledSpell = null;
        }
    }

    public void Teleport(Direction direction)
    {
        PlayerState = PlayerState.Idle;
        StartCoroutine(TryMove(direction, 3, 0.1f));
    }

    private IEnumerator EnterTile(float waitTime, Direction direction, Tile tile)
    {

        yield return new WaitForSeconds(waitTime);

        EnterTile(tile, true);
        PlayerState = PlayerState.Idle;
        if (tile.tileType == TileType.Ice)
        {
            StartCoroutine(TryMove(direction, 1));
        }
        else if (bufferedMove != Direction.NONE)
        {
            StartCoroutine(TryMove(bufferedMove, 1));
            bufferedMove = Direction.NONE;
        }
    }

    public IEnumerator ClearBuffer()
    {
        yield return new WaitForSeconds(bufferTime);

        if (bufferedMove != Direction.NONE)
        {
            bufferedMove = Direction.NONE;
        }
    }

    override public IEnumerator TryMove(Direction direction, int steps, float speed = 1)
    {
        if (PlayerState == PlayerState.Channeling)
        {
            StopChanneling();
        }

        if (PlayerState != PlayerState.Idle || steps == 0) yield break;

        Tile tile = null;
        while (tile == null && steps != 0)
        {
            switch (direction)
            {
                case Direction.Up:
                    tile = GetAvailableTile(CurrentTile.xPos, CurrentTile.yPos + steps);
                    break;
                case Direction.Down:
                    tile = GetAvailableTile(CurrentTile.xPos, CurrentTile.yPos - steps);
                    break;
                case Direction.Right:
                    tile = GetAvailableTile(CurrentTile.xPos + steps, CurrentTile.yPos);
                    break;
                case Direction.Left:
                    tile = GetAvailableTile(CurrentTile.xPos - steps, CurrentTile.yPos);
                    break;
            }
            steps--;
        }

        if (tile != null)
        {
            // Set entity on tile before entering to hinder others from entering
            tile.entityOnTile = this;
            var moveLockDuration = MoveSpeed * (steps + 1) * speed;
            gameObject.AddComponent<Move>().Init(tile.gameObject.transform.position, moveLockDuration, null);
            PlayerState = PlayerState.Moving;
            StartCoroutine(EnterTile(moveLockDuration, direction, tile));
        }
    }
}
