using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Rougelike.EventLog;

public abstract class CharacterSkill
{
    public int Level { get; private set; }
    private int currentXP;

    protected abstract string GetDisplayName();


    private Dictionary<int, int> levelThresholds = new Dictionary<int, int>
    {
        {1, 100},
        {2, 50},
        {3, 300},
        {4, 400},
        {5, 500},
        {6, 600},
        {7, 700},
        {8, 800},
        {9, 900},
        {10, 1000},
    };
    private readonly IPlayerEventsProvider eventsProvider;

    public CharacterSkill(IPlayerEventsProvider eventsProvider)
    {
        Level = 1;
        this.eventsProvider = eventsProvider;
    }

   
    public void IncreaseXP(int amount)
    {
        if(!SkillAtMaxLevel())
            currentXP += amount;

        if (PassedLevelThreshold(currentXP))
        {
            IncreaseLevelAndResetCurrentXP();
        }
    }

    public void SetLevel(int newLevel)
    {
        if (newLevel > MaxLevel())
        {
            Level = MaxLevel();
        }
        else if (newLevel < MinLevel())
        {
            Level = MinLevel();
        }
        else
        {
            while (Level < newLevel)
            {
                IncreaseLevelAndResetCurrentXP();
            }
        }

        
    }

    private bool SkillAtMaxLevel()
    {
        return Level == MaxLevel();
    }

    private void IncreaseLevelAndResetCurrentXP()
    {
        Level++;
        currentXP = 0;
        EventLogHub.Instance.QuickEventLogMessage($"{GetDisplayName()} has increased to level {Level}!", EventLogItemType.Success);
    }


    private bool PassedLevelThreshold(int amount)
    {
        if (levelThresholds.ContainsKey(Level + 1))
        {
            return levelThresholds[Level + 1] <= amount;
        }
        return false;
    }

    private int MinLevel()
    {
        return levelThresholds.Min(x => x.Key);
    }

    private int MaxLevel()
    {
        return levelThresholds.Max(x => x.Key);
    }


    public int GetCurrentXP() => currentXP;
}
