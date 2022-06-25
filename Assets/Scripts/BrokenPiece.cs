using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float deceleration = 5f;

    [SerializeField]
    private float lifeTime = 3f;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private float fadeSpeed = 2.5f;

    private Vector3 moveDirection;

    void Start()
    {
        moveDirection.x = GetMoveDirection();
        moveDirection.y = GetMoveDirection();
    }

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection =
            Vector3
                .Lerp(moveDirection,
                Vector3.zero,
                deceleration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            sr.color =
                new Color(sr.color.r,
                    sr.color.g,
                    sr.color.b,
                    Mathf
                        .MoveTowards(sr.color.a,
                        0f,
                        fadeSpeed * Time.deltaTime));

            if (sr.color.a == 0f) Destroy(gameObject);
        }
    }

    private float GetMoveDirection()
    {
        return Random.Range(-moveSpeed, moveSpeed);
    }
}
