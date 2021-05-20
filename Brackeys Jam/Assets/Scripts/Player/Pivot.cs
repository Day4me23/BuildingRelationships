using UnityEngine;

public class Pivot : MonoBehaviour
{
    [SerializeField] GameObject player;

    void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f,0f,rotationZ);
    }
}
