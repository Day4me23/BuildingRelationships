using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MainMenu : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField] GameObject screen;
    [Space]
    [SerializeField] GameObject tab_HostInfo;
    [SerializeField] GameObject tab_JoinInfo;
    [SerializeField] GameObject tab_Settings;
    [SerializeField] GameObject tab_Exit;
    [Space]
    [SerializeField] TMP_InputField textJoinInfo;
    #endregion

    #region Methods Buttons
    public void Host ()
    {
        ClearTabs();
        tab_HostInfo.SetActive(true);

        CreateLobby();
    }
    public void Join()
    {
        ClearTabs();
        tab_JoinInfo.SetActive(true);
    }
    public void Connect()
    {
        JoinLobby(textJoinInfo.text);
        textJoinInfo.text = "";
    }
    public void Settings()
    {
        ClearTabs();
        tab_Settings.SetActive(true);
    }
    public void Exit ()
    {
        ClearTabs();
        tab_Exit.SetActive(true);
    }
    public void Quit()
    {
        Debug.Log("closing game!");
        Application.Quit();
    }
    public void ClearTabs()
    {
        tab_HostInfo.SetActive(false);
        tab_JoinInfo.SetActive(false);
        tab_Settings.SetActive(false);
        tab_Exit.SetActive(false);

        if (PhotonNetwork.CurrentRoom != null)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    #endregion

    #region Methods Multiplayer
    int roomNumber;

    void Start() => PhotonNetwork.ConnectUsingSettings();
   
    // Hosting Logic
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
        screen.SetActive(false);
    }
    
    void CreateLobby() // this is run when host button is pressed
    {
        roomNumber = Random.Range(0,1000);
        PhotonNetwork.CreateRoom(roomNumber.ToString(), new RoomOptions { MaxPlayers = 2}, TypedLobby.Default);
    }
    private void OnFailedToConnect()
    {
        Debug.Log("Failed to create room.");
        CreateLobby();
    }
    public override void OnCreatedRoom()
    { 
        GameManager.instance.playerNumber = 1;
        GameManager.instance.roomNumber = roomNumber;
        Debug.Log("creating room... player " + GameManager.instance.playerNumber + " joined room: " + roomNumber);
        GameObject.Find("TextHostInfo").GetComponent<UnityEngine.UI.Text>().text = ("Room: " + roomNumber);

        Debug.Log("Player list " + PhotonNetwork.PlayerList[0]);
    }

    // Joining Logic
    void JoinLobby(string num)
    {
        if (!PhotonNetwork.JoinRoom(num))
            Debug.Log("No room found.");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("joined lobby");

        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.CountOfPlayersInRooms == 2)
            PhotonNetwork.LoadLevel(1);
    }
   
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("room list length = " + PhotonNetwork.PlayerList.Length);

            PhotonNetwork.LoadLevel(1);
        }
    }
}