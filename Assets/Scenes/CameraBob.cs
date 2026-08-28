using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [SerializeField] float bobAmount = 0.035f;
    [SerializeField] float bobSpeed = 10f;
    [SerializeField] float returnSpeed = 10f;

    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isMoving =
            Mathf.Abs(horizontal) > 0.1f ||
            Mathf.Abs(vertical) > 0.1f;

        if (isMoving)
        {
            float yBob =
                Mathf.Sin(Time.time * bobSpeed) * bobAmount;

            transform.localPosition = new Vector3(
                startLocalPosition.x,
                startLocalPosition.y + yBob,
                startLocalPosition.z
            );
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startLocalPosition,
                returnSpeed * Time.deltaTime
            );
        }
    }
}
