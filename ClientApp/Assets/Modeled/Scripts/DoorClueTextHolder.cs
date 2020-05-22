using UnityEngine;

public class DoorClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to open the door";
    }

    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to close the door";
    }
}