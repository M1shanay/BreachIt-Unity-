using System;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float offset;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _defaultCullingMask;
    [SerializeField] private LayerMask _enemyCullingMask;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z-offset), 1f);
    }

    //0 - default, 1 - with enemy
    public void SetCullingMask(int mask)
    {
        /*if(mask == 0)
        {
            _camera.cullingMask = _defaultCullingMask;
        }
        else if (mask == 1)
        {
            _camera.cullingMask = _enemyCullingMask;
        }*/
    }
}