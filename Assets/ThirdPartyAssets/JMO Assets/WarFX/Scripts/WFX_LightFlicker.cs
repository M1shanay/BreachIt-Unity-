using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

public class WFX_LightFlicker : MonoBehaviour
{
	public float time = 0.05f;
	public Light _light;
	private float timer;
	
	void Start ()
	{
		timer = time;
	}
    private void Update()
    {
		Flicker();
    }
    void Flicker()
	{
		if (timer>=0)
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
