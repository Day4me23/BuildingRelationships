using UnityEngine;

public class Deconstructor : MonoBehaviour
{
    public GameObject ObjectPrefab;
    public Vector2 itemSpawns = new Vector2 (-1f,1f);
    public bool Deconstruct(Player player)
    {
        Debug.Log("Deconstructing");

        if (player.item == null)
            return false; // no item in players slot

        if (player.item.part1 == null || player.item.part2 == null)
            return false; // item cannot be deconstructed because it pas no parts

        Item item = player.item;
        GameObject temp;

        // creates item to the left of machine
        temp = Instantiate(ObjectPrefab, this.transform.position + new Vector3(itemSpawns.x, 0, 0), Quaternion.identity); 
        temp.GetComponent<Object>().SetupObject(item.part1);

        // creates item to the right of the machine
        temp = Instantiate(ObjectPrefab, this.transform.position + new Vector3(itemSpawns.y,0,0), Quaternion.identity); 
        temp.GetComponent<Object>().SetupObject(item.part2);

        player.item = null;

        return true;
    }
}