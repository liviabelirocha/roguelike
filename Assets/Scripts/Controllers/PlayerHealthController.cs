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

    [SerializeField]
    private int

            sfxIndexHurt,
            sfxIndexDeath;

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
            MakeInvincible (damageInvincibleLength);

            UIController.instance.SetHealth (maxHealth, currentHealth);

            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX (sfxIndexDeath);
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.ToggleDeathScreen(true);
            }
            else
                AudioManager.instance.PlaySFX(sfxIndexHurt);
        }
    }

    public void MakeInvincible(float length)
    {
        invincibleCount = length;
        PlayerController.instance.SetAlpha(0.5f);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UIController.instance.SetHealth (maxHealth, currentHealth);
    }
}
