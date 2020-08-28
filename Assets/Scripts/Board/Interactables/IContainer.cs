using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IContainer<T>
{
    List<T> GetAll();
    T Get(int index);
    T Get(T element);
    T Take(int index);
    T Take(T element);
    void Place(T element);
    void Place(T element, int index);
    bool Contains(T element);
}
