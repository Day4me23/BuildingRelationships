using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }
    #endregion
    public int playerNumber;
    public int roomNumber;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {

        Debug.Log("level loaded");

        GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
            Destroy(players[i]);

        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    }
}