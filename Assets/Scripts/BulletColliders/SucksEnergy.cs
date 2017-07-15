using UnityEngine;

public class SucksEnergy : MonoBehaviour, IBulletCollider
{
    [Range(0f, 1f)]
    public float amount = .5f;

    public void ApplyEffect(PlayerBullet bullet)
    {
        bullet.DepleteEnergy(amount);
    }
}
