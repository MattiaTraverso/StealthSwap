using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTime : MonoBehaviour {
    public float time = 1f;

    float elapsed = 0f;
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;

        if ( elapsed >= time ) {
            transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
            elapsed = 0f;
        }
           


	}
}
