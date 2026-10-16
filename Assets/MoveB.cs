using UnityEngine;

public class MoveB : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(10f, 0f, 5f);
    public float speed = 5f;

    void Start()
    {
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }
}
