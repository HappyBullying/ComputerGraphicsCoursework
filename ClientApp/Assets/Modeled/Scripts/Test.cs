using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        Debug.Log( GetComponent<Renderer>().bounds.size.x + "   "
         +  GetComponent<Renderer>().bounds.size.y + "  " +  GetComponent<Renderer>().bounds.size.z);
    }
}
