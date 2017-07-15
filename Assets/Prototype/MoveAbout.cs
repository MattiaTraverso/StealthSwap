using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbout : MonoBehaviour {
    public Transform target1;
    public Transform target2;

    private Vector3 pos1;
    private Vector3 pos2;

    public float delay = 2f;
    public float moveTime = 2f;

	// Use this for initialization
	void Start () {
        StopAllCoroutines();

        pos1 = target1.position;
        pos2 = target2.position;

        StartCoroutine( Behaviour() );
	}


    IEnumerator Behaviour() {
        yield return StartCoroutine( MoveToPosition( pos1, pos2 ) );
        yield return new WaitForSeconds( delay );
        yield return StartCoroutine( MoveToPosition( pos2, pos1 ) );
        yield return new WaitForSeconds( delay );
        yield return StartCoroutine( Behaviour() );

    }

    IEnumerator MoveToPosition(Vector3 pos1, Vector3 pos2) {
        float elapsed = 0f;

        transform.localScale = new Vector3( Mathf.Sign( pos2.x - pos2.y ), transform.localScale.y, transform.localScale.z );

        while (elapsed <= moveTime) {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp( pos1, pos2, elapsed / moveTime );

            yield return new WaitForEndOfFrame();
        }
    }
	

}
