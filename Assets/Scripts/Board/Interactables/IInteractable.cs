using UnityEngine;
using System.Collections;

public interface IInteractable
{
    void Interact();
}

public interface IInteractable<T>
{
    T Interact();
}

public interface CharacterInteractable
{
    void Interact(CharacterController interractor);
}
