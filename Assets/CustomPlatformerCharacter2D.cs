using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    public class CustomPlatformerCharacter2D : MonoBehaviour
    {
        private Camera2DFollow camScript;
        private GameObject mainCamera;

        [SerializeField]
        private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        //private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private Text distanceText;
        private Text bestText;
        private Canvas submitButton;
        private int lastHighScoreSubmitted;


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            distanceText = (Text)GameObject.Find("DistanceText").GetComponent<Text>();
            bestText = (Text)GameObject.Find("BestText").GetComponent<Text>();
            submitButton = (Canvas)GameObject.Find("SubmitButtonCanvas").GetComponent<Canvas>();
            mainCamera = GameObject.Find("MainCamera");
            camScript = (Camera2DFollow)mainCamera.GetComponent<Camera2DFollow>();
            lastHighScoreSubmitted = 0;
        }
        private void Update()
        {
            if (camScript.target == gameObject.transform)
            {
                int dudeLocation = (int)((m_Rigidbody2D.position.x + 8.25) * 10);
                distanceText.text = "Distance: " + dudeLocation;

                int bestLocation = 0;
                int.TryParse(bestText.text, out bestLocation);

                if (dudeLocation > bestLocation)
                {
                    bestText.text = dudeLocation.ToString();

                    if (dudeLocation > lastHighScoreSubmitted)
                    {
                        submitButton.enabled = true;
                }
            }
            else
            {
                //Destroy(GetComponent<Rigidbody2D>());
            }
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            //m_Anim.SetBool("Ground", m_Grounded);

            //// Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void HighscoreSubmitted()
        {
           Int32.TryParse(bestText.text, out lastHighScoreSubmitted);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
