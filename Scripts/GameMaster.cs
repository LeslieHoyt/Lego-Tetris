/**	
 *  Project 1 - Lego Blocks
 *  GameMaster.cs
 *  Purpose: Handles the main logic of the game loop,
 *  by building the platform and dropping Lego blocks.
 *  
 *  @author Leslie Hoyt
 *  10/8/19
 *  COMP 465
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public GameObject legoPlateUnitPref;

    public GameObject[] legoBlockUnityPref;

    public Material[] blockMaterials;

    public float deltaTime = 0.5f;
    float resetTime;

    float x, z;
    float startX = -1f;
    float startZ = -1f;

    public Vector3 boundaryBegin, boundaryEnd;
    public Vector3 platformCenter;

    int[] rotations;

    public int platformSize = 25;

    public bool simMode;
    public bool gameMode;
    public bool breakable;

    private void OnEnable()
    {
        EventManager.StartListening("Spawn", Spawn);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Spawn", Spawn);
    }
    // Start is called before the first frame update
    void Start()
    {
        #region Create the Base
        if (PlayerPrefs.HasKey("M"))
        {
            platformSize = Convert.ToInt32(PlayerPrefs.GetString("M"));
        }
        
        for (int i = 0; i < platformSize; i++)
        {
            for (int j = 0; j < platformSize; j++)
            {
                x = (i * 0.08f) + (startX + 0.04f);
                z = (j * 0.08f) + (startZ + 0.04f);
                var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(x, 0, z), 
                    Quaternion.identity);
                gObj.tag = "base_tag";
            }
        }
        // set the boundaries
        boundaryBegin = new Vector3((startX + 0.04f), 0, (startZ + 0.04f));
        boundaryEnd = new Vector3(x,0,z);
        // find the center of the platform
        platformCenter = new Vector3((startX+0.04f+x)/2,0,(startZ+0.04f+z)/2);
        #endregion

        #region Setup game play settings
        rotations = new int[] { 0, 90, 180, 360 };
        resetTime = Time.time + deltaTime;

        if (PlayerPrefs.HasKey("Breakable"))
        {
            breakable = Convert.ToBoolean(PlayerPrefs.GetString("Breakable"));
        }
        else
        {
            // By default, turn off breakable
            breakable = false;
        }
        if (PlayerPrefs.HasKey("SimMode"))
        {
            simMode = Convert.ToBoolean(PlayerPrefs.GetString("SimMode"));
        }
        if (PlayerPrefs.HasKey("GameMode"))
        {
            gameMode = Convert.ToBoolean(PlayerPrefs.GetString("GameMode"));
            if (gameMode == true)
            {
                Spawn();
            }
        }
        else
        {
            // By default be in simulation mode
            simMode = true;
        }
        #endregion
    }

    /** Generates a Lego block in simulation mode
     *  @param none
     *  @return none
    */
    void SimulationMode()
    {
        #region (Simulation Mode) Generate a block based on the reset time
        if (resetTime < Time.time)
        {
            x = (UnityEngine.Random.Range(0, 25) * 0.08f) + (startX + 0.04f);
            z = (UnityEngine.Random.Range(0, 25) * 0.08f) + (startX + 0.04f);
            GameObject gObj = GameObject.Instantiate(legoBlockUnityPref[UnityEngine.Random.Range(0, 22)],
                new Vector3(x, UnityEngine.Random.Range(1, 3), z), 
                Quaternion.Euler(0,rotations[UnityEngine.Random.Range(0,rotations.Length)],0));
            
            // Make sure not to create blocks that are out of bounds
            if (OutOfBounds(gObj) == true)
            {
                // Destroy it
                Destroy(gObj);
                // Now return control immediately (otherwise lag occurs)
                return;
            }
            else
            {
                Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
                foreach (var r in gObj.GetComponentsInChildren<Renderer>())
                {
                    r.material = m;
                }
                // Attach applicable scripts to the blocks
                gObj.AddComponent<BlockObject>();
            }

            //Destroy(gObj, deltaTime + 15);
            resetTime = Time.time + deltaTime;
        }
        #endregion
    }

    /** Spawns a Lego block for playable game mode
     *  @param none
     *  @return none
    */
    void Spawn()
    {
        #region Spawn a Lego block
        x = (UnityEngine.Random.Range(0, 25) * 0.08f) + (startX + 0.04f);
        z = (UnityEngine.Random.Range(0, 25) * 0.08f) + (startX + 0.04f);
        var gObj = GameObject.Instantiate(legoBlockUnityPref[UnityEngine.Random.Range(0, 22)],
            new Vector3(x, UnityEngine.Random.Range(1, 3), z),
            Quaternion.Euler(0, rotations[UnityEngine.Random.Range(0, rotations.Length)], 0));

        // Make sure not to create blocks that are out of bounds
        if (OutOfBounds(gObj) == true)
        {
            // Destroy it
            Destroy(gObj);
            // Call spawn again
            Spawn();
        }
        else
        {
            // assign a random material to the brick
            Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
            foreach (var r in gObj.GetComponentsInChildren<Renderer>())
            {
                r.material = m;
            }
            // Attach applicable scripts to the blocks
            gObj.AddComponent<BlockObject>();
            // do not allow another block to drop until the current
            // one has hit something
            if (breakable == true)
            {
                // Blocks are set to breakable, thus the BrickCollision script
                // must be added to the child cubes of the lego piece
                int childCount = 0;
                foreach (var child in gObj.GetComponentsInChildren<BoxCollider>())
                {
                    // Attach necessary scripts
                    child.gameObject.AddComponent<BlockCollision>();
                    child.gameObject.AddComponent<BlockMovement>();
                    child.GetComponent<BoxCollider>().gameObject.tag = "block_tag";
                    // Assign child_tag to every child cube except the first one
                    if (childCount > 0)
                    {
                        child.GetComponent<BoxCollider>().gameObject.tag = "child_tag";
                    }
                    childCount++;
                }
            }
            else
            {
                // Blocks are not set to break, thus
                // Attach necessary scripts to this object
                gObj.AddComponent<BlockMovement>();
                gObj.AddComponent<BlockCollision>();
                gObj.tag = "block_tag";
            }
        }
        #endregion
    }

    /** Determines if a block's position is out of bounds
     *  @param GameObject representing a Lego block
     *  @return boolean value, if false block not out of bounds; otherwise,
     *  block is out of bounds
    */
    public bool OutOfBounds(GameObject gObj)
    {
        #region Check boundary
        // check every child
        bool outBounds = false;
        foreach (Transform child in gObj.transform)
        {
            // Get the child cubes
            if (child.GetComponent<BoxCollider>())
            {
                if (child.position.x < boundaryBegin.x || child.position.x > boundaryEnd.x)
                {
                    // Child cube is out of bounds
                    outBounds = true;
                }
                else if (child.position.z < boundaryBegin.z || child.position.z > boundaryEnd.z)
                {
                    // Child cube is out of bounds
                    outBounds = true;
                }
            }
        }
        if (outBounds == true)
        {
            // At least one child cube is out of bounds
            return true;
        }
        else
        {
            // No child cubes were out of bounds
            return false;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if player has hit Q for Quit
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Quit game
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        if (simMode == true)
        {
            // activate simulation mode
            SimulationMode();
        }
    }
}
