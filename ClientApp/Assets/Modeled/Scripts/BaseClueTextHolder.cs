using UnityEngine;

public class BaseClueTextHoler : MonoBehaviour
{
    public virtual string GetDefaultClueText(char ActionKey)
    {
        return "Press " + ActionKey;
    }
    public virtual string GetExecutedClueText(char ActionKey)
    {
        return "Press " + ActionKey;
    }
}