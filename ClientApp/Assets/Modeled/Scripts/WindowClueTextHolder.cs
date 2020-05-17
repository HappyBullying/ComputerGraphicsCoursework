using UnityEngine;


public class WindowClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to open the window";
    }

    public override string GetExecutedClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to close the window";
    }
}