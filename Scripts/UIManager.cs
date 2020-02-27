/**	
 *  Project 1 - Lego Blocks
 *  UIManager.cs
 *  Purpose: Handles the result of interaction with
 *  the User Interface elements.
 *  
 *  @author Leslie Hoyt
 *  10/8/19
 *  COMP 465
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MySingleton<UIManager>
{
    public RectTransform panelMainMenu;
    public RectTransform panelSetting;
    public RectTransform panelMenu;
    public InputField impMDim;
    public InputField impNDim;
    public Toggle togSim;
    public Toggle togGame;
    public Slider sldDifficulty;
    public Toggle togBreak;

    /** Loads Lego scene, after deactivating main menu
     *  and activating menu button,
     *  Executes when Start button is clicked
     *  @param none
     *  @return none
    */
    public void butStartClicked()
    {
        Debug.Log("Button Start clicked");
        // Turn off visibility of main menu
        panelMainMenu.transform.gameObject.SetActive(false);
        panelMenu.transform.gameObject.SetActive(true);
        SceneManager.LoadScene("LegoScene");
    }

    /** Activates Setup Menu,
     *  Executes when Setup button is clicked
     *  @param none
     *  @return none
    */
    public void butSetupClicked()
    {
        Debug.Log("Button Setup clicked");
        panelMainMenu.transform.gameObject.SetActive(false);
        panelSetting.transform.gameObject.SetActive(true);
    }

    /** Quits program,
     *  Executes when Exit button is clicked
     *  @param none
     *  @return none
    */
    public void butExitClicked()
    {
        Debug.Log("Button Exit clicked");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    /** Sets values in PlayerPrefs,
     *  Executes when OK button in Setup menu is pressed
     *  @param none
     *  @return none
    */
    public void butSetupOkClicked()
    {
        // Board Size
        int m = Convert.ToInt32(impMDim.text);
        int n = Convert.ToInt32(impNDim.text);

        PlayerPrefs.SetString("M", m.ToString());
        PlayerPrefs.SetString("N", n.ToString());

        Debug.Log(string.Format("The new dimension of board will be {0}x{1}", m, n));

        // Game Mode
        bool simMode = Convert.ToBoolean(togSim.isOn);
        bool gameMode = Convert.ToBoolean(togGame.isOn);

        PlayerPrefs.SetString("SimMode", simMode.ToString());
        PlayerPrefs.SetString("GameMode", gameMode.ToString());

        // Difficulty Level
        float difficulty = sldDifficulty.value;

        PlayerPrefs.SetString("Difficulty", difficulty.ToString());

        // Breakable
        bool breakable = Convert.ToBoolean(togBreak.isOn);

        PlayerPrefs.SetString("Breakable", breakable.ToString());

        // Change visibility of panels
        panelSetting.transform.gameObject.SetActive(false);
        panelMainMenu.transform.gameObject.SetActive(true);
    }

    /** Toggles activation of Main Menu,
     *  Executes when Menu button is clicked
     *  @param none
     *  @return none
    */
    public void butMenuClicked()
    {
        // Change visibility of panels
        // If main menu is open, close it
        if (panelMainMenu.transform.gameObject.activeSelf)
        {
            panelMainMenu.transform.gameObject.SetActive(false);
        }
        // If setup menu is open, close it
        else if (panelSetting.transform.gameObject.activeSelf)
        {
            panelSetting.transform.gameObject.SetActive(false);
        }
        else
        {
            // Only open main menu if setup menu is not open
            if (!panelSetting.transform.gameObject.activeSelf)
            {
                panelMainMenu.transform.gameObject.SetActive(true);
            }
        }
    }
}
