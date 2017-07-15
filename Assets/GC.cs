using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC : MonoSingleton<GC> {
    [Header( "Scene Objects" )]
    public GameObject Player;

    [Header("Prefabs")]
    public GameObject BulletPrefab;

}
