using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnHit : MonoBehaviour {
    public static Coroutine badCode = null;

    private void Start() {
        badCode = null;
        StopAllCoroutines();
    }

    void OnTriggerEnter( Collider other ) {
        if (other.gameObject.tag == "Player") {

            if ( badCode == null )
                badCode = StartCoroutine( FadeToRedThenRestart() );
            
        }
    }

    IEnumerator FadeToRedThenRestart() {
        GameObject RedPlane = GameObject.FindGameObjectWithTag( "RedPlane" );
        Material m = RedPlane.GetComponent<MeshRenderer>().material;

        float time = 1f;
        float elapsed = 0f;

        while (m.color.a < 1f) {
            elapsed += Time.deltaTime;
            float currentAlpha = Mathf.Lerp( 0f, 1f, elapsed / time );
            m.color = new Color( m.color.r, m.color.g, m.color.b, currentAlpha );
            yield return new WaitForEndOfFrame();
        }

        Application.LoadLevel( 0 );

        yield return null;
    }
}
