using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    private Camera _mainCamera;
    private Level _level;
    
    private bool _isDead;
    
    void Start()
    {
        Invoke(nameof(RedusePoints), 2);
        
        _mainCamera = Camera.main;
        _level = GameObject.Find("EventSystem").GetComponent<Level>();
    }

    void Update()
    {
        if (_level.IsGameOver)
            Destroy(transform.GameObject());
        
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !_level.IsActiveCooldown) return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.transform == transform)
            {
                Destroy(transform.GameObject());
                _level.points++;
                
                _isDead = true;
            }
        }
    }

    private void RedusePoints()
    {
        if (!_isDead && !_level.IsGameOver)
        {
            if (_level.points > 0)
                _level.points--;
            
            Destroy(transform.GameObject());
        }
    }
}
