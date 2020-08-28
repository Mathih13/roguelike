using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "ScriptableObjects/UI/EventLog/EventLogTextColorPalette")]
public class EventLogTextColorPalette : ScriptableObject
{
    public Color32 Default;
    public Color32 Info;
    public Color32 Warning;
    public Color32 Danger;
    public Color32 Success;
}
