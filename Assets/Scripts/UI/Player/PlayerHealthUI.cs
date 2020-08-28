using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Rougelike.EventLog;

public class PlayerHealthUI : MonoBehaviour, UIElement<CombatEntity>
{
    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private TMP_Text playerHealthText;

    [SerializeField]
    private GameObject floatingInfoTextPrefab;

    private CombatEntity player;
    private Action<CombatEntity, HealData> onHealed;
    private GameObject worldCanvas;

    private void Start()
    {
        worldCanvas = GameObject.FindGameObjectWithTag("World_Canvas");
    }

    public void Initialize(CombatEntity data)
    {
        player = data;
        playerHealthSlider.maxValue = player.MaxHP;
        playerHealthSlider.minValue = 0;
        SetSlider(player);
        SetText(player);
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        CombatEventHub.Instance.onCombatEntityDamaged += UpdateHealthBar;
        player.onHealed += HandleHealingDisplay;
    }

    private void HandleHealingDisplay(CombatEntity source, HealData data)
    {
        var obj = Instantiate(floatingInfoTextPrefab, worldCanvas.transform);
        obj.SetActive(false);
        obj.GetComponent<FloatingInfoText>().Initialize(new FloatingTextData
        {
            Text = data.Amount.ToString(),
            StartPosition = data.Target.Controller.transform.position
        });

    }

    private void UpdateHealthBar(AttackCalculationData obj)
    {
        if (obj.Target.Equals(player))
        {
            SetSlider(player);
            SetText(player);
        }
    }

    private void SetSlider(CombatEntity player)
    {
        playerHealthSlider.value = player.HP;
    }

    private void SetText(CombatEntity player)
    {
        playerHealthText.text = player.HP.ToString();
    }
}
