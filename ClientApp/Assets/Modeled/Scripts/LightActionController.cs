using UnityEngine;
using CommonScripts;

public class LightActionController : MonoBehaviour, IActionAble
{
    /***BY DEFAULT LIGHTS ARE TURNED OFF***/
    public float OffToggleAngle = 0;
    public float OnToggleAngle = 0;
    public Vector3 VectorAround;
    public GameObject ToggleObject;
    private bool isDefaultState = true;
    private LightControllScipt lc;


void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoAction();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnDoAction();
        }
    }

    public void DoAction()
    {
        if (!isDefaultState)
            return;
        isDefaultState = false;
        if (lc == null)
            lc = GetComponent<LightControllScipt>();

        foreach(Light l in lc.Lights)
        {
            l.enabled = true;
        }

        foreach(Light l in lc.DecorLights)
        {
            l.enabled = true;
        }
        ToggleObject.transform.Rotate(VectorAround, -(OnToggleAngle - OffToggleAngle), Space.Self);
    }


    public void UnDoAction()
    {
        if (isDefaultState)
            return;
        isDefaultState = true;
        if (lc == null)
            lc = GetComponent<LightControllScipt>();

        foreach(Light l in lc.Lights)
        {
            l.enabled = false;
        }

        foreach(Light l in lc.DecorLights)
        {
            l.enabled = false;
        }
        ToggleObject.transform.Rotate(VectorAround, (OnToggleAngle - OffToggleAngle), Space.Self);
    }

    public bool IsDefaultState()
    {
        return isDefaultState;
    }
}