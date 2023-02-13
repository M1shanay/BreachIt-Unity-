using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpottedEnemyAreaScript _spottedEnemyArea;

    void Update()
    {
        if (_spottedEnemyArea.GetIsEnemySpotted)
        {

        }
    }

    private void OnEnemySpotted() 
    {
        
    }
}