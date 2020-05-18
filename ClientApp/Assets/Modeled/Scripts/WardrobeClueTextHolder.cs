using UnityEngine;

public class WardrobeClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to open the wardrobe";
    }

    public override string GetExecutedClueText(char ActionKey)
    {
        return "Press " + ActionKey + " to close the wardrobe";
    }
}
