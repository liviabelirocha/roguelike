using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 7.5f;

    [SerializeField]
    private GameObject impactEffect;

    [SerializeField]
    private int damage = 50;

    private int

            sfxIndexBullet = 15,
            sfxIndexImpact = 4;

    void Start()
    {
        AudioManager.instance.PlaySFX (sfxIndexBullet);
    }

    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        other.GetComponent<EnemyController>()?.DamageEnemy(damage);
        other.GetComponent<Breakables>()?.DestroyBreakable();

        AudioManager.instance.PlaySFX (sfxIndexImpact);

        Destroy (gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy (gameObject);
    }
}
