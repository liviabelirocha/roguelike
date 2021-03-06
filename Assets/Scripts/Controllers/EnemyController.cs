using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer body;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rangeToChasePlayer;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private int health = 150;

    [SerializeField]
    private GameObject[] deathSplatter;

    [SerializeField]
    private GameObject impactEffect;

    // shooting
    [SerializeField]
    private bool shouldShoot;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float shootRange;

    private float fireCounter;

    private Vector3 moveDirection;

    [SerializeField]
    private int

            sfxIndexDeath,
            sfxIndexHurt;

    void Start()
    {
    }

    void Update()
    {
        if (body.isVisible && PlayerController.instance.IsPlayerActive())
        {
            // chase player
            if (
                Vector3
                    .Distance(transform.position,
                    PlayerController.instance.GetPosition()) <=
                rangeToChasePlayer
            )
            {
                moveDirection =
                    PlayerController.instance.GetPosition() -
                    transform.position;
                moveDirection.Normalize();
            }
            else
                moveDirection = Vector3.zero;

            rb.velocity = moveDirection * speed;

            // shoot
            if (
                shouldShoot &&
                Vector3
                    .Distance(transform.position,
                    PlayerController.instance.GetPosition()) <=
                shootRange
            )
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    fireCounter = fireRate;
                }
            }
        }
        else
            rb.velocity = Vector2.zero;

        // switching between idle and moving
        anim.SetBool("isMoving", moveDirection != Vector3.zero);
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        Instantiate(impactEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            Destroy (gameObject);
            AudioManager.instance.PlaySFX (sfxIndexDeath);

            int selectedSplatterIndex = Random.Range(0, deathSplatter.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deathSplatter[selectedSplatterIndex],
            transform.position,
            Quaternion.Euler(0f, 0f, rotation * 90f));
        }
        else
        {
            AudioManager.instance.PlaySFX (sfxIndexHurt);
        }
    }
}
