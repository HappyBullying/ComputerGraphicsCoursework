using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkScript : NetworkBehaviour
{
    private const float castingRadius = 0.5f;
    private const float castingDistance = 2f;
    private SortedList<string, IActionAble> ACTCOLL = new SortedList<string, IActionAble>();

    void Update()
    {
        CastForActionAbleOjects();
    }
    private void CastForActionAbleOjects()
    {
        RaycastHit sphereHit;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f, 0f));

        if (Physics.SphereCast(ray, castingRadius, out sphereHit, castingDistance))
        {
            if (sphereHit.transform.gameObject.tag == "ActionAbleObject")
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    ActionRefferer aR = sphereHit.transform.gameObject.GetComponent<ActionRefferer>();
                    CmdInteract(aR.ParentGameObject.name);
                    
                }
                
            }

        }

    }


    private void Interact(string gmoName)
    {
        IActionAble obj = null;
        if (!ACTCOLL.ContainsKey(gmoName))
        {
            obj = GameObject.Find(gmoName).GetComponent<ActionRefferer>().ParentActionComponent;
            ACTCOLL.Add(gmoName, obj);
        }
        else
        {
            obj = ACTCOLL[gmoName];
        }
        
        if (obj.IsDefaultState())
        {
            obj.DoAction();
        }
        else
        {
            obj.UnDoAction();
        }

    }

    [Command]
    private void CmdInteract(string gmoName)
    {
        Debug.Log("Cmd");
        if (isServer)
            Interact(gmoName);
        RpcInteract(gmoName);
    }


    [ClientRpc]
    private void RpcInteract(string gmoName)
    {
        Debug.Log("Rpc");
        if (isClientOnly)
            Interact(gmoName);
    }
}
