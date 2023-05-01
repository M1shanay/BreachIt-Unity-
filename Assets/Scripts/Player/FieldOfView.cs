using System.Collections;
using System.Text;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _fov;
    [SerializeField] private int _rayCount;
    [SerializeField] private float _angleRadians;
    [SerializeField] private float _viewDistance;
    [SerializeField] private Follow _follow;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private Vector3 _origin;
    private bool _isSpottedEnemy = false;

    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _origin = Vector3.zero;
        _angleRadians *= Mathf.Deg2Rad;
    }

    private void Update()
    {
        float fov = _fov;
        int rayCount = _rayCount;
        float angle = -_angleRadians;
        float angleIncrease = fov / (rayCount - 1);
        float viewDistance = _viewDistance;

        Vector3[] vertices = new Vector3[rayCount + 1];
        int[] triangles = new int[(rayCount - 1) * 3];

        vertices[0] = _origin;

        for (int i = 0; i < rayCount; ++i)
        {
            Vector3 raycastDirection = (transform.forward * Mathf.Cos(angle)) + (transform.right * Mathf.Sin(angle));
            Vector3 vertForward = (Vector3.forward * Mathf.Cos(angle)) + (Vector3.right * Mathf.Sin(angle));

            if (Physics.Raycast(transform.position, raycastDirection, out RaycastHit raycastHit, _viewDistance, _layerMask))
            {
                vertices[i + 1] = vertForward * raycastHit.distance;
            }
            else
            {
                vertices[i + 1] = vertForward * viewDistance;
            }
            angle += angleIncrease;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, ++j)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _meshFilter.mesh = _mesh;
    }
}