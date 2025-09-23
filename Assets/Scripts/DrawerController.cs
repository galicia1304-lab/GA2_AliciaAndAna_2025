using UnityEngine;

public class DrawerController : MonoBehaviour
{
    public bool open = false;
    private Animator animator;
    private bool animComplete = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            OpenDrawer();
            open = false;
        }
    }

    public void OpenDrawer()
    {
        animator.SetTrigger("OpenDrawer");

    }

    public bool isDrawerOpen()
    {
        
        return animComplete;
    }

    public void AnimComplete()
    {
        animComplete = true;
    }
}
