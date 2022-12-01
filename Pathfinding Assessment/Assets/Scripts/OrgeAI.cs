using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OrgeAI : MonoBehaviour
{
    [SerializeField] private Transform _key;                                                    //stores the location of the key
    private NavMeshAgent _agent;                                                                
    [SerializeField] private Transform _WinZone;                                                //stores the location of the winzone
    [SerializeField] private Objective _objective;                  
    public bool keyCollected = false;                                                           //helps determine if the key has been collected or not
    [SerializeField] private Vector3 currentPosition;                                           //keeps track of the Orges current position
    public GameObject destroyedKey;                                                             //stores the game object key that needs to be destroyed
    public GameObject destroyedWall;                                                            //stores the game object wall that needs to be destroyed

    public enum Objective                                                                       //for the different states
    {
        CollectingKey,                                                                          //collecting the key
        ToZone,                                                                                 //going to the winzone
    }
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();                                                  //gets the Nav Mesh Agen
    }

    void Update()
    {
        CurrentObjective();                                                                     //runs the function below
        currentPosition = GameObject.FindGameObjectWithTag("Orge").transform.position;          //tracks the current position of the Orge
    }

    private void CurrentObjective()                                                             //responsible for the current state the orge is in
    {
        switch(_objective)
        {
            case Objective.CollectingKey:
            StartCoroutine(CollectingKeyObj());
            break;
            case Objective.ToZone:
            StartCoroutine(ToWinezoneObj());
            break;
            default:
            Debug.LogWarning("Something went wrong with OrgeAI");
            break;
        }
    }

    public void CollectingKey()                                                                 //function responsible for getting the ai to collect the key
    {
        if (_key != null)                                                                       //if variable "_key" is not null then run
        {
            _agent.destination = _key.position;                                                 //sets the orge's destination to whereever the key is
        }

        if (_key != null)
        {
            if (currentPosition == _key.position)                                                   //if orge is ontop of key
            {
                _key = null;                                                                        //set key to null
                keyCollected = true;                                                                //set the boolean to true, allowing the ai to enter next state
                Destroy(destroyedKey);                                                              //destroys game object key
                Destroy(destroyedWall);                                                             //destroys game object Door
            }
        }
        
    }

    public void ToWinzone()                                                                     //function responsible for getting the orge to go to the winzone
    {
        _agent.destination = _WinZone.position;                                                 //sets the orge's destination to whereever the winzone is
    }

    private IEnumerator CollectingKeyObj()                                                      //Responsible for determining if orge should switch states or continue running set function
    {
        while (_objective == Objective.CollectingKey)                                           //while the orge is in collecting key state run the following
        {
            if (keyCollected == true)                                                           //if the key has been collected
            {
                _objective = Objective.ToZone;                                                  //go onto the next state 
            }

            CollectingKey();                                                                    //Run "CollectingKey" function is key hasn't been collected
            yield return null;  
        }
    }

    private IEnumerator ToWinezoneObj()                                                         //Responsible for determining if orge should switch states or continue running set function
    {
        while (_objective == Objective.ToZone)                                                  //while the orge is in go to winzone state run the following
        {
            ToWinzone();                                                                        //run "ToWinZone" function
            yield return null;
        }
    }
}