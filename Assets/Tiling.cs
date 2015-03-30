using UnityEngine;
using System.Collections;
[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;
	public bool reverseScale = false; //used if the object is not tilable

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake() 
	{
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () 
	{
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
	if (!hasALeftBuddy || !hasARightBuddy) 
		{
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
			float edgeVisibilePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

			//can we see the edge of the lement and calling makenewbuddy if we can
			if (cam.transform.position.x >= edgeVisibilePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy(1);
				hasARightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBuddy(-1);
				hasALeftBuddy = true;
			}
		}
	}
	/// <summary>
	/// Makes the new buddy.
	/// </summary>
	/// <param name="rightOrLeft">Right or left.</param>
	void MakeNewBuddy(int rightOrLeft)
	{
		//calculating new position for our new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		//instantiating our new buddy and storing him in an variable
		Transform newBuddy = (Transform)Instantiate (myTransform, newPosition, myTransform.rotation);

		if(reverseScale)
		{
			newBuddy.localScale = new Vector3(newBuddy.localScale.x*-1,newBuddy.localScale.y,newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;

		if(rightOrLeft > 0)
		{
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else
		{
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}
