using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonScript : MonoBehaviour
{

    public GameObject playerObject;
    private Text text;
    private Transform cannonButt;
    private Transform cannonNose;
    private Rigidbody2D playerRigidBody;
    private Camera2DFollow camScript;
    private ManageGame manageGameScript;

    public GameObject mainCamera;
    private bool isMovingClockwise = false;
    private List<Rigidbody2D> playerList;


    // Use this for initialization
    void Start()
    {
        playerList = new List<Rigidbody2D>();
        cannonButt = transform.Find("CannonButt");
        cannonNose = transform.Find("CannonNose");
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
        //text = (Text)GameObject.Find("DistanceText").GetComponent<Text>();


        camScript = (Camera2DFollow)mainCamera.GetComponent<Camera2DFollow>();
        manageGameScript = (ManageGame)GameObject.Find("_GM").GetComponent<ManageGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerList.Count > 15)
        {
            if (playerList[0] != null)
            {
                Destroy(playerList[0].transform.root.gameObject);
                playerList.RemoveAt(0);
            }
        }

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

        playerList.Add(pFab);

        camScript.target = pFab.transform;

        pFab.AddForce(dir * force);

        pFab.AddTorque(force);

        manageGameScript.ShotsFired();

    }

    private float Angle
    {
        get
        {
            return Mathf.Atan2(cannonNose.position.y - cannonButt.position.y, cannonNose.position.x - cannonButt.position.x) * 180 / Mathf.PI;
        }
    }

}
