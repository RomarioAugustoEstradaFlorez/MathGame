using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCameraBehaviour : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    // Start is called before the first frame update
    void Start()
    {   
        // This will follow the actual player instance 
        cinemachineVirtualCamera.Follow = PlayerBehaviour.Instance.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Camera " + cinemachineVirtualCamera.transform.position); 
    }

}
