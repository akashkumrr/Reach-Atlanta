using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missionComplete : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject continueButtonGameobject;
    void Start()
    {
        levelManager.onMissionComplete += setContinueButtonAsActive;
        continueButtonGameobject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setContinueButtonAsActive()
    {
        continueButtonGameobject.SetActive(true);
    }

    public void loadNextLevel()
    {
        Debug.Log("Next level loaded");
    }
}
