using UnityEngine;

public class LightClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to switch on the light";
    }

    public override string GetExecutedClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to switch off the light";
    }
}