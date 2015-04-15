using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class TombGenerator : MonoBehaviour {

    public Transform TombPrefab;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Generate()
    {
        if (Tombs.Count > 0)
        {
            foreach(Tomb t in Tombs)
            {
                var position = new Vector3(t.XPos, t.YPos + 1f);
                var rotation = new Quaternion();
                var TombFab = ((Transform)Instantiate(TombPrefab, position, rotation));

                TombFab.GetComponentInChildren<TextMesh>().text = t.Name;
            }
        }
    }

    public List<Tomb> Tombs { get; set; }
}
