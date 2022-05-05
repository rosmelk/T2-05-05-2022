using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;
    void Start()
    {
        
    }

    
    void Update()
    {
        var z = this.transform.position.z;
        this.transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, z);

    }
}
