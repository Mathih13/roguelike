using UnityEngine;
using System.Collections;
using InfallibleCode;

public class BoardPiece : MonoBehaviour
{
    public BoardPieceType PieceType { get; protected set; }
    public virtual string DisplayName { get; protected set; }
}

public enum BoardPieceType
{
    None = 0,
    Impassable,
    Interactable,
    Character
}
