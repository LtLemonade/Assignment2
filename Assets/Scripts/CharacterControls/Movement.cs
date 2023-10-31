using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class Movement : MonoBehaviour
{
    public AudioManager am;
    public Rigidbody2D player;
    public FloatingJoystick joystick;
    public float speed;
    public Animator animator;

    Vector2 movement;

    public GameObject cursor;

    public ProjectileBehavior projectile;
    public Transform LaunchOffset;
    public float offsetValue;

    private Rect bottomLeft = new Rect(0, 0, Screen.width / 2, Screen.height / 2);

    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.touchCount == 1 && movement.sqrMagnitude == 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                am.Play("Basic");
                animator.SetTrigger("Attack");
                cursor.transform.position = touchPos;
                Vector2 dir = touchPos - (new Vector2(LaunchOffset.position.x, LaunchOffset.position.y));
                ProjectileBehavior bullet = Instantiate (projectile, LaunchOffset.position, Quaternion.identity);

                Vector3 touchPos3 = new Vector3(touchPos.x, touchPos.y, LaunchOffset.position.z);
                Vector3 vectorToTarget = touchPos3 - LaunchOffset.position;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

                Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

                bullet.transform.rotation = targetRotation;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bullet.speed * Time.deltaTime;
            }
        }
        else if(Input.touchCount > 1 && movement.sqrMagnitude > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Touch touch = Input.GetTouch(1);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Began)
            {
                am.Play("Basic");
                animator.SetTrigger("Attack");
                cursor.transform.position = touchPos;
                Vector2 dir = touchPos - (new Vector2(LaunchOffset.position.x, LaunchOffset.position.y));
                ProjectileBehavior bullet = Instantiate(projectile, LaunchOffset.position, Quaternion.identity);

                Vector3 touchPos3 = new Vector3(touchPos.x, touchPos.y, LaunchOffset.position.z);
                Vector3 vectorToTarget = touchPos3 - LaunchOffset.position;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

                Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

                bullet.transform.rotation = targetRotation;
                bullet.GetComponent<Rigidbody2D>().velocity = (dir * bullet.speed * Time.deltaTime);
            }

        }
    }
    void FixedUpdate()
    {
        Vector2 direction = Vector2.up * movement.y + Vector2.right * movement.x;
        player.velocity = direction * speed;
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}