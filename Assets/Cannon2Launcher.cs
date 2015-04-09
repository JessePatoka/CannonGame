using UnityEngine;
using System.Collections;

public class Cannon2Launcher : MonoBehaviour {
    private CannonScript cScript;
    public float cannonForce;
    private GameObject cannon;
    private int playercounter;
    // Use this for initialization
    void Start ()
    {
        cannon = GameObject.Find("Cannon2");
        cScript = (CannonScript)cannon.GetComponent<CannonScript>();
        playercounter = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerRigidBody = other.transform.root.gameObject.GetComponent<Rigidbody2D>();
            Destroy(other.transform.root.gameObject);

            if (playercounter < 2)
            {
                playercounter++;
                cScript.Fire(cannonForce * (playerRigidBody.velocity.magnitude / 10));
            }
        }
    }
}
