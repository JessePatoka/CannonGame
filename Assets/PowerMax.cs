using UnityEngine;
using System.Collections;
using System;

public class PowerMax : MonoBehaviour
{
    public float fullWidth = 200;
    public bool increasing = false;
    public bool shooting = false;
    public float barSpeed = 25;
    public ParticleEmitter blastPart;
    public Light cannonLight;
    public Transform spawnPos;
    public float shotForce = 30;
    public AudioClip cannonBlast;

    public float barDisplay; //current progress
    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;
    public GUIStyle progress_empty, progress_full;

    private GameObject cannon;
    private CannonScript cScript;
    private bool decreasing;

    public GameObject Cannon;


    // Use this for initialization
    void Start()
    {
        cannon = Cannon;
        cScript = (CannonScript)cannon.GetComponent<CannonScript>();

        barDisplay = 0;

        if (increasing)
        {
            PowerUp();
        }
    }
    void OnGUI()
    {
        //http://answers.unity3d.com/questions/11892/how-would-you-make-an-energy-bar-loading-progress.html
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex, progress_empty);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    // Update is called once per frame
    void Update()
    {
        //http://www.unity3dstudent.com/2011/01/worms-style-power-bar/
        if (!shooting && Input.GetButtonDown("Jump"))
        {
            increasing = true;
            PowerUp();
        }

        if (!shooting && Input.GetButton("Jump"))
        {
            //audio.Play();
            if (barDisplay >= 1)
            {
                increasing = false;
                decreasing = true;
            }
            else if (barDisplay <= 0)
            {
                increasing = true;
                decreasing = false;
            }
        }

        if (!shooting && Input.GetButtonUp("Jump"))
        {
            increasing = false;
            decreasing = false;
            Shoot(barDisplay);
        }


        if (increasing)
            PowerUp();
        else if (decreasing)
            PowerDown();

    }


    private void PowerDown()
    {
        barDisplay -= Time.deltaTime * barSpeed;
        barDisplay = Mathf.Clamp(barDisplay, 0, fullWidth);
    }




    private void PowerUp()
    {

        barDisplay += Time.deltaTime * barSpeed;
        barDisplay = Mathf.Clamp(barDisplay, 0, fullWidth);
    }

    private void Shoot(float power)
    {
        shooting = true;

        cScript.Fire(power * shotForce);
    }
}
