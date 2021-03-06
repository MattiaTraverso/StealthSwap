﻿using UnityEngine;

public class SimpleTeleport : MonoBehaviour, ITeleportCollider
{
    public Vector3 playerTeleportOffset = Vector3.zero;

    public void ApplyTeleport(Player player)
    {
        var playerNewPos = transform.TransformPoint(playerTeleportOffset);

        transform.position = player.transform.position; // lol

        player.TeleportTo(playerNewPos);

        SimpleTween st =  GetComponent<SimpleTween>();
        if (st != null) GameObject.Destroy( st );
    }

    public void PlayerMovedFromHere(Player player)
    { }
}
