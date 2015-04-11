using UnityEngine;
using System.Collections;

public class Cannon2Launcher : MonoBehaviour {
    private CannonScript cScript;
    public float cannonForce;
    private GameObject cannon;
    public bool BUSY;
    // Use this for initialization
    void Start ()
    {
        cannon = GameObject.Find("Cannon2");
        cScript = (CannonScript)cannon.GetComponent<CannonScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!BUSY)
        {
            if (other.gameObject.tag == "Player")
            {
                BUSY = true;
                var playerRigidBody = other.transform.root.gameObject.GetComponent<Rigidbody2D>();
                Destroy(other.transform.root.gameObject);
                cScript.Fire(cannonForce * (playerRigidBody.velocity.magnitude / 10));
            }
        }
    }
}
