using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform[] brains;                                                                  //stores an array of brain locations as Vector3s
    private Vector3 _currentPosition;                                                           //stores the current location of the zombie
    [SerializeField] private Objective _objective;
    [SerializeField] private int collectedBrains = 0;                                           //keeps track of how many brains the zombie has collected
    [SerializeField] private int selectedBrain = 1;                                             //keep track of which brain the zombie is currently after
    [SerializeField] private Transform _winzone;                                                //stores the location of the winzone
    [SerializeField] private GameObject[] ObjToDestroy;                                         //stores an array of the brains as Game objects 


    

    public enum Objective                                                                       //enum of the different zombie states
    {
        CollectingBrains,
        ToZone,

    }
    void Start()
    {
        
        _agent = GetComponent<NavMeshAgent>();                                                  //gets the Nav Mesh Agent
    }

    void Update()
    {
        CurrentObjective();                                                                     //runs the function below
        _currentPosition = GameObject.FindGameObjectWithTag("Zombie").transform.position;       //tracks the current position of the Orge

    }

    private void CurrentObjective()                                                             //responsible for the current state the orge is in
    {
        switch (_objective)                                                                     //switch statement organising the states
        {
            case Objective.CollectingBrains:
                StartCoroutine(CollectingBrainsObj());
                break;
            case Objective.ToZone:
                StartCoroutine(ToWinZoneObj());
                break;
            default:
                Debug.LogWarning("Something went wrong with OrgeAI");
                break;
        }
    }

    public void CollectingBrains()                                                              //function used to get the zombie to collect the brains
    {
        if (brains != null)                                                                     //if brains is not null
        {
            Vector3 brainPosition = brains[selectedBrain].position;                             //gets the location of the current brain the zombie is after
            _agent.destination = brainPosition;                                                 //sets the zombies destination to the current brain
            if (_currentPosition == brainPosition)                                              //if zombie is ontop of brain run the following
            {
                Destroy(ObjToDestroy[selectedBrain]);                                           //Destroy the current brain
                collectedBrains++;                                                              //plus one to total brains collected
                selectedBrain++;                                                                //move on to next brain
            }
        }
    }

    public void ToWinzone()                                                                     //responsible for telling the zombie where to go to get to the winzone
    {
        _agent.destination = _winzone.position;                                                 //sets the zombies destination to the winzones location
    }

    private IEnumerator CollectingBrainsObj()                                                   //responsible for determining if the zombies should keep collecting brains or move to next state
    {
        while (_objective == Objective.CollectingBrains)                                        //while the zombie is in Collectin brains state do the following
        {
            if (collectedBrains == 3)                                                           //if the zombie has collected 3 brains do the following
            {
                _objective = Objective.ToZone;                                                  //set the zombies current objective to "ToZone"
            }
            CollectingBrains();                                                                 //Run the function above
            yield return null;                                                                  //prevents from getting stuck in a loop
        }
    }

    private IEnumerator ToWinZoneObj()                                                          //responsible for determining if the zombie should go to the winzone
    {
        while (_objective == Objective.ToZone)                                                  //while the objective is set to go to winzone do the following
        {
            ToWinzone();                                                                        //run function above
            yield return null;                                                                  //prevents from getting stuck in a loop
        }
    }
}
