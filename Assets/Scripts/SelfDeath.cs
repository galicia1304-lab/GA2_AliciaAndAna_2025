using UnityEngine;

public class SelfDeath : MonoBehaviour
{
    public float deathTime = 3.0f;

    public AudioClip[] coinSounds;

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        int ruben = Random.Range(0, coinSounds.Length);
        print(ruben);

        audio.clip = coinSounds[ruben];
        audio.Play();
        Destroy(gameObject, deathTime);
    }

    
}
