using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    void Update()
    {
        this.transform.position +=  direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyed != null)
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
