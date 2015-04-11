using UnityEngine;
using System.Collections;

public class EnableScript : MonoBehaviour {

	// Use this for initialization
	public void Enable (bool b)
    {
        gameObject.GetComponent<Canvas>().enabled = b;
	}

}
