using UnityEngine;
using System.Collections;

public class HotBarBehaviour : MonoBehaviour, IPlayerHotBarView
{
    [SerializeField]
    private PlayerHotBarUI ui;

    public void SetData(PlayerHotBar data)
    {
        ui.Initialize(data);
    }

    
}
