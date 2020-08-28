using UnityEngine;
using System.Collections;
using mhartveit;

public class MenuUI : MonoBehaviour, UIElementCollection
{
    public ItemModalViewBehaviour ItemModal { get; private set; }
    public TreasureChestBehaviour TreasureChest { get; private set; }
    public InventoryBehaviour Inventory { get; private set; }
    public EquipmentBehaviour Equipment { get; private set; }
    public CharacterSheetBehaviour CharacterSheet { get; set; }


    private void Awake()
    {
        TreasureChest = GetComponent<TreasureChestBehaviour>();
        ItemModal = GetComponent<ItemModalViewBehaviour>();
        Inventory = GetComponent<InventoryBehaviour>();
        Equipment = GetComponent<EquipmentBehaviour>();
        CharacterSheet = GetComponent<CharacterSheetBehaviour>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ItemModal.Hide();
            TreasureChest.Hide();
            Inventory.Hide();
            Equipment.Hide();
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            CharacterSheet.ShowHide();
        }
    }
}

