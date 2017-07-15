using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerBullet bullet;
    public LineRenderer aimAssistant;
    public LineRenderer lastShot;

    private PlayerInputs inputs;
    private Material aimAssistMat;
    private float aimAssistOpacity = 0f;

    private int shotsCount = 0;

    void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        aimAssistMat = aimAssistant.material;
    }

    void Update()
    {
        if (bullet.CanBeFired())
        {
            aimAssistOpacity = Mathf.MoveTowards(aimAssistOpacity, 1f, Time.deltaTime * 4f);
            aimAssistMat.SetColor("_TintColor", new Color(1f, 1f, 1f, aimAssistOpacity));

            if (shotsCount > 0)
                inputs.UpdateLastShotGraphics(lastShot, 4f);

            if (inputs.UpdateInputs(bullet))
            {
                ++shotsCount;
                // fired
            }
            else
                inputs.UpdateAimGraphics(aimAssistant, 4f);
        }
        else
        {
            inputs.UpdateLastShotGraphics(lastShot, 4f);

            aimAssistOpacity = Mathf.MoveTowards(aimAssistOpacity, 0f, Time.deltaTime * 4f);
            aimAssistMat.SetColor("_TintColor", new Color(1f, 1f, 1f, aimAssistOpacity));
        }
    }
}
