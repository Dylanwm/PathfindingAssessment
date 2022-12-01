using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _winzone;                                        //holds the location of the winzone
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();                                          //get nav mesh agent
    }

    void Update()
    {
        ToWinzone();                                                                    //runs function below
    }

    private void ToWinzone()                                                            //function responsible for telling the ai to go to he winzone
    {
        _agent.destination = _winzone.position;                                         //tells the ai to go to the winzone location
    }
}
