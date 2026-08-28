using UnityEngine;

public class CellPulse : MonoBehaviour
{
    public float amount = 0.08f;
    public float speed = 2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float pulse = 1f + Mathf.Sin(Time.time * speed) * amount;
        transform.localScale = originalScale * pulse;
    }
}
