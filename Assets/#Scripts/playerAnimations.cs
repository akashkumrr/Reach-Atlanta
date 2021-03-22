using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimations : MonoBehaviour
{
    public Animator playerAnimators;
    public AnimationClip playerWalkingClip;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimators.Play(playerWalkingClip.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
