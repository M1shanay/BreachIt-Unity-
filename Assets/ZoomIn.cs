using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
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
        while (Camera.main.fieldOfView != 55f)
        {
            Camera.main.fieldOfView -= 5f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
