using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Transform gunArm;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float moveSpeed;

    private Vector2 moveInput;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;

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

        // switching between idle and moving
        anim.SetBool("isMoving", moveInput != Vector2.zero);
    }
}
