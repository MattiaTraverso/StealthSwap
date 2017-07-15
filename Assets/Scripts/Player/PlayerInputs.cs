using UnityEngine;
using System.Collections.Generic;

class PlayerInputs : MonoBehaviour
{
    public Vector3 fireOffset;
    public Color aimAssistColor;
    public GradientMode aimAssistGradient;

    private Vector3 currentStart;
    private Vector3 currentDirection = Vector3.right;

    private Gradient lineGradient = new Gradient();

    private List<Vector3> aimPoints = new List<Vector3>();
    private Vector3 aimFinalDir;

    private List<Vector3> prevAimPoints = new List<Vector3>();
    private Vector3 prevAimFinalDir;

    void Awake()
    {
        lineGradient.mode = aimAssistGradient;
    }

    private void UpdateAimAssistant(PlayerBullet playerBullet, int maxBounces)
    {
        aimPoints.Clear();
        aimPoints.Add(currentStart);

        RaycastHit hit;
        Vector3 start = currentStart, dir = currentDirection;
        int hitsCount = 0;
        var bulletRad = playerBullet.GetRadius();
        var bulletLayers = playerBullet.GetCollLayers();
        while (hitsCount < maxBounces)
        {
            if (!Physics.SphereCast(start, bulletRad, dir, out hit, float.MaxValue, bulletLayers, QueryTriggerInteraction.Ignore))
                break;

            aimPoints.Add(hit.point + hit.normal * bulletRad);

            start = hit.point + hit.normal * bulletRad;
            dir = dir - 2f * hit.normal * Vector3.Dot(hit.normal, dir);

            ++hitsCount;
        }

        aimFinalDir = dir;
    }

    public bool UpdateInputs(PlayerBullet playerBullet)
    {
        var worldPos = transform.TransformPoint(fireOffset);

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = worldPos.z;

        currentStart = worldPos;
        currentDirection = (mousePos - worldPos).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            prevAimPoints.Clear();
            prevAimPoints.AddRange(aimPoints);
            prevAimFinalDir = aimFinalDir;

            playerBullet.Fire(currentStart, currentDirection);

            return true;
        }
        else
            this.UpdateAimAssistant(playerBullet, 2);

        return false;
    }

    public void UpdateAimGraphics(LineRenderer line, float finalTrailLength)
    {
        int i = 0, c = aimPoints.Count;
        line.positionCount = (c + 1);

        GradientAlphaKey[] alphas = new GradientAlphaKey[3];
        GradientColorKey[] colors = new GradientColorKey[3];

        alphas[0] = new GradientAlphaKey() { alpha = 1f, time = 0f };
        colors[0] = new GradientColorKey() { color = aimAssistColor, time = 0f };

        for (; i < c; ++i)
            line.SetPosition(i, aimPoints[i]);

        alphas[1] = new GradientAlphaKey() { alpha = 1f, time = ((i - 1) / (float)c) };
        colors[1] = new GradientColorKey() { color = aimAssistColor, time = ((i - 1) / (float)c) };

        alphas[2] = new GradientAlphaKey() { alpha = 0f, time = 1f };
        colors[2] = new GradientColorKey() { color = aimAssistColor, time = 1f };

        line.SetPosition(c, aimPoints[c - 1] + aimFinalDir * finalTrailLength);

        lineGradient.alphaKeys = alphas;
        lineGradient.colorKeys = colors;

        line.colorGradient = lineGradient;
    }

    public void UpdateLastShotGraphics(LineRenderer line, float finalTrailLength)
    {
        int i = 0, c = prevAimPoints.Count;
        line.positionCount = (c + 1);

        GradientAlphaKey[] alphas = new GradientAlphaKey[3];
        GradientColorKey[] colors = new GradientColorKey[3];

        alphas[0] = new GradientAlphaKey() { alpha = 1f, time = 0f };
        colors[0] = new GradientColorKey() { color = aimAssistColor, time = 0f };

        for (; i < c; ++i)
            line.SetPosition(i, prevAimPoints[i]);

        alphas[1] = new GradientAlphaKey() { alpha = ((i-1) / (float)c), time = ((i - 1) / (float)c) };
        colors[1] = new GradientColorKey() { color = aimAssistColor, time = ((i - 1) / (float)c) };

        alphas[2] = new GradientAlphaKey() { alpha = 0f, time = 1f };
        colors[2] = new GradientColorKey() { color = aimAssistColor, time = 1f };

        line.SetPosition(c, prevAimPoints[c - 1] + prevAimFinalDir * finalTrailLength);

        lineGradient.alphaKeys = alphas;
        lineGradient.colorKeys = colors;

        line.colorGradient = lineGradient;
    }
}
