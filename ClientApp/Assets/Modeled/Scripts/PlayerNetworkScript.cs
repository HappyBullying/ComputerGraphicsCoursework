using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Text;

public class PlayerNetworkScript : NetworkBehaviour
{
    private const float castingRadius = 0.55f;
    private const float castingDistance = 3f;
    private bool Synchronized = false;
    private SortedList<string, IActionAble> InteractedObjects = new SortedList<string, IActionAble>();
    
    private void Update()
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


            if (sphereHit.transform.gameObject.tag == "ClueAble")
            {

            }
        }
    }


    IActionAble FindAAObject(string gmoName)
    {
        IActionAble obj = null;
        if (!InteractedObjects.ContainsKey(gmoName))
        {
            obj = GameObject.Find(gmoName).GetComponent<ActionRefferer>().ParentActionComponent;
            InteractedObjects.Add(gmoName, obj);
        }
        else
        {
            obj = InteractedObjects[gmoName];
        }
        return obj;
    }


    private void Interact(string gmoName)
    {
        IActionAble obj = FindAAObject(gmoName);
        
        if (!obj.CanExecuteAction())
            return;

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
        if (isServer)
        {
            Interact(gmoName);
        }
        RpcInteract(gmoName);
    }


    [ClientRpc]
    private void RpcInteract(string gmoName)
    {
        if (isClientOnly)
        {
            Interact(gmoName);
        }
    }
}
