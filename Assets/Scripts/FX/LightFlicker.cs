using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float time = 0.65f;
    public Light _light;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Flicker();
    }
    void Flicker()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            _light.enabled = !_light.enabled;
            timer = time;
        }

    }
}
