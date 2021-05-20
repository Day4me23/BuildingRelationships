using UnityEngine;

public class Object : MonoBehaviour
{
    public Item item;

    private void Awake() => SetupObject(null);

    public void SetupObject(Item setupItem)
    {
        if (setupItem != null)
            item = setupItem;

        if (item != null)
            GetComponent<SpriteRenderer>().sprite = item.artwork;

        this.transform.name = item.name;
    }
}