using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [Header("Objects")]
    public GameObject player;
    
    public static bool IsLocked = true;
    
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

        if (!IsLocked) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && Level.IsGameStart)
            Shoot();


        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }

    private void LockCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        IsLocked = isLocked;
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

    private void Shoot()
    {
        GetCooldown();
            
        if (Level.IsActiveCooldown)
            FindObjectOfType<AudioManager>().Play("shoot");
    }
}
