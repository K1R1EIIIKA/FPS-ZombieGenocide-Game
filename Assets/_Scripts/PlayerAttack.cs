using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    private Camera _mainCamera;
    private Level Level;
    
    private bool _isDead;
    private float _enemySpeed;
    
    void Start()
    {
        Invoke(nameof(ReducePoints), 2);
        _enemySpeed = Random.Range(0.25f, 2f);
        
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Level.IsGameOver)
            Destroy(transform.GameObject());
        
        EnemyMove();
        
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !Level.IsActiveCooldown) return;
        HitLogic();
    }

    private void EnemyMove()
    {
        Vector3 cameraPosition = _mainCamera.transform.position;
        Vector3 position = transform.position;
        Vector3 target = new Vector3(cameraPosition.x, position.y, cameraPosition.z);

        transform.Translate((target - position) * Time.deltaTime * _enemySpeed);
        Debug.Log(_enemySpeed);
        Debug.Log(target + " " + position);
    }
    
    private void HitLogic()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.transform == transform)
            {
                Destroy(transform.GameObject());
                Level.Points++;
                
                _isDead = true;
            }
        }
    }

    private void ReducePoints()
    {
        if (!_isDead && !Level.IsGameOver)
        {
            if (Level.Points > 0)
                Level.Points--;
            
            Destroy(transform.GameObject());
        }
    }
}
