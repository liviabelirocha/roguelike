using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private float damageInvincibleLength = 1f;

    private float invincibleCount;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.SetHealth (maxHealth, currentHealth);
    }

    void Update()
    {
        if (invincibleCount > 0)
        {
            invincibleCount -= Time.deltaTime;
            if (invincibleCount <= 0) PlayerController.instance.SetAlpha(1f);
        }
    }

    public void DamagePlayer()
    {
        if (invincibleCount <= 0)
        {
            currentHealth--;
            invincibleCount = damageInvincibleLength;

            UIController.instance.SetHealth (maxHealth, currentHealth);
            PlayerController.instance.SetAlpha(0.5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.ToggleDeathScreen(true);
            }
        }
    }
}
