using UnityEngine;
using System.Collections;

public class ItemViewBehaviour : MonoBehaviour
{
}

public interface IItemView<T>
{
    void Initialize(T item);
}