using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int currentHealth;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D coll;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
    }

    protected void Update()
    {
        Death();
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }

    protected void Death()
    {
        if (currentHealth > 0) return;
        GameObject.Destroy(gameObject);
    }
}
