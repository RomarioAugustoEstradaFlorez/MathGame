using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == EnemyBehaviour.Instance.gameObject)
        {
            var newDirection = EnemyBehaviour.Instance.direction;

            newDirection = newDirection * -1;

            EnemyBehaviour.Instance.direction = newDirection;
        }
    }
}
