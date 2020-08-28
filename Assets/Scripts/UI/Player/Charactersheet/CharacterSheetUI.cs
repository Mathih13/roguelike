using UnityEngine;
using System.Collections;
using mhartveit;
using DG.Tweening;

public class CharacterSheetUI : MonoBehaviour, UIElement<CharacterSheet>, IModalView
{
    public bool Open { get; private set; }

    [SerializeField]
    private AbilityScoreUI abilityscoreUI;

    [SerializeField]
    private ClassInfoUI classinfoUI;

    public void Initialize(CharacterSheet data)
    {
        abilityscoreUI.Initialize(data.Stats);
        classinfoUI.Initialize(data.CharacterClass);
    }

    public void Show()
    {
        if (!Open)
        {
            transform.localScale = new Vector3(1, 1);
            gameObject.SetActive(true);
            Open = true;
        }
    }

    public void Hide()
    {
        if (Open)
        {
            UI.Instance.CloseAnimation(transform).OnComplete(() =>
            {
                
                Open = false;
                gameObject.SetActive(false);
            });
        }
    }
}
