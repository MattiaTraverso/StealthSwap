﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletOnClick : MonoBehaviour {

    public float SpawnForce = 100f;

    private GameObject _player;

    void Start() {
        _player = GC.instance.Player;
    }


    // Update is called once per frame
    void Update() {
        if ( !Input.GetMouseButtonDown( 0 ) )
            return;


        Vector3 _playerPosition = _player.transform.position;

        Vector3 _mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        _mousePosition.z = _playerPosition.z;

        Ray ray = new Ray( _playerPosition, _mousePosition - _playerPosition );
        _lastRay = ray;

        GameObject.Instantiate( GC.instance.BulletPrefab, _player.transform.position, Quaternion.identity );
        Rigidbody rigidBody = GC.instance.BulletPrefab.GetComponent<Rigidbody>();
        rigidBody.AddForce( ray.direction * SpawnForce, ForceMode.Impulse );

        Debug.Log( ray.direction );

    }

    //Debug
    private Ray _lastRay;

    private void OnDrawGizmos() {
        Debug.DrawRay( _lastRay.origin, _lastRay.direction * SpawnForce );
    }
}
