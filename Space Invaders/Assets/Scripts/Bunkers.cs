using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Invaders"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
