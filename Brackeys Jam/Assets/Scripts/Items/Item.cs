using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "New Item")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite artwork;

    public Item part1;
    public Item part2;

    public List<Craftable> crafting;
}

[System.Serializable]
public struct Craftable
{
    [Header("Item being crafted")]
    public Item craftedItem;
    [Header("Partner item to craft with")]
    public Item partnerItem;
}