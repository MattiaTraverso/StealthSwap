using UnityEngine;

public interface ITeleportCollider
{
    void ApplyTeleport(Player player);
    void PlayerMovedFromHere(Player player);
}
