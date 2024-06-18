using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField] 
    private SpriteRenderer SelectionSprite;

    void Awake()
    {
        SelectionManager.Instance.AvaliableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 Position)
    {
        if (Agent != null) // Verificar si el NavMeshAgent es válido
        {
            Agent.SetDestination(Position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is null or has been destroyed.");
        }
    }

    public void OnSelected()
    {
        if (SelectionSprite != null)
        {
            SelectionSprite.gameObject.SetActive(true); 
        }
    }

    public void OnDeselected()
    {
        if (SelectionSprite != null)
        {
            SelectionSprite.gameObject.SetActive(false);
        }
    }
}
