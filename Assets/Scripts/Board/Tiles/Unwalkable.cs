using UnityEngine;
using System.Collections;

public class Unwalkable : HierarchyOrganizedTile
{
    private Transform LogicTiles;

    private void Awake()
    {
        LogicTiles = GameObject.FindGameObjectWithTag("Logic_Tiles").transform;
        BoardEventHub.Instance.onBoardReady += () =>
        {
            var tile = BoardManager.Instance.GetTileFromWorldCoords(transform.position);
            tile.Cost = 0;
            transform.SetParent(LogicTiles);
        };
    }
}
