/**	
 *  Project 1 - Lego Blocks
 *  CameraManager.cs
 *  Purpose: Positions the main camera and handles
 *  user interaction with the camera.
 *  
 *  @author Leslie Hoyt
 *  10/8/19
 *  COMP 465
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // the target object
    private float speedMod = 10.0f; // a speed modifier
    private Vector3 point; // the coord to the point where the camera looks at

    // Start is called before the first frame update
    void Start()
    {
        #region Setup Camera
        // point to rotate around
        point = GameObject.Find("_GameManager").GetComponent<GameMaster>().platformCenter;
        // Position the camera in the center
        transform.position = new Vector3(point.x - 1.6f, 2.0f, point.z - 1.6f);
        transform.LookAt(point); // makes the camera look at it
        transform.eulerAngles = new Vector3(40, 45, 0); // isometric view
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Update camera position with user input
        // make the camera rotate around 'point' coords,
        // rotating around its Y axis, 20 degrees per second times the speed modifier

        // Rotate right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        }
        // Rotate left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        }
        // Move camera up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * Time.deltaTime * speedMod;
        }
        // Move camera down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += transform.up * (-1) * Time.deltaTime * speedMod;
        }
        // Zoom camera in
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += transform.forward * Time.deltaTime * speedMod;
        }
        // Zoom camera out
        if (Input.GetKey(KeyCode.X))
        {
            transform.position += transform.forward * (-1) * Time.deltaTime * speedMod;
        }
        #endregion
    }
}
