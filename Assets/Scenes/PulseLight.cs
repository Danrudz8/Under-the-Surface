using UnityEngine;

public class PulseLight : MonoBehaviour
{
    [SerializeField] private Light pulseLight;

    [SerializeField] private float minIntensity = 0f;
    [SerializeField] private float maxIntensity = 10f;
    [SerializeField] private float pulseSpeed = 2f;

    void Update()
    {
        float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);

        pulseLight.intensity = Mathf.Lerp(
            minIntensity,
            maxIntensity,
            pulse
        );
    }
}