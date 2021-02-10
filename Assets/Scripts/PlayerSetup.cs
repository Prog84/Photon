using UnityEngine;
using Photon.Pun;
using TMPro;

[RequireComponent(typeof(PlayerMover))]
public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _lookCamera;
    [SerializeField] private TextMeshProUGUI _playerNameText;

    private void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<PlayerMover>().enabled = true;
            _lookCamera.GetComponent<Camera>().enabled = true;
        }
        else
        {
            transform.GetComponent<PlayerMover>().enabled = false;
            _lookCamera.GetComponent<Camera>().enabled = false;
        }
        SetPlayerUI();
    }

    private void SetPlayerUI()
    {
        if (_playerNameText != null)
        {
            _playerNameText.text = photonView.Owner.NickName;
        }
    }
}
