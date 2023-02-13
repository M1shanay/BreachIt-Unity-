<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpottedEnemyAreaScript _spottedEnemyArea;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_spottedEnemyArea.GetIsEnemySpotted)
        {
<<<<<<< Updated upstream

=======
            
>>>>>>> Stashed changes
        }
    }

    private void OnEnemySpotted() 
    {
        
    }
}