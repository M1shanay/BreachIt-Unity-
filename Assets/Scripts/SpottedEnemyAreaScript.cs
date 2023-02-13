using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedEnemyAreaScript : MonoBehaviour
{
    private bool _isEnemySpotted = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            _isEnemySpotted = true;
        }
    }

    public bool GetIsEnemySpotted
    {
        get { return _isEnemySpotted; }
    }
}