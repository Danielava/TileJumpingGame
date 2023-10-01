using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Board;
using UnityEngine;

public enum Direction
{
    Up,
    Left,
    Down,
    Right,
    NONE
}

public class BoardEntity : MonoBehaviour
{
    public Tile CurrentTile { get; set; }
    public GridTileBoard Board;
    public float MoveSpeed = 0.1f;

    virtual public void Awake()
    {
        Board = GameObject.Find("Board").GetComponent<GridTileBoard>();
    }


    virtual public void EnterTile(Tile tile, bool direct = false)
    {
        if (CurrentTile)
        {
            CurrentTile.entityOnTile = null;
        }
        float zPos = transform.position.z;
        tile.entityOnTile = this;
        transform.position = tile.transform.position + new Vector3(0, 0, zPos);
        CurrentTile = tile;
    }

    virtual public IEnumerator WaitAndEnterTile(float waitTime, Direction direction, Tile tile)
    {
        yield return new WaitForSeconds(waitTime);
        EnterTile(tile, true);
        if (tile.tileType == TileType.Ice)
        {
            StartCoroutine(TryMove(direction, 1));
        }
    }


    public virtual IEnumerator TryMove(Direction direction, int steps = 1, float speed = 1)
    {
        if (steps == 0) yield break;
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
            tile.entityOnTile = this;
            var moveLockDuration = MoveSpeed * (steps + 1) * speed;
            gameObject.AddComponent<Move>().Init(tile.gameObject.transform.position, moveLockDuration, null);
            StartCoroutine(WaitAndEnterTile(moveLockDuration, direction, tile));
        }
    }

    public Tile GetAvailableTile(int xPos, int yPos)
    {
        if (Board.CanMoveTo(xPos, yPos))
        {
            return Board.GetTile(xPos, yPos);
        }
        return null;
    }
}
