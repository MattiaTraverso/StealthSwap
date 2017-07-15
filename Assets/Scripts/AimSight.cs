using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSight : MonoBehaviour {
    private LineRenderer _line;
    private GameObject _player;

	void Start () {
        _line = GetComponent<LineRenderer>();
        _player = GC.instance.Player;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 _playerPosition = _player.transform.position;

        Vector3 _mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        _mousePosition.z = _playerPosition.z;

        _line.SetPosition( 0, _playerPosition );
        _line.SetPosition( 1, _mousePosition );
    }
}
