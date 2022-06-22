using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 direction;

    void Start()
    {
        direction =
            PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            PlayerHealthController.instance.DamagePlayer();

        Destroy (gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy (gameObject);
    }
}
