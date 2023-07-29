using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1f;

    private float _axisX;
    private float _axisY;
    private bool _isLocked = true;

    private Vector3 _rotate;
    
    void Start()
    {
        LockCursor(true);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LockCursor(false);
            Time.timeScale = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LockCursor(true);
            Time.timeScale = 1;
        }

        if (Level.IsGameOver)
            LockCursor(false);

        if (!_isLocked) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && Level.IsGameStart)
        {
            GetCooldown();
            
            if (Level.IsActiveCooldown)
                FindObjectOfType<AudioManager>().Play("shoot");
        }
        
        
        _axisY = Input.GetAxis("Mouse X");
        _axisX = Input.GetAxis("Mouse Y");
        _rotate = new Vector3(_axisX, _axisY * -mouseSensitivity, 0);

        transform.eulerAngles -= _rotate;
    }

    private void LockCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        _isLocked = isLocked;
    }

    private void GetCooldown()
    {
        if (Level.LastCooldownTime + Level.AttackCooldown < Time.time)
        {
            Level.IsActiveCooldown = true;
            Level.LastCooldownTime = Time.time;
        }
        else
        {
            Level.IsActiveCooldown = false;
        }
    }
}
