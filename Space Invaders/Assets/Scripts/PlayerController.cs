using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Projectiles BulletPrefab;
    public float speedOfPlayer = 5.0f;

    private bool BulletInScene;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * speedOfPlayer * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * speedOfPlayer * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if(!BulletInScene)
        {
            BulletInScene = true;
            Projectiles bullet = Instantiate(this.BulletPrefab, this.transform.position, Quaternion.identity);
            bullet.destroyed += bulletDestroyed;
        }
    }

    private void bulletDestroyed()
    {
        BulletInScene = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Invaders") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
