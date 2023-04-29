using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballmoves : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 10;
    [SerializeField] private float IncreaseSpeed = 0.25f;
    [SerializeField] private Text playerscore;
    [SerializeField] private Text aiscore;

    private int hitCounter;
    private Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       Invoke("StartBall",2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity,  InitialSpeed +(IncreaseSpeed * hitCounter));
    }

    private void StartBall()   
    {
        rb.velocity = new Vector2(-1, 0) * (InitialSpeed + IncreaseSpeed * hitCounter);
    }

    private void resetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall",2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;

        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }
        Vector2 direction = new Vector2(xDirection, yDirection).normalized;
        rb.velocity = direction * (InitialSpeed + (IncreaseSpeed * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "player1" || collision.gameObject.name == "player2")
        {
            PlayerBounce(collision.transform);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            resetBall();
            playerscore.text = (int.Parse(playerscore.text) + 1).ToString();
        }
        else if(transform.position.x < 0)
        {
            resetBall();
            aiscore.text = (int.Parse(aiscore.text) + 1).ToString();
        }
    }

    
}
