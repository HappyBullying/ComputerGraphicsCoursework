using UnityEngine;


public class CanvasAction : MonoBehaviour, IActionAble
{
    public void DoAction()
    {
    }

    public void UnDoAction()
    {
    }

    public bool IsDefaultState()
    {
        return true;
    }

    public bool CanExecuteAction()
    {
        return true;
    }
}