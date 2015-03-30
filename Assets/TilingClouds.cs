using UnityEngine;
using System.Collections;
[RequireComponent (typeof(SpriteRenderer))]

public class TilingClouds : MonoBehaviour {

	public int offsetX = 2;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

    private float camHalfHorizontalWidth { get; set; }

    void Awake() 
	{
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start ()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () 
	{
			camHalfHorizontalWidth = (cam.orthographicSize * Screen.width/Screen.height);
			float edgePastCamLeft = myTransform.position.x + (spriteWidth/2);

			//can we see the edge of the lement and calling makenewbuddy if we can
			if ((cam.transform.position.x - camHalfHorizontalWidth) >= edgePastCamLeft)
			{
                MovePosition();
			}
	}
	/// <summary>
	/// Makes the new buddy.
	/// </summary>
	/// <param name="rightOrLeft">Right or left.</param>
	void MovePosition()
	{
		//calculating new position for our new buddy
		Vector3 newPosition = new Vector3 (cam.transform.position.x + spriteWidth + (camHalfHorizontalWidth*2), myTransform.position.y, myTransform.position.z);
        //instantiating our new buddy and storing him in an variable
        myTransform.position = newPosition;
	}
}
