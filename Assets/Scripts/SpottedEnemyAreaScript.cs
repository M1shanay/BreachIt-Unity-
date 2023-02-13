using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedEnemyAreaScript : MonoBehaviour
{
    private bool _isEnemySpotted = false;

<<<<<<< Updated upstream
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
=======
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
>>>>>>> Stashed changes
        {
            _isEnemySpotted = true;
        }
    }

    public bool GetIsEnemySpotted
    {
        get { return _isEnemySpotted; }
    }
}