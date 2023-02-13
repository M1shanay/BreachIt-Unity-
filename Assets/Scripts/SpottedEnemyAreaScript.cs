using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedEnemyAreaScript : MonoBehaviour
{
    private bool _isEnemySpotted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isEnemySpotted = true;
        }
    }

    public bool GetIsEnemySpotted
    {
        get { return _isEnemySpotted; }
    }
}