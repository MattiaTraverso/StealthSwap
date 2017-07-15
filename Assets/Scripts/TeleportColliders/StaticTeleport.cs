using UnityEngine;

public class StaticTeleport : MonoBehaviour, ITeleportCollider, ILevelCollider
{
    public Vector3 playerTeleportOffset = Vector3.zero;
    public bool disableCollisions = true;

    public void ApplyTeleport(Player player)
    {
        if (disableCollisions)
        {
            var colls = GetComponentsInChildren<Collider>();
            foreach (var coll in colls)
                coll.enabled = false;
        }

        player.TeleportTo(transform.TransformPoint(playerTeleportOffset));
    }

    public void PlayerMovedFromHere(Player player)
    {
        if (disableCollisions)
        {
            var colls = GetComponentsInChildren<Collider>();
            foreach (var coll in colls)
                coll.enabled = true;
        }
    }

    public void ResetLevel()
    {
        if (disableCollisions)
        {
            var colls = GetComponentsInChildren<Collider>();
            foreach (var coll in colls)
                coll.enabled = true;
        }
    }
}
