using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public int knockBack;
    public int damage;

    public Rigidbody2D rb;

    public HealthBar healthBar;

    public Animator animator;

    public GameObject deathScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 force = (rb.transform.position - collision.transform.position).normalized;
            animator.SetTrigger("Hit");
            TakeDamage(damage);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            animator.SetBool("Dead", true);
            Time.timeScale = 0f;
            deathScreen.SetActive(true);
        }
    }

    public void HealDamage(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
    }
}
