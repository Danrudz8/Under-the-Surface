using UnityEngine;

public class CharacterBob : MonoBehaviour
{
    public float bobAmount = 0.04f;
    public float bobSpeed = 10f;

    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        if (isMoving)
        {
            float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;

            transform.localPosition = new Vector3(
                startLocalPosition.x,
                startLocalPosition.y + bob,
                startLocalPosition.z
            );
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startLocalPosition,
                10f * Time.deltaTime
            );
        }
    }
}

