/**	
 *  Project 1 - Lego Blocks
 *  BlockCollision.cs
 *  Purpose: Handles the collision detection of a Lego block
 *  and activates/deactivates the ability to move the block
 *  
 *  @author Leslie Hoyt
 *  10/8/19
 *  COMP 465
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockCollision : MonoBehaviour
{
    public bool allowMovement = true;
    bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        allowMovement = true;
    }

    /** Triggers a Spawn event if collision between
     *  two blocks (or the platform) occurs
     *  @param Collision object representing the colliding object
     *  @return none
    */
    private void OnCollisionEnter(Collision collision)
    {
        // Prevent the movement of this block
        if (collision.gameObject.tag != "block_tag" && collision.gameObject.tag != "child_tag")
        {
            if (isColliding == true)
            {
                return;
            }
            else
            {
                isColliding = true;
                if (gameObject.tag == "block_tag")
                {
                    gameObject.tag = "collision_tag";
                    allowMovement = false;
                    EventManager.TriggerEvent("Spawn");
                }
                else if (gameObject.tag == "child_tag")
                {
                    gameObject.tag = "collision_tag";
                    allowMovement = false;
                }
                else
                {
                    // do nothing
                }
            }
        }
    }
}
