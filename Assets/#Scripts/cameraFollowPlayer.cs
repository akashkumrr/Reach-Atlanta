using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Awake()
    {
        offset = cameraTransform.position - playerTransform.position;
    }

    // AWAKE works but when calculating offset in start it doesnt work
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = playerTransform.position+offset;
    }

}
