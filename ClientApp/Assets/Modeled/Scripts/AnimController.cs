using UnityEngine;
public class AnimController : MonoBehaviour, IActionAble
{
    public Animation _animation;
    private AnimationClip[] Clips;
    private bool isDefaultState = true;
    private void Awake()
    {
        Clips = new AnimationClip[_animation.GetClipCount()];
        int i = 0;
        foreach(AnimationState a_s in _animation)
        {
            Clips[i] = a_s.clip;
            i++;
        }
    }
    // Unterface methods
    /***************************************************/
    public void DoAction()
    {
        if (!isDefaultState)
            return;
        isDefaultState = false;
        _animation.clip = Clips[0];
        _animation.Play();
    }

    public void UnDoAction()
    {
        if (isDefaultState)
            return;
        isDefaultState = true;
        _animation.clip = Clips[1];
        _animation.Play();
    }

    public bool IsDefaultState()
    {
        return isDefaultState;
    }
    public bool CanExecuteAction()
    {
        return !_animation.isPlaying;
    }
    /***************************************************/
}