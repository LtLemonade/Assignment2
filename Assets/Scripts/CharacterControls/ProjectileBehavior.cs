using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed;
    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
