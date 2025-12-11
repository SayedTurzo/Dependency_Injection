using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Register<AudioTest>(this);
    }
    
    //ServiceLocator.Get<AudioManager>().PlaySFX("slash"); this is how we can get it..
}
