using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private int attackPower = 5;

    [Header("Life")]
    [SerializeField] public int health = 100;
    [SerializeField] public int hurtResistance = 10;

    private Rigidbody2D rb;


    [Header("Raycast Behaviour")]
    private RaycastHit2D rightLedgeRc;
    private RaycastHit2D leftLedgeRc;
    private RaycastHit2D rightWallRc;
    private RaycastHit2D leftWallRc;
    private RaycastHit2D rightEnemyRc;
    private RaycastHit2D leftEnemyRc;
    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastVerticalLength = 2;
    [SerializeField] private float rayCastHorizontalLength = 2;
    [SerializeField] private LayerMask rayCastLayerMask;
    private int direction = 1; // it couldn't be 0

    float speed = 2f;
    float exponent = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 1f; // Set the gravity scale to 1 to enable gravity
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement();
        //rayCastMovement();

        /*
        * Health
        */
        if (health <= 0) Destroy(gameObject);
    }

    public void rayCastMovement()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        /*
        * Ray cast to detect elements
        */
        // Ledge right
        rightLedgeRc = Physics2D.Raycast(
                            new Vector2(transform.position.x + rayCastOffset.x, transform.position.y), Vector2.down, rayCastVerticalLength);
        Debug.DrawRay(
                new Vector2(transform.position.x + rayCastOffset.x, transform.position.y), Vector2.down * rayCastVerticalLength, Color.red);
        // Ledge left
        leftLedgeRc = Physics2D.Raycast(
                            new Vector2(transform.position.x - rayCastOffset.x, transform.position.y), Vector2.down, rayCastVerticalLength);
        Debug.DrawRay(
                new Vector2(transform.position.x - rayCastOffset.x, transform.position.y), Vector2.down * rayCastVerticalLength, Color.blue);

        // wall right
        rightWallRc = Physics2D.Raycast(
                            new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastHorizontalLength, rayCastLayerMask);
        Debug.DrawRay(
                new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastHorizontalLength, Color.green);
        // wall left
        leftWallRc = Physics2D.Raycast(
                            new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastHorizontalLength, rayCastLayerMask);
        Debug.DrawRay(
                new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastHorizontalLength, Color.green);

        // enemy right
        rightEnemyRc = Physics2D.Raycast(
                            new Vector2(transform.position.x + (rayCastOffset.x + 0.4f), transform.position.y), Vector2.down, rayCastVerticalLength);
        Debug.DrawRay(
                new Vector2(transform.position.x + (rayCastOffset.x + 0.4f), transform.position.y), Vector2.down * rayCastVerticalLength, Color.white);
        // wall left
        leftEnemyRc = Physics2D.Raycast(
                            new Vector2(transform.position.x - (rayCastOffset.x + 0.4f), transform.position.y), Vector2.down, rayCastVerticalLength);
        Debug.DrawRay(
                new Vector2(transform.position.x - (rayCastOffset.x + 0.4f), transform.position.y), Vector2.down * rayCastVerticalLength, Color.white);

        /*
        * Setting direction left or right to the enemy
        */
        if (rightLedgeRc.collider == null) direction = -1;
        if (leftLedgeRc.collider == null) direction = 1;

        if (rightWallRc.collider != null) direction = -1;
        if (leftWallRc.collider != null) direction = 1;

        if (rightEnemyRc.collider != null && rightEnemyRc.collider.CompareTag("EnemyTag")) direction = -1;
        if (leftEnemyRc.collider != null && leftEnemyRc.collider.CompareTag("EnemyTag")) direction = 1;
    }

    public void enemyMovement()
    {
        //float time = Time.time;
        //float newPositionX = Mathf.Pow(time, exponent) * speed;
        //transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

        float newPositionX = 1f * Time.deltaTime; // Calculate the first movement component
        float newPositionY = 4f * Time.deltaTime; // Calculate the second movement component

        transform.position += new Vector3(newPositionX, newPositionY, 0f);
    }

    float powerRule(float speed, float exponent)
    {
        float time = Time.time;
        Debug.Log(time);
        float positionX = Mathf.Pow(time * speed, exponent);
        return positionX;
        //float a = 3f; // constant coefficient
        //float n = 2f; // exponent
        //return a * Mathf.Pow(x, n); // power rule: d/dx(x^n) = n * x^(n-1)
    }

    private void OnCollisionEnter2D(Collision2D colEnemy) {
        if(colEnemy.gameObject == PlayerBehaviour.Instance.gameObject) {
            //if(PlayerBehaviour.Instance.health > 0) PlayerBehaviour.Instance.health -= attackPower;
            //PlayerBehaviour.Instance.UpdateUI();
        }
    }
}
