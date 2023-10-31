using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAi : MonoBehaviour
{
    public AudioManager am;
    //public AIPath aiPath; 
    public Animator animator;
    public Transform target;

    public float speed;
    public float nextWaypointDistance = 3f;

    private Pathfinding.Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public HealthBar healthBar;

    Seeker seeker;
    Rigidbody2D rb;

    public int maxHealth = 50;
    public int currentHealth = 50;
    public int knockBack;
    public int damage;
    public int fireballDamage;

    public bool isDead = false;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDead)
        {
            Vector2 movement = new Vector2(rb.velocity.x, rb.velocity.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetFloat("Horizontal", rb.velocity.x);
            animator.SetFloat("Vertical", rb.velocity.y);

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            //Vector2 force = direction * speed * Time.deltaTime;

            rb.velocity = direction * speed;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            am.Play("Basic Hit");
            Vector2 force = (rb.transform.position - collision.transform.position).normalized;
            animator.SetTrigger("Hit");
            TakeDamage(15);
        }
        if (collision.gameObject.tag == "Fireball")
        {
            am.Play("Fireball Hit");
            Vector2 force = (rb.transform.position - collision.transform.position).normalized;
            animator.SetTrigger("Hit");
            TakeDamage(fireballDamage);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            animator.SetBool("Dead", true);
            isDead = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
