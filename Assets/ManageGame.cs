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
    public Transform BombPrefab;

    private PowerMax powerMaxScript;
    private Camera2DFollow camScript;
    private Text distanceText;
    private Vector3 bomb1position;
    private Vector3 bomb2position;
    private Vector3 bomb3position;
    private Quaternion bomb1rotation;
    private Quaternion bomb2rotation;
    private Quaternion bomb3rotation;
    // Use this for initialization
    void Start()
    {
        distanceText = (Text)GameObject.Find("DistanceText").GetComponent<Text>();
        distanceText.text = "Distance: 0";
        camScript = (Camera2DFollow)mainCamera.GetComponent<Camera2DFollow>();
        powerMaxScript = (PowerMax)gameObject.GetComponent<PowerMax>();

        bomb1position = new Vector3(Bomb1.transform.position.x, Bomb1.transform.position.y);
        bomb2position = new Vector3(Bomb2.transform.position.x, Bomb2.transform.position.y);
        bomb3position = new Vector3(Bomb3.transform.position.x, Bomb3.transform.position.y);
        bomb1rotation = new Quaternion(Bomb1.transform.rotation.x, Bomb1.transform.rotation.y, Bomb1.transform.rotation.z, Bomb1.transform.rotation.w);
        bomb2rotation = new Quaternion(Bomb2.transform.rotation.x, Bomb2.transform.rotation.y, Bomb2.transform.rotation.z, Bomb2.transform.rotation.w);
        bomb3rotation = new Quaternion(Bomb3.transform.rotation.x, Bomb3.transform.rotation.y, Bomb3.transform.rotation.z, Bomb3.transform.rotation.w);
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
            if(Bomb1 != null) Destroy(Bomb1);
            if (Bomb2 != null) Destroy(Bomb2);
            if (Bomb3 != null) Destroy(Bomb3);

            GameObject bomb1 = ((Transform)Instantiate(BombPrefab, bomb1position, bomb1rotation)).gameObject;
            GameObject bomb2 = ((Transform)Instantiate(BombPrefab, bomb2position, bomb2rotation)).gameObject;
            GameObject bomb3 = ((Transform)Instantiate(BombPrefab, bomb3position, bomb3rotation)).gameObject;

            Bomb1 = bomb1;
            Bomb2 = bomb2;
            Bomb3 = bomb3;

            //reset powerbar
            powerMaxScript.barDisplay = 0;
            powerMaxScript.shooting = false;
        }
    }
}
