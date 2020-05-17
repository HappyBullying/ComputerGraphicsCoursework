using UnityEngine;
using System.Collections.Generic;

public class AnimController : MonoBehaviour, IActionAble
{
    public Animation animation;
    private AnimationClip[] Clips;
    private bool isDefaultState = true;

    private void Awake()
    {
        Clips = new AnimationClip[animation.GetClipCount()];
        int i = 0;
        foreach(AnimationState a_s in animation)
        {
            Clips[i] = a_s.clip;
            i++;
        }
    }

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
        animation.clip = Clips[0];
        animation.Play();
    }

    public void UnDoAction()
    {
        if (isDefaultState)
            return;
        isDefaultState = true;
        animation.clip = Clips[1];
        animation.Play();
    }

    public bool IsDefaultState()
    {
        return isDefaultState;
    }
}
