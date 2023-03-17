using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : PhysicsObject
{
    [Header("Attributes")]
    [Tooltip("The speed when the player walks")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private ItemType playerType; // "Square": first and important player "Skeleton": test
    //[SerializeField] public Light2D playerLight;

    enum ItemType { Square, Skeleton } // Creates an ItemType enum (drop down)
    public float VelocityX;
    
    [Header("Movements")]
    private float horizontalMove;
    private bool jumpMove;
    private bool punchMove;

    [Header("Instances")]
    private GameManager gm;

    public bool platformLanded;

    /*
    * Singleton instantiation 
    * playerInstance - Will save the information of this component
    * and if it does not appear, the Instance method get the information by itself.
    * The it save the return value into Instance, so this could be call from everywhere
    */
    private static PlayerBehaviour playerInstance;
    public static PlayerBehaviour Instance
    {
        get
        {
            if (playerInstance == null) playerInstance = GameObject.FindObjectOfType<PlayerBehaviour>();
            return playerInstance;
        }
    }

    private void Awake()
    {
        /* Here the player object is destroyed */
        if (GameObject.Find("RealPlayer")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        /* Here the player is created again */
        DontDestroyOnLoad(gameObject);
        gameObject.name = "RealPlayer";

        gm = GameManager.instance;

        //gm.diamondsText = GameObject.Find("Diamonds").GetComponent<TextMeshProUGUI>();
        //gm.healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        //gm.healthBarOriginalSize = gm.healthBar.rectTransform.sizeDelta;

        SetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (getPLayerType() == "Square")
        {
            movementPlayer();

            /* Flip horizontal when the player moves left or right */
            if (targetVelocity.x < -0.01) transform.localScale = new Vector2(-1, 1);
            if (targetVelocity.x > 0.01) transform.localScale = new Vector2(1, 1);
   
        }

    }

    public void movementPlayer()
    {
        /* To move horizontally using linear function */
        float horizontalMove = Input.GetAxis("Horizontal");
        targetVelocity = new Vector2(horizontalMove * movementSpeed, 0);

        /* To Jump using square root function */
        bool jumpMove = Input.GetButtonDown("Jump");
        if (jumpMove && grounded)
        {
            float jumpTime = Mathf.Sqrt(2f * jumpForce / gravityModifier);
            velocity.y = jumpTime * gravityModifier;
        }

        // Remove parent-child relationship if player object is on platform but not stationary
        if (targetVelocity.magnitude > 4 && platformLanded == true) {
            transform.SetParent(null);
        }
        if (targetVelocity.magnitude < 4 && platformLanded == false) {
            transform.SetParent(transform);
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*
    * This set the player into hes spawn element position
    */
    public void SetSpawnPosition()
    {
        if (GameObject.Find("SpawnPosition"))
        {
            transform.position = GameObject.Find("SpawnPosition").transform.position;
            GameObject.Find("SpawnPosition").SetActive(false);
        }
    }

    /*
    * getPLayerType - This gets the player evolution
    */
    private string getPLayerType()
    {
        string name = "";
        if (playerType == ItemType.Square) name = "Square";
        if (playerType == ItemType.Skeleton) name = "Skeleton";
        return name;
    }
}
