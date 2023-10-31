using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
    public int hits;
    public int maxHits;
    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if ( hits == maxHits )
        {
            gameObject.SetActive(false);
            hits = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hits++;
        }
    }
}
