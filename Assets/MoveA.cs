using UnityEngine;

public class MoveA : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 targetPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
    }

    void Update()
    {
        transform.position = targetPosition;
    }
}
