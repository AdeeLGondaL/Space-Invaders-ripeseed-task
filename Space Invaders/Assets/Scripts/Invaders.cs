using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public int rows = 5;
    public int cols = 11;

    public Invader[] prefabs;

    Vector3 DirectionOfInvaders = Vector3.right;
    public AnimationCurve SpeedOfInvaders;
    public Projectiles MissilePrefab;

    public float MissileLaunchRate = 1.0f;

    public int InvadersKilled { get; private set; }
    public int TotalInvaders => this.rows * this.cols;
    public int InvadersAlive => this.TotalInvaders - this.InvadersKilled;
    public float percentKilled => (float)this.InvadersKilled / (float)this.TotalInvaders;

    private void Awake()
    {
        for(int row = 0; row < rows; row++)
        {
            float width = 2.0f * (this.cols - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 centreTheInvaders = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centreTheInvaders.x, centreTheInvaders.y + (row * 2.0f), 0.0f);

            for(int col = 0; col < cols; col++)
            {
                Invader invader = Instantiate(prefabs[row], this.transform);
                invader.Killed = Invaderkilled;
                Vector3 positionOfInvader = rowPosition;
                positionOfInvader.x += col * 2.0f;
                invader.transform.localPosition = positionOfInvader;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(Missiles), this.MissileLaunchRate, this.MissileLaunchRate);
    }

    void Update()
    {
        this.transform.position += DirectionOfInvaders * Time.deltaTime * SpeedOfInvaders.Evaluate(this.percentKilled);

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right); 

        foreach(Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if((DirectionOfInvaders == Vector3.right) && invader.position.x >= (rightEdge.x - 1.0f))
            {
                DirectionAndRowUpdate();
            }
            else if((DirectionOfInvaders == Vector3.left) && invader.position.x <= (leftEdge.x + 1.0f))
            {
                DirectionAndRowUpdate();
            }
        }
    }

    private void DirectionAndRowUpdate()
    {
        DirectionOfInvaders.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void Missiles()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.InvadersAlive))
            {
                Instantiate(this.MissilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
        }

    private void Invaderkilled()
    {
        this.InvadersKilled++;

        if(this.InvadersKilled >= this.TotalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
