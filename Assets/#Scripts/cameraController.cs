using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Vector3 cameraTargetPosition;
    public Transform playerTransform;

    public Vector3 offset;
    public joyStickIsShootDirection theShootingJoystickScript;
    public Vector3 lastNonShootingCameraPosition;
    [Space(10)]
    public float weaponRangeToGiveIdeaHowFarWeCanSee;
    public float cameraSnappingToFinalPositionSpeed;
    [Space(20)]
    public Vector3 shootingUpDirectionAccordingToCamera;
    public float calculateOffsetBasedForShootingAngle;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        offset = transform.position - playerTransform.position;

        shootingUpDirectionAccordingToCamera = Vector3.ProjectOnPlane(Camera.main.transform.up, Vector3.up);
        calculateOffsetBasedForShootingAngle = Vector3.SignedAngle(Vector3.forward,shootingUpDirectionAccordingToCamera,Vector3.up);
        theShootingJoystickScript.offset_angleDependingUponCameraAngle = calculateOffsetBasedForShootingAngle;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(theShootingJoystickScript.shootJoystickDragged==false)
        {
            cameraTargetPosition = Vector3.Lerp(cameraTargetPosition, playerTransform.position + offset,Time.deltaTime* cameraSnappingToFinalPositionSpeed) ;
            transform.position = cameraTargetPosition;
        }
        else
        {
            lastNonShootingCameraPosition = playerTransform.position + offset;
            cameraTargetPosition = Vector3.Lerp(cameraTargetPosition, lastNonShootingCameraPosition + theShootingJoystickScript.dir* weaponRangeToGiveIdeaHowFarWeCanSee, Time.deltaTime * cameraSnappingToFinalPositionSpeed);
            transform.position = cameraTargetPosition;
        }
        */
        lastNonShootingCameraPosition = playerTransform.position + offset;
        cameraTargetPosition = Vector3.Lerp(cameraTargetPosition, lastNonShootingCameraPosition + theShootingJoystickScript.joystickRotationDirectionVector * weaponRangeToGiveIdeaHowFarWeCanSee, Time.deltaTime * cameraSnappingToFinalPositionSpeed);
        transform.position = cameraTargetPosition;


    }
}
