using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lookSensitivity = 5f;
    [SerializeField] private GameObject _lookCamera;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private float _cameraUpDownRotation = 0f;
    private float _currentCameraUpDownRotation = 0f;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 movementHorizontal = transform.right * xMovement;
        Vector3 movementVertical = transform.forward * zMovement;

        Vector3 movementVelocity = (movementHorizontal + movementVertical).normalized * _speed;

        Move(movementVelocity);

        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation, 0) * _lookSensitivity;

        Rotate(_rotationVector);

        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * _lookSensitivity;

        RotateCamera(_cameraUpDownRotation);
    }

    private void FixedUpdate()
    {
        if (_velocity!= Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
        }

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));

        if (_lookCamera != null)
        {
            _currentCameraUpDownRotation -= _cameraUpDownRotation;
            _currentCameraUpDownRotation = Mathf.Clamp(_currentCameraUpDownRotation, -85, 85);
            _lookCamera.transform.localEulerAngles = new Vector3(_currentCameraUpDownRotation, 0, 0);
        }
    }

    private void Move(Vector3 movementVelocity)
    {
        _velocity = movementVelocity;
    }

    private void Rotate(Vector3 rotationVector)
    {
        _rotation = rotationVector;
    }

    private void RotateCamera(float cameraUpDownRotation)
    {
        _cameraUpDownRotation = cameraUpDownRotation;
    }
}
