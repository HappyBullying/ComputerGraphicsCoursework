using UnityEngine;

public class FridgeClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to open the fridge door";
    }

    public override string GetExecutedClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to close the fridge door";
    }
}