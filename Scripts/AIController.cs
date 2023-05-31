using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    Transform AI_Striker_Position = null;

    [SerializeField]
    GameObject AI_Striker = null;

    [SerializeField]
    GameObject Striker_location = null;

    [SerializeField]
    GameObject Helper_Arrow_ = null;

    GameController Gc;
    bool StrikerForce;
    public bool Ready_to_Strike =false;
    float forceValue_Striker_ = 10f;
    float Value; //to randomly set the striker!
    bool force_once; //to check if the force is being applied only once.
    Vector2 direction;

    float angle;
    // Start is called before the first frame update
    void Start()
    {
        Gc = FindObjectOfType<GameController>();
        force_once = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AI_Strike()
    {
        
        //Debug.Log("AI called!");
        if (Ready_to_Strike == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            if (hit.collider)
            {
                if (hit.collider.name == "striker")
                {
                    //Debug.Log("Active"); 
                    Value = Random.Range(-1.2f, 1.18f);
                    AI_Striker_Position.position = new Vector3(Value, -1.4f, -0.1f); //to change the striker position!
                    Helper_Arrow_.SetActive(true); //to show in which direction it is being applied.
                    angle = Random.Range(0f, 360f) * Mathf.Rad2Deg;
                    
                    StartCoroutine(Wait_For_Rotation());
                }
                
            }
        }else if (Ready_to_Strike == true){   
            if(force_once == true) {
                AI_Striker.GetComponent<Rigidbody2D>().AddForce(direction * forceValue_Striker_, ForceMode2D.Impulse); //to make the force sudden.
                force_once = false;                                                                                        
            }
            //so that the force is applied only once here!
                Helper_Arrow_.SetActive(false);
                StartCoroutine(Wait_AI());
        }
    }
    IEnumerator Wait_AI() // to make the ai wait after it is striked
    {
        yield return new WaitForSeconds(6f);
        //Debug.Log("For AI");
        AI_Striker.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        AI_Striker.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        AI_Striker.transform.position = Striker_location.transform.position;
        Ready_to_Strike = false;
        force_once = true;

        //Activating the human player!
        Gc.isPlayer = true;

        Gc.ActivePlayer(Gc.AIActive, false);
        Gc.ActivePlayer(Gc.HumanActive, true);

        //RESETTING THE FORCE ON THE PAWNS ONCE THE STRIKER HAS STRIKED!
        Gc.Reset_Force(Gc.White_Pawns);
        Gc.Reset_Force(Gc.Black_Pawns);
        Gc.Reset_Force(Gc.Red_Pawn);

    }
    IEnumerator Wait_For_Rotation() //waiting for it to decide in which direction it wants to rotate.
    {
        yield return new WaitForSeconds(2f);
        Ready_to_Strike = true;
        direction = Helper_Arrow_.transform.rotation * Vector2.right;
        direction.Normalize();
    }
}
