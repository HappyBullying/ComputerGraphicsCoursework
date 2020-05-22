using UnityEngine;

public class LightClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to switch on the light";
    }

    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to switch off the light";
    }
}