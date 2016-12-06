/**
 * Script for managing our main character. 
 * This script manages player's movement, object interaction, 
 * and player's current information. 
 * 
 * Author: Imported from Unity Standard Assets. Modifided by team Nightmare 
 * */
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

        private Camera m_Camera;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private AudioSource m_AudioSource;
        private Collider collides;
        private Light flashlight;


        /***************************************************
         * For storing the items player has
         * ************************************************/
        public static Boolean WestHall_Key;
        public static Boolean MusicRoom_Key;
        public static Boolean RecRoom_Key;
        public static Boolean Dining_Key;
        public static Boolean MusEsc_Key;

        /***************************************************
         * For storing player status
         * ************************************************/
        public Boolean inWardrobe;
        public Boolean isPeeking;

        GameObject refg;
        TextController t;


        // Use this for initialization
        //Modifided by team Nightmare 
        private void Start()
        {
            //get text controller for showing text 
            refg= GameObject.Find("TextController");
            t = refg.GetComponent<TextController>();

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
            collides = GetComponent<Collider>();
            flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();
            inWardrobe = false;
            isPeeking = false;
        }


        // Update is called once per frame
        //Modifided by team Nightmare 
        private void Update()
        {

            //for ray casting 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //find any object within a range of 1 step forward. 
            if (Physics.Raycast(ray, out hit, 2.0f))
            {
                //get the object

                GameObject hitted = hit.collider.gameObject;
                //Debug.Log(hitted.name);
                //if the object is interactable (for instance Rock2) 
                if (hitted.name == "key_1" || hitted.name == "key_2" || hitted.name == "key_3" || hitted.name == "ChinaCabinet"
                    || hitted.name == "Crate (2)")
                {
                    PazzledObject po = hitted.GetComponent<PazzledObject>();
                    po.findKey();
                }
                else if (hitted.tag == "Riddle")//for reading a riddle
                {
                    Note no = hitted.GetComponent<Note>();
                    no.readNote();
                }
                //for entering Wardrobe 
                else if (hitted.tag == "WardrobeDoor")
                {
                    if (!inWardrobe)
                        t.textUpdate("[Press E to Enter Wardrobe]");
                    else
                        t.textUpdate("[Press E to Leave Wardrobe]\n[Press Q to Peek Out]");
                    float doorDistance;
                    //for getting in to wardrobe 
                    if (!inWardrobe && Input.GetKeyDown(KeyCode.E))
                    {
                        t.textClear();
                        doorDistance = Vector3.Distance(transform.position, hitted.transform.position);
                        Debug.Log(doorDistance);
                        transform.Translate(Vector3.forward * (doorDistance));
                        inWardrobe = true;
                    }
                    //for peeking
                    else if (inWardrobe && Input.GetKeyDown(KeyCode.Q) && !isPeeking)
                    {
                        isPeeking = true;
                        flashlight.enabled = false;
                        t.textClear();
                        GetComponentInChildren<Camera>().enabled = false;
                        hitted.GetComponentInChildren<Camera>().enabled = true;
                        t.textUpdate("[Press Q to Stop Peeking]");

                    }
                    //stop peeking 
                    else if (Input.GetKeyDown(KeyCode.Q) && isPeeking)
                    {
                        isPeeking = false;
                        flashlight.enabled = true;
                        t.textClear();
                        hitted.GetComponentInChildren<Camera>().enabled = false;
                        GetComponentInChildren<Camera>().enabled = true;
                        t.textUpdate("[Press Q to Stop Peeking]");

                    }
                    //get out from wardrobe 
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        t.textClear();
                        transform.Translate(Vector3.forward * 3);
                        inWardrobe = false;
                    }
                }
            }
            //for changing the flashlight intensity 
            if (Input.GetKeyDown(KeyCode.F)) {
                if (flashlight.intensity == 0)
                    flashlight.intensity = 3;
                else
                    flashlight.intensity = 0;
            }

            //for clearing the text pop up
            if (Input.GetMouseButtonDown(1))
                t.condUpdate();
            //if (Input.GetMouseButtonDown(0))
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
                t.textClear();
            }
             RotateView();
            // the jump state needs to read here to make sure it is not missed
           
        }


        private void FixedUpdate()
        {
          
                float speed;
                GetInput(out speed);
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

                // get a normal for the surface that is being touched to move along it
                RaycastHit hitInfo;
                Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                                   m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                m_MoveDir.x = desiredMove.x * speed;
                m_MoveDir.z = desiredMove.z * speed;


                m_MoveDir.y = -m_StickToGroundForce;
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
                m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

                ProgressStepCycle(speed);
                UpdateCameraPosition(speed);

                m_MouseLook.UpdateCursorLock();
            
        }
        
        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

        //for entering the door 
        //Added by team Nightmare 
        private void OnTriggerEnter(Collider collide) {
            GameObject refg = GameObject.Find("TextController");
            TextController t=refg.GetComponent<TextController>();

            if (collide.gameObject.name.Equals("DoorEntToWest") &&WestHall_Key||collide.gameObject.tag.Equals("Exit_Door")) {
               transform.position = collide.gameObject.GetComponent<DoorBehaviour>().getExitDoorPosition();
            }
            else if (collide.gameObject.name.Equals("DoorEntToWest") && !WestHall_Key)
            {
                t.textUpdate("The door to the West Hall is locked.");
            }
            else if (collide.gameObject.name.Equals("DoorWestToMus") && MusicRoom_Key)
            {
                transform.position = collide.gameObject.GetComponent<DoorBehaviour>().getExitDoorPosition();
            }
            else if (collide.gameObject.name.Equals("DoorWestToMus") && !MusicRoom_Key)
            {
                t.textUpdate("The door to the Music Room Key is locked.");
            }
            else if (collide.gameObject.name.Equals("DoorRecToWest") && RecRoom_Key)
            {
                transform.position = collide.gameObject.GetComponent<DoorBehaviour>().getExitDoorPosition();
            }
            else if (collide.gameObject.name.Equals("DoorRecToWest") && !RecRoom_Key)
            {
                t.textUpdate("The door locked behind you!");
            }
            else if (collide.gameObject.name.Equals("DoorMusToWest") && MusEsc_Key)
            {
                transform.position = collide.gameObject.GetComponent<DoorBehaviour>().getExitDoorPosition();
            }
            else if (collide.gameObject.name.Equals("DoorMusToWest") && !MusEsc_Key)
            {
                t.textUpdate("The door locked behind you.");
            }
            else if (collide.gameObject.name.Equals("DoorDinToEast") && !Dining_Key)
            {
                t.textUpdate("The door to the East Hall is locked.");
            }
            else if (collide.gameObject.name.Equals("DoorDinToEast") && Dining_Key)
            {
                transform.position = collide.gameObject.GetComponent<DoorBehaviour>().getExitDoorPosition();
            }
            else if(collide.gameObject.tag.Equals("Door_out") )//for outside door. 
            {
                Application.LoadLevel("MainFloor");

            }
            //if all keys have been collected 

            else if (collide.gameObject.name.Equals("MainDoor") && isCollected())
            {
                Application.LoadLevel("Ending");
            }
            //check to see if all key has been collected 
            else if (collide.gameObject.name.Equals("MainDoor") && !isCollected())
            {
                t.textUpdate("The door to outside is locked! \nYou'll need to collect all the keys to escape from here!");
            }
           
            else if (collide.gameObject.tag.Equals("Enemy")) //when enemy attacked player 
            {
                MusicRoom_Key = false;
                RecRoom_Key = false;
                WestHall_Key = false;
                Application.LoadLevel("DeathScreen");  
            }

        }
        //for checking if all keys have been collected
        private Boolean isCollected()
        {
            if (WestHall_Key && MusicRoom_Key && RecRoom_Key && Dining_Key && MusEsc_Key)
            {
                return true;
            }
            else
                return false;
        }
    }
}
