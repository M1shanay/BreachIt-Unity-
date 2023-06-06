using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public float inFov;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Zoom());
        }
    }

    IEnumerator Zoom()
    {
        while (Camera.main.fieldOfView != inFov)
        {
            Camera.main.fieldOfView -= 5f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
