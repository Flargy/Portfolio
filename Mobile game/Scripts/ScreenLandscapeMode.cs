using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLandscapeMode : MonoBehaviour {

	void Start () {
        Screen.autorotateToLandscapeLeft = true;

        Screen.autorotateToLandscapeRight = true;

        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;

        //Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
