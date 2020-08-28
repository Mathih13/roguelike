using UnityEngine;
using System.Collections;

public class CharacterSheetBehaviour : MonoBehaviour, ICharacterSheetView
{
    private CharacterSheet currentCharacterSheet;

    [SerializeField]
    private CharacterSheetUI ui;

    public void Hide()
    {
        ui.Hide();
    }

    public void SetData(CharacterSheet data)
    {
        currentCharacterSheet = data;
        ui.Initialize(currentCharacterSheet);
    }

    public void Show()
    {
        ui.Show();
    }

    public void ShowHide()
    {
        if (ui.Open)
        {
            Hide();
            return;
        }

        Show();
    }
}
