using UnityEngine;

public class BaseClueTextHoler : MonoBehaviour
{
    public virtual string GetDefaultClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey;
    }
    public virtual string GetExecutedClueText(KeyCode ActionKey)
    {
        return "Press " + ActionKey;
    }
}