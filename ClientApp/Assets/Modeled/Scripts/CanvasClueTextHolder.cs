using System;
using UnityEngine;

public class CanvasClueTextHolder : BaseClueTextHoler
{
    public string CanvasDescription;
    public override string GetDefaultClueText(KeyCode ActionKey)
    {
        return CanvasDescription;
    }
    public override string GetExecutedClueText(KeyCode ActionKey)
    {
        return CanvasDescription;
    }
}