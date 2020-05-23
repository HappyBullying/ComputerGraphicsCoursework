using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.Linq;

public class PlayerNetworkScript : NetworkBehaviour
{
    private GameObject Panel;
    private Text ClueText;
    public KeyCode ActionKey = KeyCode.E;
    private const float castingRadius = 0.55f;
    private const float castingDistance = 3f;
    private bool Synchronized = false;
    private SortedList<string, IActionAble> InteractedObjects = new SortedList<string, IActionAble>();
    

    private void Start()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Clue");
        Panel = tmp.FirstOrDefault(name => name.name == "CluePanel");
        ClueText = (Text)(tmp.FirstOrDefault(name => name.name == "ClueText").GetComponent<Text>());
        Panel.SetActive(false);
    }


    private void Update()
    {   
        CastForActionAbleOjects();
    }
    private void CastForActionAbleOjects()
    {
        RaycastHit sphereHit;
        Ray ray = Camera.main.ScreenPointToRay(
            Input.mousePosition);

        if (Physics.SphereCast(ray, castingRadius, out sphereHit, castingDistance))
        {
            if (sphereHit.transform.gameObject.tag == "ActionAbleObject")
            {
                ActionRefferer aR = sphereHit.transform.gameObject.GetComponent<ActionRefferer>();
                SetClue(aR);
                if (Input.GetKeyDown(ActionKey))
                {
                    
                    Debug.LogWarning(aR.ParentGameObject.name);
                    CmdInteract(aR.ParentGameObject.name);
                }
            }
            else
            {
                HideClue();
            }
        }
    }


    private void SetClue(ActionRefferer aR)
    {
        if (Panel == null)
            return;
        Panel.SetActive(true);
        if (aR.ParentActionComponent.IsDefaultState())
        {
            ClueText.text = aR.ParentGameObject.GetComponent<BaseClueTextHoler>().GetDefaultClueText(ActionKey);
        }
        else
        {
            ClueText.text = aR.ParentGameObject.GetComponent<BaseClueTextHoler>().GetExecutedClueText(ActionKey);
        }
    }

    private void HideClue()
    {
        if (Panel != null)
            Panel.SetActive(false);
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
