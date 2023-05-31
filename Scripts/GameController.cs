using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Transform[] White_Locations;

    [SerializeField]
    Transform[] Black_Locations;

    [SerializeField]
    Transform Red_Locations;

    [SerializeField]
    Transform Striker_Location;

    [SerializeField]
    public GameObject[] White_Pawns;

    [SerializeField]
    public GameObject[] Black_Pawns;
    
    [SerializeField]
    public GameObject Red_Pawn; 
    
    [SerializeField]
    GameObject Striker_;

    [SerializeField]
    public GameObject HumanActive = null;

    [SerializeField]
    public GameObject AIActive = null;

    [SerializeField]
    public Text Score_Human;
    
    [SerializeField]
    public Text Score_AI;

    public bool isPlayer;
    TriggerDetection SCORES_;

    // Start is called before the first frame update
    void Start()
    {
        isPlayer = true;
        SCORES_ = FindObjectOfType<TriggerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        Score_Counter();

    }

    public void ResetGame()
    {
        isPlayer = true;
        Reset_PAWNS(White_Pawns, White_Locations);
        Reset_PAWNS(Black_Pawns, Black_Locations);
        Reset_Red();
        Reset_Striker(Striker_);
    }

    void Reset_PAWNS(GameObject[] PAWNS_, Transform[] LOCATIONS_)
    {
        for(int i=0; i<PAWNS_.Length; i++)
        {
          PAWNS_[i].transform.position = LOCATIONS_[i].position;
          PAWNS_[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
          PAWNS_[i].GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }

    }
    void Reset_Red()
    {
        Red_Pawn.transform.position = Red_Locations.position;
        Red_Pawn.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Red_Pawn.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    public void Reset_Striker(GameObject Striker_)
    {
        Striker_.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Striker_.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        Striker_.transform.position = Striker_Location.position;
    }

    public void Reset_Force(GameObject[] PAWNS_)
    {
        //to pause the force on the pawns once the striker hit them.
        for (int i = 0; i < PAWNS_.Length; i++)
        {
            GameObject pawnObject = PAWNS_[i];

            if (pawnObject != null && pawnObject.activeInHierarchy)
            {
                Rigidbody2D rb = pawnObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
        }
    }
    public void Reset_Force(GameObject PAWN_)
    {
        //to pause the force on the pawn once the striker hit them.
        if (PAWN_ != null && PAWN_.activeInHierarchy)
        {
            Rigidbody2D rb = PAWN_.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            else
            {
                Debug.LogWarning("Gameobject destoryed!");
            }
        }
        else
        {
            Debug.LogWarning("GameObject Destoryed");
        }
    }

    //to set active/disable the current player accordingly!
    public void ActivePlayer(GameObject CURRENT_PLAYER_BUBBLE, bool VAR)
    {
        CURRENT_PLAYER_BUBBLE.SetActive(VAR);
    }

    public void Score_Counter()
    {
        Score_AI.text ="Score: " + SCORES_.Score_AI.ToString();
        Score_Human.text = "Score: " + SCORES_.Score_Human.ToString();
    }
}
