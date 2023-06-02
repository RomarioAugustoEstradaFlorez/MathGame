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
    
    public int direction = 1; // it couldn't be 0

    float speed = 2f;
    float exponent = 2f;


    /*
    * Singleton instantiation 
    * enemyInstance - Will save the information of this component
    * and if it does not appear, the Instance method get the information by itself.
    * The it save the return value into Instance, so this could be call from everywhere
    */
    private static EnemyBehaviour enemyInstance;
    public static EnemyBehaviour Instance
    {
        get
        {
            if (enemyInstance == null) enemyInstance = GameObject.FindObjectOfType<EnemyBehaviour>();
            return enemyInstance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 1f; // Set the gravity scale to 1 to enable gravity
    }

    // Update is called once per frame
    void Update()
    {
        enemyJumpingMovement();
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

    public void enemyJumpingMovement()
    {
        float newPositionX = 3f * Time.deltaTime; // Calculate the first movement component in x
        float newPositionY = 4f * Time.deltaTime; // Calculate the second movement component in y

        transform.position += new Vector3(newPositionX * direction, newPositionY, 0f);
    }

    private void OnCollisionEnter2D(Collision2D colEnemy) {
        if(colEnemy.gameObject == PlayerBehaviour.Instance.gameObject) {
            //if(PlayerBehaviour.Instance.health > 0) PlayerBehaviour.Instance.health -= attackPower;
            //PlayerBehaviour.Instance.UpdateUI();
        }
    }
}
