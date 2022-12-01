using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAnimator : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;
    [SerializeField] private Vector3 _currentPosition;                      //variable stores the current location of the AI
    [SerializeField] private Vector3 _lastPos;                              //stores the previous location of the AI
    public GameObject ai;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();                         //Get an Animator
        _agent = GetComponent<NavMeshAgent>();                              //Get an Nav Mesh Agent
    }

    void Update()
    {
        
        if (ai.transform.position == _lastPos)                              //if the ai hasnt moved
        {
            _anim.SetBool("isWalking", false);                              //play stationary animation
        }
        else
        {
            _anim.SetBool("isWalking", true);                               //play walking animation
        }
         _lastPos = ai.transform.position;                                  //set the ai's last position their current position
    }
}
