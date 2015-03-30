using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonScript : MonoBehaviour
{

    public GameObject playerObject;
    private Text text;
    private Transform cannonButt;
    private Transform cannonNose;
    private Rigidbody2D playerRigidBody;
    private Camera2DFollow camScript;

    private GameObject mainCamera;
    private bool isMovingClockwise = false;


    // Use this for initialization
    void Start()
    {
        cannonButt = transform.Find("CannonButt");
        cannonNose = transform.Find("CannonNose");
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
        //text = (Text)GameObject.Find("DistanceText").GetComponent<Text>();

        mainCamera = GameObject.Find("MainCamera");
        camScript = (Camera2DFollow)mainCamera.GetComponent<Camera2DFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject != GameObject.Find("Cannon2"))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                RotateLeft();

            if (Input.GetKey(KeyCode.RightArrow))
                RotateRight();
        }
        else
        {
            Rotate();
        }
        //text.text = "Distance: " + (int)((playerRigidBody.position.x + 8.25) * 10);
    }

    private void Rotate()
    {
        if (isMovingClockwise)
        {
            transform.Rotate(Vector3.forward, -90 * Time.deltaTime);
            IsBoundHit();

        }
        else
        {
            transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
            IsBoundHit();

        }
    }

    private void IsBoundHit()
    {
        if (Angle >= 90)
        {
            isMovingClockwise = true;
        }

        if (Angle <= 0)
        {
            isMovingClockwise = false;
        }
    }

    void RotateLeft()
    {
        if (Angle < 90)
        {
            transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
            IsBoundHit();
        }
    }

    void RotateRight()
    {
        if (Angle > 0)
        {
            transform.Rotate(Vector3.forward, -90 * Time.deltaTime);
            IsBoundHit();
        }
    }

    public void Fire(float force)
    {
        Vector3 dir = Quaternion.AngleAxis(Angle, Vector3.forward) * Vector3.right;

        Rigidbody2D pFab = (Rigidbody2D)Instantiate(playerRigidBody, cannonNose.position, cannonNose.rotation);

        camScript.target = pFab.transform;

        pFab.AddForce(dir * force);

        pFab.AddTorque(force);

    }

    private float Angle
    {
        get
        {
            return Mathf.Atan2(cannonNose.position.y - cannonButt.position.y, cannonNose.position.x - cannonButt.position.x) * 180 / Mathf.PI;
        }
    }

}
