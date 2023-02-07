using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private List<Sprite> Sprites;
    private SpriteRenderer spriteRenderer;
    private List<TupleAngle> Angles = new List<TupleAngle> { new TupleAngle(-22.5f, 22.5f), new TupleAngle(22.5f, 67.5f), new TupleAngle(67.5f, 112.5f),
        new TupleAngle(112.5f, 167.5f), new TupleAngle(-67.5f, -22.5f), new TupleAngle(-112.5f, -67.5f), new TupleAngle(-167.5f, -112.5f),
        new TupleAngle(float.MinValue, float.MaxValue) };

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePosition - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spriteRenderer.sprite = Sprites[Angles.FindIndex(x => angle >= x.MinAngle && angle <= x.MaxAngle)];
    }
}

public struct TupleAngle
{
    public TupleAngle(float minAngle, float maxAngle)
    {
        MinAngle = minAngle;
        MaxAngle = maxAngle;
    }

    public float MinAngle { get; set; }
    public float MaxAngle { get; set; }
}
