using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private string levelToGo;
    [SerializeField] private GameObject SpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject == PlayerBehaviour.Instance.gameObject) {
            SceneManager.LoadScene(levelToGo);

            /* The realPLayer will be in the position set it to the Spawn Element */
            PlayerBehaviour.Instance.gameObject.transform.position = SpawnPosition.transform.position;
        }
    }
}
