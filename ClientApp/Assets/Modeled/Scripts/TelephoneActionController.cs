using UnityEngine;


public class TelephoneActionController : MonoBehaviour, IActionAble
{
    public AudioSource PhoneBeeps;
    public void DoAction()
    {
        PhoneBeeps.Play();
    }
    public void UnDoAction()
    {
        PhoneBeeps.Play();
    }
    public bool IsDefaultState()
    {
        return !PhoneBeeps.isPlaying;
    }

    public bool CanExecuteAction()
    {
        return !PhoneBeeps.isPlaying;
    }
}