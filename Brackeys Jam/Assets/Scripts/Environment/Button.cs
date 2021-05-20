using UnityEngine;

public class Button : Trigger
{
    public SpriteRenderer buttonPad;
    public Sprite ButtonUp;
    public Sprite ButtonDown;
    [SerializeField] int count = 0;

    private void Update()
    {
        if (count > 0)
        {
            buttonPad.GetComponent<SpriteRenderer>().sprite = ButtonDown;
            active = true;
        }
        else
        {
            buttonPad.GetComponent<SpriteRenderer>().sprite = ButtonUp;
            active = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Object"))
            count++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Object"))
            count--;
    }
}