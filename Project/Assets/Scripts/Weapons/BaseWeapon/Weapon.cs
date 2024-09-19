using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] Vector2 bulletOffset;
    [SerializeField] public BaseBullet bulletObject;
    [SerializeField] public Sprite muzzleFlash;
    [SerializeField] public float fireRate;
    [SerializeField] public float bulletCount;
    [SerializeField] public float fireSpread;
    [SerializeField] public Vector2 weaponHoldOffset;
    [SerializeField] public float weaponGravity;
    [SerializeField] public float recoil;
    [SerializeField] public float recoilWearOffRate;
    [SerializeField] public float weaponDrag;
    [SerializeField] public Vector2 weaponDragLimits;
    [SerializeField] public Sprite weaponSprite;
    [SerializeField] public Sprite heldWeaponSprite;
    public static Transform bulletParent { get; private set; }

    private Vector2 targetPosition = Vector2.zero;
    public CountdownTimer attackCooldown;
    public float currentRecoil = 0;

    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D coll { get; private set; }

    public PlayerController holder = null;
    public Queue<BaseBullet> bulletInstanceQueue = new Queue<BaseBullet>();

    public BaseWeaponState droppedState = new DroppedState();
    public BaseWeaponState holdingState = new HoldingState();

    private BaseWeaponState currentState;

    public abstract void Attack();

    private void Start()
    {
        attackCooldown = new CountdownTimer(1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ChangeState(droppedState);
        InstantiateBullets();
    }

    protected void Update()
    {
        currentState.UpdateState(this);
        WearOffRecoil();
    }
    protected void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public bool IsHeld()
    {
        return holder;
    }

    
    public void ChangeState(BaseWeaponState newState)
    {
        currentState = newState;
        currentState.StartState(this);
    }

    private void InstantiateBullets()
    {
        int instanceCount = GetBulletInstanceCount();

        InitializeBulletParent();
        for (int i = 0; i < instanceCount; i++)
        {
            BaseBullet currentInstance = GameObject.Instantiate(bulletObject.gameObject, bulletParent).GetComponent<BaseBullet>();
            bulletInstanceQueue.Enqueue(currentInstance);
        }
    }

    protected abstract int GetBulletInstanceCount();

    protected int GetBulletInstanceCountRapidFire()
    {
        float bulletsSummonedPerSecond = (fireRate / 60f) * bulletCount;

        float bulletTotalPotentialLifeSpan = bulletObject.lifeSpanInSeconds + bulletObject.randomizedLifeSpanOffset;

        float maximumBulletsSummoned = bulletsSummonedPerSecond * bulletTotalPotentialLifeSpan;

        return Mathf.CeilToInt(maximumBulletsSummoned);
    }

    public void SummonBullet()
    {
        Vector3 bulletSpawnPoint = getBulletSpawnPoint();

        BaseBullet bullet = bulletInstanceQueue.Dequeue();
        bulletInstanceQueue.Enqueue(bullet);

        bullet.Summon(bulletSpawnPoint, Quaternion.identity);

        ApplySpread(bullet);

        if(holder.IsFacingRight())
        {
            bullet.transform.Rotate(0f, 0f, 180f);
        }
    }

    public Vector3 getBulletSpawnPoint()
    {
        return transform.position + new Vector3(bulletOffset.x * holder.GetFacing(), bulletOffset.y, 0);
    }

    protected float getAttackInterval()
    {
        return 60 / fireRate;
    }


    public void resetAttackCooldown()
    {
        attackCooldown = new CountdownTimer(getAttackInterval());
    }

    protected void ApplyRecoil()
    {
        currentRecoil = recoil;
    }

    protected void WearOffRecoil()
    {
        currentRecoil -= (recoil / 100) * recoilWearOffRate;
        currentRecoil = Mathf.Max(currentRecoil, 0);
    }

    protected void ApplySpread(BaseBullet bullet)
    {
        float currentSpread = Random.Range(-fireSpread, fireSpread);
        bullet.transform.Rotate(new Vector3(0, 0, currentSpread));
    }

    public void UnfreezeGunRotation()
    {
        rb.freezeRotation = false;
    }

    public void FreezeGunRotation()
    {
        rb.freezeRotation = true;
    }

    public void ResetGunRotation()
    {
        rb.SetRotation(0f);
    }

    private void InitializeBulletParent()
    {
        if (bulletParent) return;
        bulletParent = GameObject.Find("Particles").transform;
    }
}
