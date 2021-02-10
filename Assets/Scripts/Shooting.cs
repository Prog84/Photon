using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera _lookCamera;

    public float FireRate = 0.1f;
    private float _fireTimer;

    private void Update()
    {
        if (_fireTimer <= FireRate)
        {
            _fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && _fireTimer>FireRate)
        {
            _fireTimer = 0f;
            RaycastHit hit;
            Ray ray = _lookCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                }
            }
        }
    }
}
