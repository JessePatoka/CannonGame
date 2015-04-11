using System.Diagnostics;
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

    private Cannon2Launcher cannon2script;
    private PowerMax powerMaxScript;
    private Camera2DFollow camScript;
    private Text distanceText;
    private Vector3 bomb1position;
    private Vector3 bomb2position;
    private Vector3 bomb3position;
    private Quaternion bomb1rotation;
    private Quaternion bomb2rotation;
    private Quaternion bomb3rotation;
    private Canvas submitButton;
    private Canvas submitHS;
    private Canvas validationCanvas;
    private Canvas successCanvas;
    private Stopwatch stopWatch;
    // Use this for initialization
    void Start()
    {
        distanceText = (Text)GameObject.Find("DistanceText").GetComponent<Text>();
        distanceText.text = "Distance: 0";
        camScript = (Camera2DFollow)mainCamera.GetComponent<Camera2DFollow>();
        powerMaxScript = (PowerMax)gameObject.GetComponent<PowerMax>();
        cannon2script = (Cannon2Launcher)GameObject.Find("CannonReceiver").GetComponent<Cannon2Launcher>();

        bomb1position = new Vector3(Bomb1.transform.position.x, Bomb1.transform.position.y);
        bomb2position = new Vector3(Bomb2.transform.position.x, Bomb2.transform.position.y);
        bomb3position = new Vector3(Bomb3.transform.position.x, Bomb3.transform.position.y);
        bomb1rotation = new Quaternion(Bomb1.transform.rotation.x, Bomb1.transform.rotation.y, Bomb1.transform.rotation.z, Bomb1.transform.rotation.w);
        bomb2rotation = new Quaternion(Bomb2.transform.rotation.x, Bomb2.transform.rotation.y, Bomb2.transform.rotation.z, Bomb2.transform.rotation.w);
        bomb3rotation = new Quaternion(Bomb3.transform.rotation.x, Bomb3.transform.rotation.y, Bomb3.transform.rotation.z, Bomb3.transform.rotation.w);

        submitButton = (Canvas)GameObject.Find("SubmitButtonCanvas").GetComponent<Canvas>();
        submitHS = (Canvas)GameObject.Find("SubmitHSCanvas").GetComponent<Canvas>();
        validationCanvas = (Canvas)GameObject.Find("ValidationStringCanvas").GetComponent<Canvas>();
        successCanvas= (Canvas)GameObject.Find("SuccessStringCanvas").GetComponent<Canvas>();
        submitHS.enabled = false;
        submitButton.enabled = false;
        validationCanvas.enabled = false;
        successCanvas.enabled = false;
        stopWatch = new Stopwatch();
        
    }

    // Update is called once per frame
    void Update()
    {
        long stopwatchElapsedMilli = (stopWatch.IsRunning) ? stopWatch.ElapsedMilliseconds : 4000;
        if (Input.GetKey(KeyCode.Escape) && stopwatchElapsedMilli > 3000)
        {
            stopWatch.Start();
            //Application.LoadLevel(0);
            submitHS.enabled = false;
            validationCanvas.enabled = false;
            successCanvas.enabled = false;

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
            cannon2script.BUSY = false;
        }
    }

    public void ShotsFired()
    {
        if(stopWatch.IsRunning)
        {
            stopWatch.Reset();
        }

        stopWatch.Start();

    }
}
