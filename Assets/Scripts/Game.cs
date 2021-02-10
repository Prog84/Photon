using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerTemplate;

    public static Game instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (_playerTemplate != null)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(_playerTemplate.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
