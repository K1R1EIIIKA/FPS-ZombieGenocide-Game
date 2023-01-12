using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensivity = 1f;

    private float _axisX;
    private float _axisY;
    private bool _isLocked = true;

    [SerializeField] private AudioSource attackSound;
    
    private Vector3 _rotate;
    private Level _level;
    
    void Start()
    {
        LockCursor(true);
        
        _level = GameObject.Find("EventSystem").GetComponent<Level>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LockCursor(false);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            LockCursor(true);

        if (_level.IsGameOver)
            LockCursor(false);

        if (!_isLocked) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _level.IsGameStart)
        {
            GetCooldown();
            
            if (_level.IsActiveCooldown)
                attackSound.Play();
        }
        
        
        _axisY = Input.GetAxis("Mouse X");
        _axisX = Input.GetAxis("Mouse Y");
        _rotate = new Vector3(_axisX, _axisY * -mouseSensivity, 0);

        transform.eulerAngles -= _rotate;
    }

    private void LockCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        _isLocked = isLocked;
    }

    private void GetCooldown()
    {
        if (_level.LastCooldownTime + _level.attackCooldown < Time.time)
        {
            _level.IsActiveCooldown = true;
            _level.LastCooldownTime = Time.time;
        }
        else
        {
            _level.IsActiveCooldown = false;
        }
    }
}
