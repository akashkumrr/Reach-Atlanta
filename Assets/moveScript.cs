using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript : MonoBehaviour
{

    public float horDir;
    public float verDir;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        horDir = Input.GetAxis("Horizontal");
        verDir = Input.GetAxis("Vertical");

        transform.position += new Vector3(horDir, 0, verDir)*moveSpeed*Time.deltaTime;

    }
}
