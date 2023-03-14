using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Attributes")]
    public TextMeshProUGUI diamondsText;
    public Image healthBar;
    public Vector2 healthBarOriginalSize;
    
    [Header("Instances")]
    /*
    * Singleton instantiation 
    * gameManagerInstance - Will save the information of this component
    * and if it does not appear, the Instance method get the information by itself.
    * The it save the return value into Instance, so this could be call from everywhere
    */
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        // DontDestroyOnLoad(this.gameObject);
        if (GameObject.Find("RealGameManager")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "RealGameManager";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void delete() {
        Destroy(gameObject);
    }
}
