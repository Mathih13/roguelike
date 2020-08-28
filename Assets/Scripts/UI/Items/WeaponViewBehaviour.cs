using UnityEngine;
using System.Collections;

public class WeaponViewBehaviour : ItemViewBehaviour, IItemView<Weapon>
{
    public Weapon Weapon { get; private set; }

    public void Initialize(Weapon item)
    {
        Weapon = item;
    }
}
