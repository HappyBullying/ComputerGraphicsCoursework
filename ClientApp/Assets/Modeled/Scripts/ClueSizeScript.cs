using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueSizeScript : MonoBehaviour
{
    public RectTransform transform;
    void Start()
    {
        transform.sizeDelta = new Vector2(transform.sizeDelta.x, Screen.height * 0.1f);
    }
}
