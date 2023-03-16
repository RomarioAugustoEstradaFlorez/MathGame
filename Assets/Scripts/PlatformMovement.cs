using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform[] positions;
    private int nextPosIndex;

    private Transform nextPosition;
    public float moveSpeed;

    /*
    * Singleton instantiation 
    * platformInstance - Will save the information of this component
    * and if it does not appear, the Instance method get the information by itself.
    * The it save the return value into Instance, so this could be call from everywhere
    */
    private static PlatformMovement platformInstance;
    public static PlatformMovement Instance
    {
        get
        {
            if (platformInstance == null) platformInstance = GameObject.FindObjectOfType<PlatformMovement>();
            return platformInstance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = positions[0];    
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == nextPosition.position)
        {
            nextPosIndex++;
            if(nextPosIndex >= positions.Length) nextPosIndex = 0;

            nextPosition = positions[nextPosIndex];
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerBehaviour.Instance.gameObject) {
            collision.gameObject.transform.SetParent(transform);
            PlayerBehaviour.Instance.platformLanded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerBehaviour.Instance.gameObject)
        {
            collision.transform.SetParent(null);
            PlayerBehaviour.Instance.platformLanded = false;
        }
    }
}
