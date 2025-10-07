using UnityEngine;

public class ShelfSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shelfOpenClip;

    public void PlayShelfSound()
    {
        if (shelfOpenClip == null) return;

        if (audioSource != null)
            audioSource.PlayOneShot(shelfOpenClip);
        else
            AudioSource.PlayClipAtPoint(shelfOpenClip, transform.position);
    }
}
