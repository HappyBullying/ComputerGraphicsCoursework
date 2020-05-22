using UnityEngine;

public class WardrobeClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to open the wardrobe";
    }

    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to close the wardrobe";
    }
}
