using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : MonoBehaviour
{
    private Rigidbody2D rb;

    private float xSpeed;
    private float ySpeed;
    [SerializeField] private Transform enemyCheck;
   [SerializeField] private float enemyCheckRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    public void SetupSpeed(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }

    private void CheckForEnemy()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();

                if(newEnemy.invincible)
                    return;

                if(rb.velocity.y < 0)
                {
                    newEnemy.Damage();
                }
                
            }
        }
    }
}


