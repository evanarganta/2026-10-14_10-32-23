using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Kecepatan putar dalam derajat per detik
    public Vector3 rotationSpeed = new Vector3(0, 0, 50);

    void Update()
    {
        // Memutar objek setiap frame secara konsisten
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
