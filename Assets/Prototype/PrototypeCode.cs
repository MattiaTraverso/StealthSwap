using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeCode : MonoBehaviour {
    public GameObject Player;
    public LineRenderer Line;

    void Start() {
        var pp = FindObjectOfType<PlayerPosition>();
        Player.transform.position = pp.transform.position;
    }


    // Update is called once per frame
    void Update () {
        Vector3 playerPosition = Player.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mousePosition.z = playerPosition.z;

        Ray ray = new Ray( playerPosition, mousePosition - playerPosition );
        Line.SetPosition( 0, playerPosition );
        Line.SetPosition( 1, mousePosition );

        if ( !Input.GetMouseButtonDown( 0 ) )
            return;

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) {
            if (hitInfo.collider.gameObject.tag == "Enemy") {
                GameObject Enemy = hitInfo.collider.gameObject;

                Vector3 enemyPosition = Enemy.transform.Find("Enemy").position;
                Enemy.transform.position = Player.transform.position;
                Player.transform.position = enemyPosition;

            }
        }

	}
}
