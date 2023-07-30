using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    private Camera _mainCamera;
    private Transform _player;
    private NavMeshAgent _enemy;
    
    // private bool _isDead;
    public float enemySpeed = 4;
    
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _enemy.speed = enemySpeed;
        
        _player = GameObject.Find("Player").transform;
        _mainCamera = Camera.main;
        
        // Invoke(nameof(ReducePoints), 2);
        enemySpeed = Random.Range(0.25f, 2f);
    }

    void Update()
    {
        if (Level.IsGameOver)
        {
            Destroy(transform.GameObject());
            return;
        }
        
        EnemyMove();
        
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !Level.IsActiveCooldown) return;
        HitLogic();
    }

    private void EnemyMove()
    {
        if (Level.IsGameStart && !Level.IsGameOver)
            _enemy.SetDestination(_player.position);
    }
    
    private void HitLogic()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.transform == transform)
            {
                _enemy.isStopped = true;
                Destroy(transform.GameObject());
                Level.Points++;
                
                // _isDead = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (Level.Points > 0)
                Level.Points--;
            
            Destroy(transform.GameObject());
        }
    }

    // private void ReducePoints()
    // {
    //     if (!_isDead && !Level.IsGameOver)
    //     {
    //         if (Level.Points > 0)
    //             Level.Points--;
    //         
    //         Destroy(transform.GameObject());
    //     }
    // }
}
