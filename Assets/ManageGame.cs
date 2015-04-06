using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    private Text distanceText;
    // Use this for initialization
    void Start()
    {
        distanceText = (Text)GameObject.Find("DistanceText").GetComponent<Text>();
        distanceText.text = "Distance: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
    }
}
