using UnityEngine;


public class TelephoneClueTextHolder : BaseClueTextHoler
{
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to Call Kate Marsh";
    }
    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey + " to Call Kate Marsh";
    }
}