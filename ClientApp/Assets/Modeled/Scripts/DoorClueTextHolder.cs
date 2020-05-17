using UnityEngine;

public class DoorClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to open the door";
    }

    public override string GetExecutedClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to close the door";
    }
}