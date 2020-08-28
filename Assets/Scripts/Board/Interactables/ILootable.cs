using UnityEngine;
using System.Collections;

public interface ILootable
{
    // An item that can be added and removed from a container
    // The container can be a chest or the player itself
}


public interface ILootable<T>
{
    // An item that can be added and removed from a container
    // The container can be a chest or the player itself
    void PickUp(T looter);
}