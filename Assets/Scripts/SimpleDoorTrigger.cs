using UnityEngine;

public class SimpleDoorTrigger : MonoBehaviour
{
    public SimpleDoorController door;
    public GameObject prompt;
    public LayerMask doorMask = 1 << 9;

    
    public AudioSource audioSource;        
    public AudioClip openClip;
    public AudioClip closeClip;
    [Range(0f, 1f)] public float volume = 1f;
    public Vector2 pitchRandomRange = new Vector2(0.96f, 1.04f);

    bool near = false;
    float timer = -1f;

    void Start()
    {
        if (prompt) prompt.SetActive(false);
        // auto-find if not assigned
        if (!audioSource && door) audioSource = door.GetComponent<AudioSource>();
        if (audioSource) audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!near && prompt && prompt.activeSelf) prompt.SetActive(false);

        if (near && Input.GetKeyDown(KeyCode.E))
        {
            if (!door.IsOpen)
            {
                door.OpenDoor();
                PlayOpenSFX();
                timer = Time.time;
            }
            else
            {
                door.CloseDoor();
                PlayCloseSFX();
                timer = -1f;
            }
        }

        // auto-close after 7s
        if (door.IsOpen && timer > 0f && Time.time - timer > 7f)
        {
            door.CloseDoor();
            PlayCloseSFX();
            timer = -1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) near = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Transform t = other.transform;
        bool lookingAtDoor = Physics.Raycast(t.position, t.forward, out _, 10f, doorMask);
        if (prompt) prompt.SetActive(lookingAtDoor && !door.IsOpen);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        near = false;
        if (prompt) prompt.SetActive(false);
    }

    //  SFX 
    void PlayOpenSFX()
    {
        if (!openClip) return;
        if (audioSource)
        {
            audioSource.pitch = Random.Range(pitchRandomRange.x, pitchRandomRange.y);
            audioSource.PlayOneShot(openClip, volume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(openClip, door.transform.position, volume);
        }
    }

    void PlayCloseSFX()
    {
        if (!closeClip) return;
        if (audioSource)
        {
            audioSource.pitch = Random.Range(pitchRandomRange.x, pitchRandomRange.y);
            audioSource.PlayOneShot(closeClip, volume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(closeClip, door.transform.position, volume);
        }
    }
}
