using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public AudioManager am;
    public GameObject indicator;
    public Animator animator;
    public Transform LaunchOffset;
    public FireballBehaviour fireball;

    public GameObject shield;
    public float shieldTime;

    public Movement movement;
    public float speedTime;
    public float speedMultiplier;

    public Player player;
    public int healAmount;

    private IEnumerator coroutine;

    // Fireball Ability
    public void FireBall()
    {
        am.Play("Fireball");
        animator.SetTrigger("Fireball");
        Vector2 touchPos = new Vector2(indicator.transform.position.x, indicator.transform.position.y);
        Vector2 dir = touchPos - (new Vector2(LaunchOffset.position.x, LaunchOffset.position.y));
        FireballBehaviour bullet = Instantiate(fireball, LaunchOffset.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().velocity = dir * bullet.speed * Time.deltaTime;
    }

    // Shield Ability
    public void Shield()
    {
        am.Play("Shield");
        shield.SetActive(true);
        coroutine = ShieldTimer();
        StartCoroutine(coroutine);
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldTime);
        shield.SetActive(false);
    }

    // Wind Ability
    public void SpeedBoost()
    {
        am.Play("Speed");
        float baseSpeed = movement.speed;
        movement.speed *= speedMultiplier;
        coroutine = SpeedTimer(baseSpeed);
        StartCoroutine(coroutine);
    }

    private IEnumerator SpeedTimer(float baseSpeed)
    {
        yield return new WaitForSeconds(speedTime);
        movement.speed = baseSpeed;
    }

    // Water Heal
    public void Heal()
    {
        am.Play("Heal");
        player.HealDamage(healAmount);
    }
}
