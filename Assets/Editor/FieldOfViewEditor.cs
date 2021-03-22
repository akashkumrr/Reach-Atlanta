using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

    void OnSceneGUI()
    {   /*
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.fowardDirectionHelper.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, true);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, true);

        Handles.color = Color.black;
        Handles.DrawLine(fow.fowardDirectionHelper.position, fow.fowardDirectionHelper.position + viewAngleA * fow.viewRadius);
        Handles.color = Color.green;
        Handles.DrawLine(fow.fowardDirectionHelper.position, fow.fowardDirectionHelper.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        
         * foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.fowardDirectionHelper.position, visibleTarget.position);
        }
        */
    }

}