using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] Vector2 bulletOffset;
    [SerializeField] public GameObject bulletObject;
    [SerializeField] public Sprite muzzleFlash;
    [SerializeField] public float fireRate;
    [SerializeField] public float bulletCount;
    [SerializeField] public float fireSpread;
    [SerializeField] public Vector2 weaponHoldOffset;
    [SerializeField] public float weaponGravity;
    [SerializeField] public float recoil;
    [SerializeField] public float weaponDrag;
    [SerializeField] public Sprite weaponSprite;
    [SerializeField] public Sprite heldWeaponSprite;

    private Vector2 targetPosition = Vector2.zero;
    public CountdownTimer attackCooldown;
    public float currentRecoil = 0;

    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D coll { get; private set; }

    public PlayerController holder = null;

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

    public void SpawnBullet()
    {
        Vector3 bulletSpawnPoint = getBulletSpawnPoint();
        GameObject bullet = Instantiate(bulletObject, bulletSpawnPoint, Quaternion.identity);

        ApplySpread(bullet);

        if(!holder.IsFacingRight())
        {
            bullet.transform.Rotate(0f, 0f, 180f);
        }
    }

    public Vector3 getBulletSpawnPoint()
    {
        return transform.position + new Vector3(bulletOffset.x * holder.playerFacing, bulletOffset.y, 0);
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
        currentRecoil -= recoil / 100;
        currentRecoil = Mathf.Max(currentRecoil, 0);
    }

    protected void ApplySpread(GameObject bullet)
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
}
