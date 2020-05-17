using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkScript : MonoBehaviour
{
    private const float castingRadius = 0.5f;
    private const float castingDistance = 2f;

    void Update()
    {
        CastForActionAbleOjects();
    }
    private void CastForActionAbleOjects()
    {
        RaycastHit sphereHit;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f, 0f)
        );
        Debug.DrawRay(transform.position, Camera.main.transform.forward * castingDistance);
        if (Physics.SphereCast(ray, castingRadius, out sphereHit, castingDistance))
        {
            if (sphereHit.transform.gameObject.tag == "ActionAbleObject")
            {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * castingDistance, Color.yellow);
                if (Input.GetKeyDown(KeyCode.T))
                sphereHit.transform.gameObject.GetComponent<ActionRefferer>().ParentActionComponent.DoAction();
                try{
                Debug.Log(sphereHit.transform.gameObject.GetComponent<ActionRefferer>().ParentGameObject.GetComponent<BaseClueTextHoler>().GetDefaultClueText('E'));    
                }
                catch
                {
                    Debug.LogError(sphereHit.transform.gameObject.name);
                }
            }

        }

    }
}
