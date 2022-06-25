using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Transform gunArm;

    [SerializeField]
    private Animator anim;

    // bullets
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float timeBetweenShots;

    private float shotCounter;

    // player
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private SpriteRenderer bodySR;

    private Vector2 moveInput;

    private Camera cam;

    private float activeMoveSpeed;

    [SerializeField]
    private float

            dashSpeed = 8f,
            dashLength = .5f,
            dashCooldown = 1f,
            dashInvincibility = .5f;

    private float

            dashCounter,
            dashCooldownCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        // if mouse x < player x that means player should be facing left, else player should face right
        float direction = mousePosition.x < screenPoint.x ? -1f : 1f;
        transform.localScale = new Vector3(direction, 1f, 1f);
        gunArm.localScale = new Vector3(direction, direction, 1f);

        // rotating gun arm
        Vector2 offset =
            new Vector2(mousePosition.x - screenPoint.x,
                mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        // firing a bullet
        if (Input.GetMouseButtonDown(0)) Shoot();

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0) Shoot();
        }

        // dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanDash())
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                PlayerHealthController.instance.MakeInvincible (
                    dashInvincibility
                );
                anim.SetTrigger("dash");
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0) dashCooldownCounter -= Time.deltaTime;

        // switching between idle and moving
        anim.SetBool("isMoving", moveInput != Vector2.zero);
    }

    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        shotCounter = timeBetweenShots;
    }

    public bool IsPlayerActive()
    {
        return gameObject.activeInHierarchy;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetAlpha(float alpha)
    {
        bodySR.color =
            new Color(bodySR.color.r, bodySR.color.g, bodySR.color.b, alpha);
    }

    private bool CanDash()
    {
        return dashCooldownCounter <= 0 && dashCounter <= 0;
    }
}
