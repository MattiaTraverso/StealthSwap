using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAfterSeconds : MonoBehaviour {
    public float delay = 2f;
    public float tweenTime = 0.6f;

    void Start() {
        StopAllCoroutines();

        StartCoroutine( FlipAfterTime( delay ) );
    }

    IEnumerator FlipAfterTime(float timeAfterFlip) {
        yield return new WaitForSeconds( timeAfterFlip );

        yield return StartCoroutine( FlipTween( transform.localScale.x, tweenTime ) );

        yield return StartCoroutine(FlipAfterTime(delay));

        yield return null;
    }

    IEnumerator FlipTween(float originalScale, float timeForFlip) {
        float timePassed = 0f;

        while (timePassed < timeForFlip) {
            timePassed += Time.deltaTime;

            transform.localScale = new Vector3(Mathf.Lerp( originalScale, -originalScale, timePassed / timeForFlip ), transform.localScale.y, transform.localScale.z);
            Debug.Log( timeForFlip / timePassed );

            yield return new WaitForEndOfFrame();
        }
    }
}
