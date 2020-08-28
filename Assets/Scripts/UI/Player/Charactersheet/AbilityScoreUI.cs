using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScoreUI : MonoBehaviour, UIElement<CharacterStats>
{
    [SerializeField]
    private TextMeshProUGUI strength_value;

    [SerializeField]
    private TextMeshProUGUI dexterity_value;

    [SerializeField]
    private TextMeshProUGUI constitution_value;

    [SerializeField]
    private TextMeshProUGUI intelligence_value;

    [SerializeField]
    private TextMeshProUGUI wisdom_value;

    [SerializeField]
    private TextMeshProUGUI charisma_value;

    public void Initialize(CharacterStats data)
    {
        strength_value.text = data.Strength.Value.ToString();
        dexterity_value.text = data.Dexterity.Value.ToString();
        constitution_value.text = data.Constitution.Value.ToString();
        intelligence_value.text = data.Intelligence.Value.ToString();
        wisdom_value.text = data.Wisdom.Value.ToString();
        charisma_value.text = data.Charisma.Value.ToString();
    }
}
