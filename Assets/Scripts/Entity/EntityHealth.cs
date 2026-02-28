using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool isAlive;

    public Animator anim;
    public Slider healthSlider;
    public TMP_Text healthText;
    public Slider healthBar;

    private void Start()
    {
        if (!CompareTag("Player"))
        {
            healthBar = GetComponentInChildren<Slider>();
            healthBar.gameObject.SetActive(EnemyManager.instance.InCombat);

            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (CompareTag("Player"))
        {
            healthText.text = currentHealth + " / " + maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        anim.SetInteger("health", currentHealth);
    }

    // Set player health on slider
    void FixedUpdate()
    {
        if (CompareTag("Player"))
        {
            healthText.text = currentHealth + " / " + maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        anim.SetInteger("health", currentHealth);
    }

    public void ChangeHealth(int amount)
    {
        if (!isAlive) return;

        if (amount + currentHealth >= maxHealth) currentHealth = maxHealth;
        else
        {
            currentHealth += amount;
            if (currentHealth < 0) currentHealth = 0;
        }

        if (!CompareTag("Player"))
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (currentHealth == 0)
        {
            isAlive = false;
            StartCoroutine(Kill());
        }
    }

    public IEnumerator Kill()
    {
        anim.Play("Death");
        // Wait one frame so the animator switches states
        yield return null;
        float deathLength = anim.GetCurrentAnimatorStateInfo(0).length - 0.1f;
        yield return new WaitForSeconds(deathLength);
        gameObject.SetActive(false);
        yield break;
    }
}
