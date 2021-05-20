using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Vector2 bound1;
    [SerializeField] Vector2 bound2;
    [SerializeField][Range (0.1f, 2)] float speed;
    [SerializeField] MoveType moveType;
    [SerializeField] Trigger trigger;

    [SerializeField] SpriteRenderer leftLight;
    [SerializeField] SpriteRenderer rightLight;

    bool moving;
    float percentage;
    bool forwards;
    bool touching = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touching = true;
            collision.collider.transform.SetParent(transform);
        }

       else if (collision.gameObject.CompareTag("Object"))
        {
            touching = true;
            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touching = false;
            collision.collider.transform.SetParent(null);
        }

        else if (collision.gameObject.CompareTag("Object"))
        {
            touching = false;
            collision.collider.transform.SetParent(null);
        }

    }
    private void FixedUpdate()
    {
        if (moveType == MoveType.auto)
        {
            moving = true;
        }
        else if (moveType == MoveType.touch)
        {
            if (touching)
                moving = true;
            else
                moving = false;
        }
        else if (moveType == MoveType.trigger)
        {
            if (trigger != null)
            {
                if (trigger.active)
                    moving = true;
                else
                    moving = false;
            }
            else
            {
                Debug.LogWarning("add trigger to platform!");
                moving = false;
            }
        }

        if (moving)
        {
            Move();
            leftLight.color = Color.green;
            rightLight.color = Color.green;
        }
        else
        {
            leftLight.color = Color.red;
            rightLight.color = Color.red;
        }
    }
    void Move()
    {
        if (forwards)
            percentage += Time.deltaTime * speed;
        else
            percentage -= Time.deltaTime * speed;

        if (percentage >= 1 && forwards)
            forwards = !forwards;
        if (percentage <= 0 && !forwards)
            forwards = !forwards;


        transform.position = Vector2.Lerp(bound1, bound2, percentage);
    }
}
public enum MoveType { auto, trigger, touch}