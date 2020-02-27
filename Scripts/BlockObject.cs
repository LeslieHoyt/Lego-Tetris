/**	
 *  Project 1 - Lego Blocks
 *  BlockObject.cs
 *  Purpose: Attaches the Rigidbodies to either the
 *  parent of the Lego block or the child cubes of the
 *  Lego block. Applies the difficulty setting.
 *  
 *  @author Leslie Hoyt
 *  10/8/19
 *  COMP 465
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    bool breakable;
    float difficulty;

    // Start is called before the first frame update
    void Start()
    {
        #region Get player preferences
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficulty = (float)Convert.ToDouble(PlayerPrefs.GetString("Difficulty"));
        }
        else
        {
            difficulty = 0; // assume hard mode
        }
        // Determine if blocks should break upon impact or not
        breakable = GameObject.Find("_GameManager").GetComponent<GameMaster>().breakable;
        #endregion
        #region Assign Rigidbody
        if (breakable == true)
        {
            // Allow breaking (by putting rigidbodies on the child cubes)
            foreach (Transform child in this.gameObject.transform)
            {
                if (child.GetComponent<BoxCollider>())
                {
                    // Attach rigidbody
                    child.GetComponent<BoxCollider>().gameObject.AddComponent<Rigidbody>();
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().useGravity = true;
                    child.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                    child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation |
                                                                    RigidbodyConstraints.FreezePositionX |
                                                                    RigidbodyConstraints.FreezePositionZ;
                    // apply difficulty level by adjusting drag
                    child.GetComponent<Rigidbody>().drag = difficulty;
                }
            }
        }
        else
        {
            // Do not allow breaking (by putting rigidbody only on parent)
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation |
                                                                    RigidbodyConstraints.FreezePositionX |
                                                                    RigidbodyConstraints.FreezePositionZ;
            // apply difficulty level by adjusting drag
            this.gameObject.GetComponent<Rigidbody>().drag = difficulty;
        }
        #endregion
    }
}
