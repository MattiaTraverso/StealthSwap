using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTween : MonoBehaviour {
    public Transform target1;
    public Transform target2;

    public float duration = 2f;

    private Vector3 start;
    private Vector3 end;

	// Use this for initialization
	void Start () {
        start = target1.transform.position;
        end = target2.transform.position;
	}

    void Flip() {
        Vector3 temp = start;
        start = end;
        end = temp;

        elapsed = 0f;
    }

    float elapsed = 0f;
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;

        transform.position = Vector3.Lerp( start, end, elapsed / duration );

        if (elapsed >= duration) {
            Flip();
        }
	}
}
