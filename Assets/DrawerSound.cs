using UnityEngine;

public class DrawerSound : MonoBehaviour
{
    public AudioSource drawerAudioSource;   
    public AudioClip drawerOpenClip;

    public void PlayDrawerOpenSound()
    {
        if (drawerAudioSource == null) return;

        if (drawerOpenClip != null)
            drawerAudioSource.PlayOneShot(drawerOpenClip);
        else
            AudioSource.PlayClipAtPoint(drawerOpenClip, transform.position);

    }

}
