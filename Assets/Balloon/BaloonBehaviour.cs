using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaloonBehaviour : MonoBehaviour {
    public Image[] BaloonImages;
    public Image[] InteractiveTip;
    public Text BaloonText;

    //============RESET=============
    void Awake() {
        ResetBalloon();

        StartBaloonBehaviour( BaloonBehaviourType.BANTER );
    }

    private void ResetBalloon() {
        foreach ( Image i in BaloonImages )
            i.color -= new Color( 0f, 0f, 0f, i.color.a );

        foreach ( Image i in InteractiveTip )
            i.color -= new Color( 0f, 0f, 0f, i.color.a );

        BaloonText.color -= new Color( 0f, 0f, 0f, BaloonText.color.a );
    }


    //============START=============

    public enum BaloonBehaviourType {
        BANTER,
        PLAYER
    }

    private BaloonBehaviourType _type;

    public void StartBaloonBehaviour( BaloonBehaviourType type ) {
        _type = type;

        IEnumerator[] sequence = new IEnumerator[2];
        sequence[0] = FadeBaloon( 1f, true );
        sequence[1] = DisplayTextType( 3f, "BEEP BOOP", KeyCode.Space);

        StartCoroutine( BalloonBehaviour( sequence) );
    }

    IEnumerator BalloonBehaviour(IEnumerator[] Sequence) {
        foreach (IEnumerator enumerator in Sequence) {
            yield return StartCoroutine( enumerator );
        }
    }


    //==============FUNCTIONS=====================

    IEnumerator FadeBaloon( float duration = 1f, bool fadeIn = true ) {
        float startAlpha = ( fadeIn ) ? 0f : 1f;
        float endAlpha = ( fadeIn ) ? 1f : 0f;

        float elapsed = 0f;


        while ( elapsed <= duration ) {
            elapsed += Time.deltaTime;

            float currentAlpha = Mathf.Lerp( startAlpha, endAlpha, elapsed / duration );

            foreach ( Image i in BaloonImages )
                i.color = i.color.SameColorDifferentAlpha( currentAlpha );

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator DisplayTextType( float duration = 2f, string newString = "BEEP BOP", KeyCode skipKey = KeyCode.None ) {
        BaloonText.text = "";
        BaloonText.color = BaloonText.color.SameColorDifferentAlpha( 1f );
        float elapsed = 0f;

        int numCharacters = 0;
        int maxCharacters = newString.Length;

        while ( elapsed <= duration ) {
            elapsed += Time.deltaTime;

            if ( skipKey != KeyCode.None )
                elapsed = ( Input.GetKey( skipKey ) ) ? duration : elapsed;

            int currentNumCharacters = (int)Mathf.Lerp( numCharacters, maxCharacters, elapsed / duration );
            BaloonText.text = newString.Substring( 0, currentNumCharacters );

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }






}
