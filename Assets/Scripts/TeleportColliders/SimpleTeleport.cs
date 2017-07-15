using UnityEngine;

public class SimpleTeleport : MonoBehaviour, ITeleportCollider
{
    public Vector3 playerTeleportOffset = Vector3.zero;

    public void ApplyTeleport(Player player)
    {
        var playerNewPos = transform.TransformPoint(playerTeleportOffset);

        transform.position = player.transform.position; // lol

        player.TeleportTo(playerNewPos);
    }

    public void PlayerMovedFromHere(Player player)
    { }
}
