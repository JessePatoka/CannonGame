using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class ManageGame : MonoBehaviour
{
    public GameObject Bomb1;
    public GameObject Bomb2;
    public GameObject Bomb3;
    public GameObject Cannon1;
    public GameObject mainCamera;

    private PowerMax powerMaxScript;
    private Camera2DFollow camScript;
    private Text distanceText;
    // Use this for initialization
    void Start()
    {
        distanceText = (Text)GameObject.Find("DistanceText").GetComponent<Text>();
        distanceText.text = "Distance: 0";
        camScript = (Camera2DFollow)mainCamera.GetComponent<Camera2DFollow>();
        powerMaxScript = (PowerMax)gameObject.GetComponent<PowerMax>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
            //Application.LoadLevel(0);

            //reset camera
            camScript.target = Cannon1.transform;

            //reset distance
            distanceText.text = "Distance: 0";

            //reset bombs
            var bomb1position = new Vector3(Bomb1.transform.position.x, Bomb1.transform.position.y);
            var bomb2position = new Vector3(Bomb2.transform.position.x, Bomb2.transform.position.y);
            var bomb3position = new Vector3(Bomb3.transform.position.x, Bomb3.transform.position.y);
            var bomb1rotation = new Quaternion(Bomb1.transform.rotation.x, Bomb1.transform.rotation.y, Bomb1.transform.rotation.z, Bomb1.transform.rotation.w);
            var bomb2rotation = new Quaternion(Bomb2.transform.rotation.x, Bomb2.transform.rotation.y, Bomb2.transform.rotation.z, Bomb2.transform.rotation.w);
            var bomb3rotation = new Quaternion(Bomb3.transform.rotation.x, Bomb3.transform.rotation.y, Bomb3.transform.rotation.z, Bomb3.transform.rotation.w);
            Object.Destroy(Bomb1);
            Object.Destroy(Bomb2);
            Object.Destroy(Bomb3);
            GameObject bomb1 = new GameObject();
            GameObject bomb2 = new GameObject();
            GameObject bomb3 = new GameObject();

            

            bomb1 = (GameObject)Instantiate(Bomb1, bomb1position, bomb1rotation);
            bomb2 = (GameObject)Instantiate(Bomb2, bomb2position, bomb2rotation);
            bomb3 = (GameObject)Instantiate(Bomb3, bomb3position, bomb3rotation);

            //reset powerbar
            powerMaxScript.barDisplay = 0;
            powerMaxScript.shooting = false;
        }
    }
}
