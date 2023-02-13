using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z-offset), 1f);
    }
}
