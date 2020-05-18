using UnityEngine;
using Mirror;

public class ActionRefferer : MonoBehaviour
{
    public GameObject ParentGameObject;
    private IActionAble aComponent = null;
   
    public IActionAble ParentActionComponent
    {
        get
        {
            if (aComponent == null)
            {
                if (this is IActionAble)
                {
                    aComponent = (IActionAble)this;
                }
                else
                {
                    aComponent = ParentGameObject.GetComponent<IActionAble>();
                }
            }
            return aComponent;
        }
    }
}