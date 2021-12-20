using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public GameObject Actor;
    public bool enables;
    public LayerMask WSM;
    bool inUse = false;


    public void Update()
    {
        if (!Physics2D.OverlapCircle(transform.position, 0.2f, WSM))
        {
            Actor.SetActive(!enables);
        }
        else if(Physics2D.OverlapCircle(transform.position, 0.2f, WSM))
        {
            Actor.SetActive(enables);
        }
    }
}
    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Clone")
        {
            inUse = true;
            Actor.SetActive(enables);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Clone")
        {
            inUse = false;
            Actor.SetActive(!enables);
        }
    }
*/