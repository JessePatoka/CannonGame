using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(transform.gameObject);

        //if (FindObjectsOfType(GetType()).Length > 1)
        //{
        //    Destroy(gameObject);
        //}
    }

}
