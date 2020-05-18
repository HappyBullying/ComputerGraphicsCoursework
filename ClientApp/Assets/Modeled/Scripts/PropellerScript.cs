using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerScript : MonoBehaviour
{
    const float angleStep = 4;
    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.forward, angleStep, Space.World);
    }
}
