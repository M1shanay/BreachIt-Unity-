using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Zoom());
        }
    }

    IEnumerator Zoom()
    {
        while (Camera.main.fieldOfView != 80f)
        {
            Camera.main.fieldOfView += 5f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
