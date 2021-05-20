using UnityEngine;

[CreateAssetMenu(menuName = "New level")]
public class Level : ScriptableObject
{
    [Header ("Main information")]
    public int level;
    public bool locked;
    [Header ("Level information")]
    public Vector2 player1Spawn;
    public Vector2 player2Spawn;
    [Header ("Details")]
    public float time;
}