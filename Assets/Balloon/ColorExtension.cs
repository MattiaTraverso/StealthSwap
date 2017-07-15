using UnityEngine;

public static class ColorExtension {
    public static Color SameColorDifferentAlpha( this Color color, float newAlpha ) {
        return new Color( color.r, color.g, color.b, newAlpha );
    }
}