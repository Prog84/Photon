using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;

    private string _nameSceneLevel = "GameScene";

    #region Unity Methods
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;        
    }

    private void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    #endregion

    #region Public Methods
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }      
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected on server");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(_nameSceneLevel);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion
}
