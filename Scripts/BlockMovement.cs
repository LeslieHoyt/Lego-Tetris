/**	
 *  Project 1 - Lego Blocks
 *  BlockMovement.cs
 *  Purpose: Moves/rotates the Lego block
 *  
 *  @author Leslie Hoyt
 *  10/8/19
 *  COMP 465
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    float movementIncrement = 0.08f;
    float degrees = 0;
    Vector3 currentRotation;
    Vector3 currentPosition;
    Vector3 newPosition;
    bool breakBlock;
    bool move;

    // Start is called before the first frame update
    void Start()
    {
        breakBlock = GameObject.Find("_GameManager").GetComponent<GameMaster>().breakable;
    }   

    // Update is called once per frame
    void Update()
    {
        // Set move
        move = this.gameObject.GetComponent<BlockCollision>().allowMovement;
        #region React to user input for movement
        // Rotate brick
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }
        // Move brick forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveForward();
        }
        // Move brick back
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveBack();
        }
        // Move brick right
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        // Move brick left
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        #endregion
    }

    /** Rotates a Lego block
     *  @param none
     *  @return none
    */
    void Rotate()
    {
        #region Rotate block logic
        if (move)
        {
            // Constrains rotation to degrees 0, 90, 180, 360
            //if (degrees == 0 || degrees == 90)
            if (degrees != 360)
            {
                degrees += 90;
            }
            //else if (degrees == 180)
            //{
            //    degrees = 360;
            //}
            else
            {
                // reset degrees, since degrees is 360
                degrees = 0;
            }
            if (breakBlock)
            {
                currentRotation = this.transform.parent.eulerAngles;
                this.transform.parent.eulerAngles = new Vector3(0, degrees, 0);
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.transform.parent.gameObject) == true)
                {
                    // reset the rotation
                    this.transform.parent.eulerAngles = currentRotation;
                }
            }
            else
            {
                currentRotation = transform.eulerAngles;
                transform.eulerAngles = new Vector3(0, degrees, 0);
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.gameObject) == true)
                {
                    // reset the rotation
                    transform.eulerAngles = currentRotation;
                }
            }
        }
        #endregion
    }

    /** Moves a Lego block forward
     *  @param none
     *  @return none
    */
    void MoveForward()
    {
        #region Move block forward
        if (move)
        {
            if (breakBlock)
            {
                currentPosition = this.transform.parent.position;
                newPosition = new Vector3(this.transform.parent.position.x, 
                    this.transform.parent.position.y, (this.transform.parent.position.z + movementIncrement));
                this.transform.parent.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.transform.parent.gameObject) == true)
                {
                    // reset the move
                    this.transform.parent.position = currentPosition;
                }

            }
            else
            {
                currentPosition = transform.position;
                newPosition = new Vector3(transform.position.x, 
                    transform.position.y, (transform.position.z + movementIncrement));
                transform.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.gameObject) == true)
                {
                    // reset the move
                    transform.position = currentPosition;
                }
            }
        }
        #endregion
    }

    /** Moves a Lego block backward
     *  @param none
     *  @return none
    */
    void MoveBack()
    {
        #region Move block backwards
        if (move)
        {
            if (breakBlock)
            {
                currentPosition = this.transform.parent.position;
                newPosition = new Vector3(this.transform.parent.position.x, 
                    this.transform.parent.position.y, (this.transform.parent.position.z - movementIncrement));
                this.transform.parent.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.transform.parent.gameObject) == true)
                {
                    // reset the movement
                    this.transform.parent.position = currentPosition;
                }
            }
            else
            {
                currentPosition = transform.position;
                newPosition = new Vector3(transform.position.x, 
                    transform.position.y, (transform.position.z - movementIncrement));
                transform.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.gameObject) == true)
                {
                    // reset the movement
                    transform.position = currentPosition;
                }
            }
        }
        #endregion
    }

    /** Moves a Lego block to the left
     *  @param none
     *  @return none
    */
    void MoveLeft()
    {
        #region Move block to the left
        if (move)
        {
            if (breakBlock)
            {
                currentPosition = this.transform.parent.position;
                newPosition = new Vector3((this.transform.parent.position.x - movementIncrement), 
                    this.transform.parent.position.y, this.transform.parent.position.z);
                this.transform.parent.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.transform.parent.gameObject) == true)
                {
                    // reset the movement
                    this.transform.parent.position = currentPosition;
                }
            }
            else
            {
                currentPosition = transform.position;
                newPosition = new Vector3((transform.position.x - movementIncrement), 
                    transform.position.y, transform.position.z);
                transform.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.gameObject) == true)
                {
                    // reset the movement
                    transform.position = currentPosition;
                }
            }
        }
        #endregion
    }

    /** Moves a Lego block to the right
     *  @param none
     *  @return none
    */
    void MoveRight()
    {
        #region Move block to the right
        if (move)
        {
            if (breakBlock)
            {
                currentPosition = this.transform.parent.position;
                newPosition = new Vector3((this.transform.parent.position.x + movementIncrement), 
                    this.transform.parent.position.y, this.transform.parent.position.z);
                this.transform.parent.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.transform.parent.gameObject) == true)
                {
                    // reset the movement
                    this.transform.parent.position = currentPosition;
                }
            }
            else
            {
                currentPosition = transform.position;
                newPosition = new Vector3((transform.position.x + movementIncrement), 
                    transform.position.y, transform.position.z);
                transform.position = newPosition;
                // Check to make sure the new position is within bounds
                if (GameObject.Find("_GameManager").GetComponent<GameMaster>().OutOfBounds(this.gameObject) == true)
                {
                    // reset the movement
                    transform.position = currentPosition;
                }
            }
        }
        #endregion
    }
}
