using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private int healAmount = 1;

    [SerializeField]
    private float waitToBeCollected = .5f;

    [SerializeField]
    private int sfxIndex;

    void Start()
    {
    }

    void Update()
    {
        if (waitToBeCollected > 0) waitToBeCollected -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            PlayerHealthController.instance.Heal (healAmount);
            Destroy (gameObject);
            AudioManager.instance.PlaySFX (sfxIndex);
        }
    }
}
