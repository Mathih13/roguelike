using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class GameTile
{
    public Vector3Int LocalPlace { get; set; }
    public Vector3 WorldLocation { get; set; }
    public TileBase TileBase { get; set; }
    public Tilemap TilemapMember { get; set; }
    public string Name { get; set; }
    public bool IsExplored { get; set; }
    public GameTile ExploredFrom { get; set; }
    public int Cost { get; set; }
    public List<CharacterController> CharacterPieces { get; set; }
    public NonCharacterBoardPiece NonCharacterPiece { get; private set; }


    public GameTile()
    {
        CharacterPieces = new List<CharacterController>();
    }

    public MoveResult MoveCharacterToTile(GameTile from, CharacterController character)
    {
        var Result = new MoveResult();

        if (!HasCharacter && Cost > 0)
        {
            CharacterPieces.Add(character);
            if (from != null)
            {
                from.RemoveCharacter(character);
            }

            Result.Moved = true;
            Result.CharacterAtTile = character;
            Result.PositionOfTile = LocalPlace;
            return Result;
        }

        Result.PositionOfTile = LocalPlace;
        Result.CharacterAtTile = CharacterPieces[0] != null ? CharacterPieces[0] : null;
        Result.Moved = false;
        return Result;
    }

    public MoveResult MoveBoardPieceToTile(GameTile from, NonCharacterBoardPiece piece)
    {
        var Result = new MoveResult();

        if (NonCharacterPiece != null)
        {
            Result.Moved = false;
            return Result;
        }

        if (from != null)
        {
            from.RemovePiece(piece);
        }

        NonCharacterPiece = piece;
        Result.Moved = true;
        Result.PositionOfTile = LocalPlace;
        return Result;
    }

    public bool HasCharacter => CharacterPieces.Count() > 0;
    public CharacterController GetCharacter => CharacterPieces[0];
    public void RemoveCharacter(CharacterController character) => CharacterPieces.Remove(character);
    public void RemovePiece(NonCharacterBoardPiece piece) => NonCharacterPiece = null;

}

public struct MoveResult
{
    public bool Moved { get; set; }
    public CharacterController CharacterAtTile { get; set; }
    public Vector3 PositionOfTile { get; set; }
}
