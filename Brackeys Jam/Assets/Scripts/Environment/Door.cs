using UnityEngine;

public class Door : MonoBehaviour
{
    public Trigger trigger;
    [HideInInspector] public bool open;
    [HideInInspector] public bool locked;
    [Space]
    public bool LockIfLocked;
    public Level levelPath;
    [Space]
    [SerializeField] new SpriteRenderer light;
    public Sprite DoorOpen;
    public Sprite DoorClosed;

    private void Start()
    {
        locked = false;
    }
    public void Update()
    {
        if (trigger != null)
            locked = !trigger.active;
        else
            locked = false;

        if (LockIfLocked && levelPath.locked)
            locked = true;

        ChangeAppearance();
    }
    void ChangeAppearance()
    {
        if (locked)
            light.color = Color.red;
        else
            light.color = Color.green;

        if (open && !locked)
            GetComponent<SpriteRenderer>().sprite = DoorOpen;
        else
            GetComponent<SpriteRenderer>().sprite = DoorClosed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            open = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            open = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            open = false;
    }
}
