using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterSheet
{
    public CharacterStats Stats { get; private set; }
    public Class CharacterClass { get; private set; }
    public CharacterSkills Skills { get; private set; }

    private ICharacterSheetView view;

    public CharacterSheet(ICharacterSheetView view)
    {
        this.view = view;
    }

    public void SetStats(CharacterStats stats)
    {
        Stats = stats;
        UpdateView();
    }

    public void SetClass(Class characterClass)
    {
        CharacterClass = characterClass;
        UpdateView();
    }

    public void InstantiateSkills(IPlayerEventsProvider playerEvents)
    {
        Skills = new CharacterSkills(playerEvents);
    }

    private void UpdateView()
    {
        view.SetData(this);
    }
}

public class CharacterSkills
{
    private List<ICharacterSkill> skills;

    public CharacterSkills(IPlayerEventsProvider playerEvents)
    {
        skills = new List<ICharacterSkill>();
        skills.Add(new OneHandedSkill(playerEvents));
        skills.Add(new LightArmorSkill(playerEvents));
        skills.Add(new HeavyArmorSkill(playerEvents));
    }

    public T GetUIElement<T>()
    {
        return (T)skills.First(e => e.GetType() == typeof(T));
    }
}

public interface ICharacterSheetView
{
    void Show();
    void Hide();
    void ShowHide();
    void SetData(CharacterSheet data);
}