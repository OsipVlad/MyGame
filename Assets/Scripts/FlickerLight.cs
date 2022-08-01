using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    private Light _light;
    private float start_intensity;
    public float min_intensity = 0.3f;
    public float max_intensity = 1.5f;
    public float noise_speed = 0.15f;

    public bool flicker_ON;
    public bool random_timer;

    public float random_timer_value_min = 5f;
    public float random_timer_value_max = 20f;

    private float random_timer_value;

    public float start_timer_value = 0.1f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _light = GetComponent<Light>();
        start_intensity = _light.intensity;
        yield return new WaitForSeconds(start_timer_value);

        while (random_timer)
        {
            random_timer_value = Random.Range(random_timer_value_min, random_timer_value_max);
            yield return new WaitForSeconds(random_timer_value);
            if (flicker_ON)
            {
                _light.intensity = start_intensity;
                flicker_ON = false;
            }
            else
            {
                flicker_ON = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flicker_ON) _light.intensity = Mathf.Lerp(min_intensity, max_intensity, Mathf.PerlinNoise(10, Time.deltaTime/noise_speed));
    }
}
