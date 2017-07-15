using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerBullet : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Fire,
        EnergyDepleted,
        MaxHitsReached,
        Teleport,
    }

    public float maxEnergy = 1f;
    public int maxHits = 3;
    public float fireSpeed = 10f;

    [NonSerialized] public Player myPlayer;

    private FSMObject<PlayerBullet, State> fsm;
    private Rigidbody rb;
    private SphereCollider sphere;
    private int collLayerMask;

    private Vector3 fireStartPos;
    private Vector3 fireDirection;
    private float currentEnergy;
    private int hitsCount;

    private void IdleEnter(PlayerBullet self, float time) { rb.isKinematic = true; rb.velocity = Vector3.zero; }
    private void IdleExec(PlayerBullet self, float time) { }
    private void IdleExit(PlayerBullet self, float time) { }

    private void FireEnter(PlayerBullet self, float time)
    {
        rb.isKinematic = false;
        rb.MovePosition(fireStartPos);
        rb.velocity = fireDirection * fireSpeed;
        currentEnergy = maxEnergy;
        hitsCount = 0;
        rb.detectCollisions = true;
    }

    private void FireExec(PlayerBullet self, float time)
    {
        currentEnergy -= Time.fixedDeltaTime;
        if (currentEnergy <= 0f)
            fsm.State = State.EnergyDepleted;
    }

    private void FireExit(PlayerBullet self, float time)
    {
        rb.detectCollisions = false;
    }

    private void EnergyDepletedEnter(PlayerBullet self, float time)
    { }
    private void EnergyDepletedExec(PlayerBullet self, float time)
    { fsm.State = State.Idle; }
    private void EnergyDepletedExit(PlayerBullet self, float time)
    { }

    private void MaxHitsReachedEnter(PlayerBullet self, float time)
    { }
    private void MaxHitsReachedExec(PlayerBullet self, float time)
    { fsm.State = State.Idle; }
    private void MaxHitsReachedExit(PlayerBullet self, float time)
    { }

    private void TeleportEnter(PlayerBullet self, float time)
    { }
    private void TeleportExec(PlayerBullet self, float time)
    { fsm.State = State.Idle; }
    private void TeleportExit(PlayerBullet self, float time)
    { }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.rotation = Quaternion.identity;

        fsm.AddState(State.Idle, IdleEnter, IdleExec, IdleExit);
        fsm.AddState(State.Fire, FireEnter, FireExec, FireExit);
        fsm.AddState(State.EnergyDepleted, EnergyDepletedEnter, EnergyDepletedExec, EnergyDepletedExit);
        fsm.AddState(State.MaxHitsReached, MaxHitsReachedEnter, MaxHitsReachedExec, MaxHitsReachedExit);
        fsm.AddState(State.Teleport, TeleportEnter, TeleportExec, TeleportExit);

        fsm.State = State.Idle;

        sphere = GetComponent<SphereCollider>();

        collLayerMask = 0;
        int bulletLayer = gameObject.layer;
        for (int i = 0; i < 32; ++i)
        {
            if (!Physics.GetIgnoreLayerCollision(bulletLayer, i))
                collLayerMask |= (1 << i);
        }
    }

    void FixedUpdate()
    {
        fsm.Update();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (fsm.State != State.Fire)
            return;

        ++hitsCount;

        var teleportCollider = collision.collider.GetComponentInParent<ITeleportCollider>();
        if (teleportCollider != null)
        {
            fsm.State = State.MaxHitsReached;
            myPlayer.BeforeTeleport(teleportCollider);
            teleportCollider.ApplyTeleport(myPlayer);
            return;
        }

        if (hitsCount == maxHits)
        {
            fsm.State = State.MaxHitsReached;
            return;
        }

        var bulletCollider = collision.collider.GetComponentInParent<IBulletCollider>();
        if (bulletCollider != null)
            bulletCollider.ApplyEffect(this);
    }

    public bool CanBeFired()
    {
        return fsm.State == State.Idle;
    }

    public int GetCollLayers()
    {
        return collLayerMask;
    }

    public float GetRadius()
    {
        var scale = transform.lossyScale;
        return sphere.radius * Mathf.Max(scale.x, scale.y);
    }

    public void DepleteEnergy(float amount)
    {
        Debug.Assert(fsm.State == State.Fire);
        currentEnergy -= (maxEnergy * amount);
        if (currentEnergy <= 0f)
            fsm.State = State.EnergyDepleted;
    }

    public void Fire(Vector3 startPosition, Vector3 direction)
    {
        Debug.Assert(fsm.State == State.Idle);
        fireStartPos = startPosition;
        fireDirection = direction;
        fsm.State = State.Fire;
    }
}
