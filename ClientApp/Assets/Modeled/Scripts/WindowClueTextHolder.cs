using UnityEngine;


public class WindowClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to open the window";
    }

    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to close the window";
    }
}