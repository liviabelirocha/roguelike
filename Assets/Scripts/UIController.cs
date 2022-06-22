using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private GameObject deathScreen;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void SetHealth(int maxHealth, int currentHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text =
            currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void ToggleDeathScreen(bool status)
    {
        deathScreen.SetActive (status);
    }
}
