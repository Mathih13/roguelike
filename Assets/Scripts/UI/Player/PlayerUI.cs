using UnityEngine;
using System.Collections;
using mhartveit;

public class PlayerUI : MonoBehaviour, UIElementCollection
{
    public PlayerHealthUI Health { get; private set; }
    public HotBarBehaviour HotBar { get; private set; }

    private void Start()
    {
        Health = GetComponent<PlayerHealthUI>();
        HotBar = GetComponent<HotBarBehaviour>();
        
    }
}
