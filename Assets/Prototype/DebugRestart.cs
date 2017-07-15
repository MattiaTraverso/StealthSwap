using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRestart : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (!Application.isLoadingLevel) {
            if ( Input.GetKeyDown( KeyCode.R ) )
                Application.LoadLevel( 0 );
        }
	}
}
