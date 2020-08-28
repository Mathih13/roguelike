using UnityEngine;
using System.Collections;

public interface IEffect<T>
{
    void ApplyEffect(T target);
    string GetDescription();
}