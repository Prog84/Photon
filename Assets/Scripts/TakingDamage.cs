using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image _healthBar;

    private float _health;
    private float _startHealth = 100f;

    private void Start()
    {
        _health = _startHealth;
        _healthBar.fillAmount = _health / _startHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        _health -= damage;
       
        _healthBar.fillAmount = _health / _startHealth;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (photonView.IsMine)
        {
            Game.instance.LeaveRoom();
        }    
    }
}
