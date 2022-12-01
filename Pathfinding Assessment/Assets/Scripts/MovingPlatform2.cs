using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingPlatform2 : MonoBehaviour
{
    [SerializeField] private Vector3[] Positions;                                           //holds the positions the platform with move between
    [SerializeField] private float DockDuration = 2f;                                       //how long the platform pauses
    [SerializeField] private float MoveSpeed = 0.01f;                                       //how long the platform moves each frame


    private List<NavMeshAgent> AgentsOnPlatform = new List<NavMeshAgent>();                 //holds a list of whatever agents are on the platform

    private void Start()
    {
        StartCoroutine(MovePlatform());                                                      //Starts the platform moving
    }   

    private void OnTriggerEnter(Collider other)                                             //responsible for getting the ai on the platform
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))                    //if an object with a nav mesh agent gets on the platform run the following 
        {
            AgentsOnPlatform.Add(agent);                                                    //adds the agent to the list if it steps onto the platform
        }
    }

    private void OnTriggerExit(Collider other)                                              //responsible for getting the ai off the platform
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))                    //if the agent left the platform run the following
        {
            AgentsOnPlatform.Remove(agent);                                                 //removes them from the list
        }
    }

    private IEnumerator MovePlatform()                                                      //responsible for moving the platform
    {
        transform.position = Positions[0];                                                  //sets the current position the platform is moving to
        int positionIndex = 0;                                                              //holds the previous position index
        int lastPositionIndex;                                                              //sets the platform to wait however long the DockDuration variable has set
        WaitForSeconds Wait = new WaitForSeconds(DockDuration);                             //keeps it running constantly

        while (true)
        {
            lastPositionIndex = positionIndex;                                              //sets the current position index to the previous position index
            positionIndex++;                                                                //plus 1 to the position index
            if (positionIndex >= Positions.Length)                                          //if the position index is a equal or higher number to the amount of positions index run the following
            {
                positionIndex = 0;                                                          //sets the position index back to 0
            }

            Vector3 platformMoveDirection = (Positions[positionIndex] - Positions[lastPositionIndex]).normalized;                   //determines the direction of the platform
            float distance = Vector3.Distance(transform.position, Positions[positionIndex]);                                        //determines how far the platform goes
            float distanceTraveled = 0;                                                                                             //resets the distance travelled to 0
            while (distanceTraveled < distance)
            {
                transform.position += platformMoveDirection * MoveSpeed;                                                            //moves the platform
                distanceTraveled += platformMoveDirection.magnitude * MoveSpeed;                                                    //determines how far the platform moved across 1 frame
                        
                for (int i = 0; i < AgentsOnPlatform.Count; i++)                                                                    //for all agents on the platform
                {
                    AgentsOnPlatform[i].Warp(AgentsOnPlatform[i].transform.position + platformMoveDirection * MoveSpeed);           //move the agents with the platform
                }

                yield return null;                                                                                                  //prevents from getting stuck in a loop
            }

            yield return Wait;                                                                                                      //waits the set time
        }
    }
}
