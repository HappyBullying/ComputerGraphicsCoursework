using UnityEngine;

public class FridgeClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to open the fridge door";
    }

    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to close the fridge door";
    }
}