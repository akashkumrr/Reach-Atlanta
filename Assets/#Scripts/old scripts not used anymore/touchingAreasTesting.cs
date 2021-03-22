using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingAreasTesting : MonoBehaviour
{
    Rect touchable_area;

    public float percentageAreaOfScreen=0.75f;
    public bool insideTouchArea;
    public bool whetherScreenTouched = false;
    public float touchXcoordinate;
    public float touchYcoordinate;

    private void Start()
    {
        #region Touch Handler (Touch will not work in these rectangles)
        //Setting any of xMin, xMax, yMin and yMax will resize the rectangle
        //        touchable_area = new Rect(getX(0), getY(-0), getX(100), getY(300));

        //touchable_area = new Rect(x, y, w, h);
        touchable_area = new Rect(0, 0, Screen.width,Screen.height* percentageAreaOfScreen);
        #endregion
    }

    void OnGUI()
    {
        GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        GUI.Box(touchable_area, "");
    }

    void Update()
    {
        /*

        if (Application.isEditor)
        {
            // Again I'm tellig u  L E F T is firing finger
            whetherScreenTouched = Input.GetMouseButtonDown(0);
            touchXcoordinate = Input.mousePosition.x;
            touchYcoordinate = Input.mousePosition.y;
        }
        */

        if (whetherScreenTouched && touchable_area.Contains(new Vector2(touchXcoordinate,Screen.height-touchYcoordinate)))
        {
           // Debug.Log("INSIDE TOUCH AREA");
            insideTouchArea = true;

        }
        if (whetherScreenTouched && !touchable_area.Contains(new Vector2(touchXcoordinate, Screen.height - touchYcoordinate)))
        {
            //Debug.Log("NO! outside touch area");
            insideTouchArea = false;

        }
    }


    public bool checkWhetherInsideOrOutsideTouchArea()
    {
        if (whetherScreenTouched && touchable_area.Contains(new Vector2(touchXcoordinate, Screen.height - touchYcoordinate)))
        {
            // Debug.Log("INSIDE TOUCH AREA");
            insideTouchArea = true;

        }
        if (whetherScreenTouched && !touchable_area.Contains(new Vector2(touchXcoordinate, Screen.height - touchYcoordinate)))
        {
            //Debug.Log("NO! outside touch area");
            insideTouchArea = false;

        }

        return insideTouchArea;
    }
}
