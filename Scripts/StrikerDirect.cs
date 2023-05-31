using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerDirect : MonoBehaviour
{
    [SerializeField]
    Slider StrikerSlider = null;

    [SerializeField]
    Transform Striker_Guide_ = null;

    [SerializeField]
    Transform Point = null;
    
    [SerializeField]
    Transform Location = null;
    
    [SerializeField]
    GameObject Helper_Arrow_ = null;
    
    //[SerializeField]
    //GameObject Striker_ = null;

    GameController Gc;
    AIController AI;

    bool StrikerForce;
    float forceValue_Striker_ = 10f;
    bool STRIKED_ = false;

    Vector2 force; //to check the direction in which i want to apply the force!
    Rigidbody2D Rigid_ = null;
    
    // Start is called before the first frame update

    void Start()
    {
        StrikerSlider.onValueChanged.AddListener(ChangeDirectionOfStriker);
        Rigid_ = GetComponent<Rigidbody2D>();
        Gc = FindObjectOfType<GameController>();
        AI = FindObjectOfType<AIController>();
        Gc.ActivePlayer(Gc.HumanActive, true);
    }

    // Update is called once per frame
    void Update()
    {
        Strike();
    }

    //to change the direction of the striker wrt to the sldier.
    void ChangeDirectionOfStriker(float Value)
    {
        gameObject.transform.position = new Vector3(Value,-1.4f,-0.1f);
    }

    void Strike()
    {//for player!
        
        if (Input.GetMouseButton(1) && Gc.isPlayer)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if (hit.collider) //checking if raycast hits the gameObjects!
            {
                //to make the functionality to show if the stiker is selected or not!
                if (hit.collider.name == "striker")
                {
                    StrikerForce = true;
                    Helper_Arrow_.SetActive(true);
                }
                if (StrikerForce)
                {
                    Striker_Guide_.LookAt(hit.point);
                    //to rotate the helper arrow in the direction of striking.

                    Vector3 ScaleValue = (Vector3)hit.point - transform.position;
                    float angle = Mathf.Atan2(ScaleValue.y, ScaleValue.x) * Mathf.Rad2Deg;
                    Striker_Guide_.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }
        }
        else if (Input.GetMouseButtonUp(1) && Gc.isPlayer)
        {
            Vector3 pointOfContact = new Vector3(Point.position.x - transform.position.x, Point.position.y - transform.position.y, -0.1f);
            StrikerForce = false;
            Rigid_.AddForce((Vector2)pointOfContact * forceValue_Striker_, ForceMode2D.Impulse); //to make the striker strike using force'
            Helper_Arrow_.SetActive(false);//to demolish it!
            STRIKED_ = true;
            if(STRIKED_)
                StartCoroutine(WaitFor8Second());
        }


        if(Gc.isPlayer == false)
        { //since the player isnt playing its ai's turn hence the call!
            Gc.ActivePlayer(Gc.AIActive, true);
            Gc.ActivePlayer(Gc.HumanActive, false);
            AI.AI_Strike();
        }
    }

    IEnumerator WaitFor8Second()
    {
        yield return new WaitForSeconds(8f);
        //Debug.Log("After 8 seconds");
        Rigid_.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Rigid_.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        //RESETTING THE FORCE ON THE PAWNS ONCE THE STRIKER HAS STRIKED!
        Gc.Reset_Force(Gc.White_Pawns);
        Gc.Reset_Force(Gc.Black_Pawns);
        Gc.Reset_Force(Gc.Red_Pawn);

        //RESETTING THE POSITION OF THE STRIKER FOR THE AI TO PLAY!
        gameObject.transform.position = Location.position;

        //DEACTIVATING THE PLAYER TO BE SWITCHED TO THE AI.
        Gc.ActivePlayer(Gc.HumanActive, false);
        Gc.isPlayer = false;
        
    }

    
}
