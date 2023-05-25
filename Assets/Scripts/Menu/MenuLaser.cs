using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLaser : MonoBehaviour
{
    public Texture2D cursorTexture;
    private LineRenderer line;
    public Transform laserposition;
    public Transform laserPoint;

    private ImpactManager impactSpawner;
    // Start is called before the first frame update
    void Start()
    {
        impactSpawner = GetComponent<ImpactManager>();
        line = GetComponent<LineRenderer>();
        Cursor.visible = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        Laser();
    }
    void Laser()
    {
        line.SetPosition(0, laserposition.position);
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Луч пускается из камеры с тегом MainCamera
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            if (hit.collider)
            {
                line.SetPosition(1, hit.point);
                laserPoint.transform.position = hit.point;
                if (Input.GetMouseButtonDown(0))
                {
                    impactSpawner.ImpactSpawn(hit, 1);
                }
            }
        }

    }
}
