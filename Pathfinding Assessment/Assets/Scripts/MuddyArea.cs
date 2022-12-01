using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshModifierVolume))]

public class MuddyArea : MonoBehaviour
{
    [SerializeField] private NavMeshModifierVolume volume;

    private void Awake()
    {
        volume = GetComponent<NavMeshModifierVolume>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent Agent))
        {
            if (volume.AffectsAgentType(Agent.agentTypeID))
            {
                float CostModifier = NavMesh.GetAreaCost(volume.area);
                Agent.speed /= CostModifier;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent> (out NavMeshAgent Agent))
        {
            float CostModifier = NavMesh.GetAreaCost(volume.area);
            Agent.speed *= CostModifier;

        }
    }
}
