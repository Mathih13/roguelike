using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Tilemaps;

public class BoardManager : Singleton<BoardManager>
{
    
    public Grid boardGrid;
    public Tilemap Tilemap;
    public Transform Pieces;
    public Dictionary<Vector3, GameTile> tiles;

    public Player player;
    public GameObject[] nonCombatants;

    private List<BoardPiece> piecesOnBoard;
    public IEnumerable<BoardPiece> EnemiesOnBoard => piecesOnBoard.Where(c => c.GetType() == typeof(Enemy));

    public List<Type> characterTypes;

    private void Awake()
    {
        BoardEventHub.Instance.onBoardGenerated += SetupBoard;
    }

    private void SetupBoard()
    {
        GetWorldTiles();

        player = FindObjectOfType<Player>();
        piecesOnBoard = new List<BoardPiece>();

        BoardEventHub.Instance.BoardReady();
    }

  

    private void AddPieceToGameBoard(NonCharacterBoardPiece piece)
    {
        piece.transform.SetParent(Pieces);
        piecesOnBoard.Add(piece);
        var pos = new Vector3Int(0, 0, 0);
        piece.transform.position = boardGrid.CellToWorld(pos);
        tiles[pos].MoveBoardPieceToTile(null, piece);
    }

    private void AddPieceToGameBoard(CharacterController piece)
    {
        piece.transform.SetParent(Pieces);
        piecesOnBoard.Add(piece);
        var pos = new Vector3Int(0, 0, 0);
        piece.transform.position = boardGrid.CellToWorld(pos);
        tiles[pos].MoveCharacterToTile(null, piece);
    }

    public void AddPieceToGameBoard(NonCharacterBoardPiece piece, Vector3Int position)
    {
        piece.transform.position = boardGrid.CellToWorld(position);
        piece.transform.SetParent(Pieces);
        piecesOnBoard.Add(piece);
        tiles[position].MoveBoardPieceToTile(null, piece);
    }

    private void AddPieceToGameBoard(CharacterController piece, Vector3Int position)
    {
        piece.transform.position = boardGrid.CellToWorld(position);
        piece.transform.SetParent(Pieces);
        piecesOnBoard.Add(piece);
        tiles[position].MoveCharacterToTile(null, piece);
    }

    public void RemovePieceFromGameBoard(NonCharacterBoardPiece piece, Vector3 worldPosition)
    {
        GetTileFromWorldCoords(worldPosition).RemovePiece(piece);
        piecesOnBoard.Remove(piece);
        Destroy(piece.gameObject);

    }

    public void RemoveCharacterFromGameBoard(CharacterController character)
    {
        GetGameTileUnderCharacter(character).RemoveCharacter(character);
        piecesOnBoard.Remove(character);
        Destroy(character.gameObject);
    }


    public void AddCharacterToGameBoard(CharacterController character)
    {
        AddPieceToGameBoard(character);
    }

    public void AddCharacterToGameBoard(CharacterController character, Vector3Int position)
    {
        AddPieceToGameBoard(character, position);
    }

    

    public void AddToGameBoard(IEnumerable<CharacterController> character)
    {
        piecesOnBoard.AddRange(character);
    }

    public GameTile GetTileFromWorldCoords(Vector3 coordinates)
    {
        return tiles[boardGrid.WorldToCell(coordinates)];
    }

    public Vector3Int GetPositionOfCharacter(CharacterController character)
    {
        return boardGrid.WorldToCell(character.transform.position);
    }

    public GameTile GetGameTileUnderCharacter(CharacterController character)
    {
        return tiles[GetPositionOfCharacter(character)];
    }

    public Vector3Int GetPlayerPosition() => GetPositionOfCharacter(player);

    private void GetWorldTiles()
    {
        tiles = new Dictionary<Vector3, GameTile>();
        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            var tile = new GameTile
            {
                LocalPlace = localPlace,
                WorldLocation = Tilemap.CellToWorld(localPlace),
                TileBase = Tilemap.GetTile(localPlace),
                TilemapMember = Tilemap,
                Name = localPlace.x + "," + localPlace.y,
                Cost = 1 // cost = 0 for impassable
            };

            tiles.Add(tile.WorldLocation, tile);
        }
    }
}

public struct CellData
{
    public bool Occupied { get; set; }
    public CharacterController Character { get; set; }
    public Vector3 Position { get; set; }
}
