using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") Damage();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") Damage();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") Damage();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") Damage();
    }

    private void Damage()
    {
        PlayerHealthController.instance.DamagePlayer();
    }
}
