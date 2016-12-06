using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyAI : MonoBehaviour
{
    public float viewRange; //distance at which the object will turn and look toward the target
    public float attackRange; //distance at which the object will move toward the target
    public float speed; //speed at which the object will move toward the target
    public GameObject target; //the target of the object - set in inspector
    public AudioClip warningSound; //sound the enemy plays when in view range of target
    public AudioClip attackSound; //sound the enemy plays when in attack range of target
    private float targetDistance;
    private Rigidbody rigidbody;
    private bool hasWarningGrowled = false;
    private bool hasAttackScreamed = false;
    private FirstPersonController fpsc;
    private GameObject blurUI, attackUI;
    private string type;

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("FPSController");
        fpsc = target.GetComponent<FirstPersonController>();
        blurUI = GameObject.Find("blurWarning");
        attackUI = GameObject.Find("blurAttack");
        type = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance < attackRange && !fpsc.inWardrobe)
        {
            lookAtTarget();
            attackPlayer();

            //warn player 
            //            GameObject refg = GameObject.Find("TextController");
            //            TextController t = refg.GetComponent<TextController>();

            //            t.textUpdate("Enemy is close to you!!!!");
            if (type.Equals("Shade"))
            {
                blurUI.GetComponent<SpriteRenderer>().enabled = false;
                attackUI.GetComponent<SpriteRenderer>().enabled = true;
            }



            //play attack sound
            //AudioSource.Stop();
            if (!hasAttackScreamed)
            {
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
                hasAttackScreamed = true;
            }
            //hasWarningGrowled = false;
        }
        else if (targetDistance < attackRange && fpsc.inWardrobe)
        {
            lookAtTarget();
            leavePlayer();
        }
        else if (targetDistance < viewRange && fpsc.inWardrobe)
        {
            if (type.Equals("Shade"))
            {
                blurUI.GetComponent<SpriteRenderer>().enabled = false;
                attackUI.GetComponent<SpriteRenderer>().enabled = false;
            }
            lookAtTarget();
            leavePlayer();
        }
        else if (targetDistance < viewRange) //if can see target (player)
        {
            lookAtTarget();
            if (type.Equals("Shade"))
            {
                attackUI.GetComponent<SpriteRenderer>().enabled = false;
                blurUI.GetComponent<SpriteRenderer>().enabled = true;
            }

            //play warning clip
            if (!hasWarningGrowled)
            {
                AudioSource.PlayClipAtPoint(warningSound, transform.position);
                hasWarningGrowled = true;
            }
            //hasAttackScreamed = false;
        }
        else if (targetDistance > viewRange && hasWarningGrowled)
        {
            hasWarningGrowled = false;
            hasAttackScreamed = false;
            if (type.Equals("Shade"))
            {
                blurUI.GetComponent<SpriteRenderer>().enabled = false;
                attackUI.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void attackPlayer()
    {
        transform.position += transform.forward * Time.deltaTime * speed; //move towards (already facing) target
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void leavePlayer()
    {
        transform.position -= transform.forward * Time.deltaTime * speed/4; //move towards (already facing) target
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void OnColliderEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            FirstPersonController.MusicRoom_Key = false;
            FirstPersonController.RecRoom_Key = false;
            FirstPersonController.WestHall_Key = false;
            Application.LoadLevel("MainMenu");
        }
    }

    void lookAtTarget()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
}
