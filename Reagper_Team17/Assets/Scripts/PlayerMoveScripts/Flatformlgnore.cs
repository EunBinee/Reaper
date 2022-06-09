using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatformlgnore : MonoBehaviour
{
    public BoxCollider2D platformCollider;
    GameObject player;
    PlayerController playerController;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<CapsuleCollider2D>(), platformCollider, true);
        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<CapsuleCollider2D>(), platformCollider, false);
        }
    }
}
