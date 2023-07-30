using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Characteristics")]
    public float mouseSensitivity = 1f;
    public float moveSpeed = 1f;

    private float _rotateAxisX;
    private float _rotateAxisY;
    private float _moveAxisX;
    private float _moveAxisY;
    
    private Vector3 _rotate;
    private Vector3 _moveDirection;

    private Vector3 _velocity = Vector3.down * 10;
    private const float Gravity = -9.81f;
    private float _groundDistance = 1f;
    private bool _isGrounded;
    
    [Header("Objects")]
    public CharacterController controller;
    public Transform groundChecker;
    public LayerMask groundMask;

    void Update()
    {
        if (!CameraMovement.IsLocked) return;
        
        RotateLogic();
        MoveLogic();
    }
    
    private void MoveLogic()
    {
        _isGrounded = Physics.CheckSphere(groundChecker.position, _groundDistance, groundMask);
        // if (_isGrounded && _velocity.y < 0)
        //     _velocity.y = -2f;
        
        _moveAxisX = Input.GetAxisRaw("Horizontal");
        _moveAxisY = Input.GetAxisRaw("Vertical");

        _moveDirection = transform.forward * _moveAxisY + transform.right * _moveAxisX;
        controller.Move(_moveDirection * (moveSpeed * Time.deltaTime));

        // _velocity.y += Gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
    
    private void RotateLogic()
    {
        _rotateAxisY = Input.GetAxis("Mouse X");
        _rotateAxisX = Input.GetAxis("Mouse Y");
        _rotate = new Vector3(_rotateAxisX, _rotateAxisY * -mouseSensitivity, 0);

        transform.eulerAngles -= _rotate;
    }
}
