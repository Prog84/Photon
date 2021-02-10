using UnityEngine;
using Photon.Pun;

public class PlayerNameInputManager : MonoBehaviour
{
    public void SetPlayerName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("player name is empty");
            return;
        }

        PhotonNetwork.NickName = playerName;
    }
}
