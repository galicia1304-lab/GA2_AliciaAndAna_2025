using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform player;

    public float seekDistance = 20f;
    public GameObject missile;
    public float missileSpeed = 10f;
    public float shotFrequency = 0.2f;

    private bool canShoot = true;
    private float shootTimer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 newDir = new Vector3(player.position.x, player.position.y + 3f, player.position.z);
            transform.LookAt(newDir);
            if (!canShoot )
            {
                shootTimer += Time.deltaTime;
                if (shootTimer > shotFrequency)
                {
                    shootTimer = 0f;
                    canShoot = true;
                }
            }
            if (Vector3.Distance(transform.position, newDir) < seekDistance && canShoot)
            {
                
                canShoot = false;
                GameObject proj = Instantiate(missile, transform.position, transform.rotation);
                proj.transform.rotation = transform.rotation;
                //Vector3 newDir = player.position - transform.position;
                proj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * missileSpeed);
            }
        }
    }
}
