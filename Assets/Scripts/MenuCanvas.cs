﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The Menu Canvas holds the menu options for the player
/// It also contains the music for the game
/// Handles "pausing/resuming" the game when the menu is enabled/disabled
/// Quits the application when requested
/// </summary>
public class MenuCanvas : MonoBehaviour
{
    /// <summary>
    /// A reference the main BG
    /// </summary>
    [SerializeField]
    GameObject mainBG;

    /// <summary>
    /// A reference the main BG
    /// </summary>
    [SerializeField]
    GameObject creditsBG;

    /// <summary>
    /// A reference the menu BG
    /// </summary>
    [SerializeField]
    GameObject Menu;

    /// <summary>
    /// A reference to the player
    /// </summary>
    Player player;

    /// <summary>
    /// Holds where the player started to send them right back on game restart
    /// </summary>
    Vector3 playerStartPos;

    /// <summary>
    /// A reference to the companion
    /// </summary>
    Companion companion;

    /// <summary>
    /// Holds where the companion started to send them right back on game restart
    /// </summary>
    Vector3 companionStartPos;

    /// <summary>
    /// Prevents the menu from opening and closing while the player 
    /// The menu starts opened
    /// </summary>
    public bool isMenuOpened = true;

    /// <summary>
    /// Only opens the menu when the button is pressed
    /// </summary>
    bool isButtonPressed = false;

    /// <summary>
    /// A reference to the virtual dpad controller
    /// </summary>
    VirtualDPadController dpad;
    
    /// <summary>
    /// Initialize
    /// </summary>
    void Start()
    {
        this.dpad = FindObjectOfType<VirtualDPadController>();
        this.player = FindObjectOfType<Player>();
        this.companion = FindObjectOfType<Companion>();

        this.playerStartPos = this.player.transform.position;
        this.companionStartPos = this.companion.transform.position;

        // Disable the player
        this.player.DisablePlayerControl();
    }

    /// <summary>
    /// Toggles between opening and closing the menu
    /// </summary>
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) || this.dpad.Input["Menu"] == 1) {

            // If player is moving ignore
            if(!this.player.CanOpenMenu()) {
                return;
            }

            // Open the menu
            if(!this.isButtonPressed && !this.isMenuOpened) {
                this.isMenuOpened = true;
                this.isButtonPressed = true;
                this.OpenMenu();
            }

            // Close the menu
            if(!this.isButtonPressed && this.isMenuOpened) {
                this.isButtonPressed = true;
                this.CloseMenu();
            }
        } else {
            this.isButtonPressed = false;
        }
    }

    /// <summary>
    /// Closes the menu
    /// Resumes the game
    /// </summary>
    public void Play()
    {
        Cursor.visible = false; 
        // End screen 
        if(this.creditsBG.activeSelf) {
            this.creditsBG.SetActive(false);
        }

        // Close the menu so the playe can play
        this.isMenuOpened = false;
        this.Menu.SetActive(false);
        this.player.EnablePlayerControl();
    }

    /// <summary>
    /// Shows the "thank you for playing"
    /// Sends the player right back to the start in case they want to play again
    /// </summary>
    public void OpenCreditsMenu()
    {
        Cursor.visible = true;
        this.isMenuOpened = true;
        this.Menu.SetActive(true);
        this.creditsBG.SetActive(true);
        this.player.DisablePlayerControl();
        this.player.UpdateCheckpoint(this.playerStartPos);
        this.companion.UpdateRespawnPoint(this.companionStartPos);
        this.companion.DrainAllPower();
        this.player.transform.position = this.playerStartPos;
        this.companion.transform.position = this.companionStartPos;
    }

    /// <summary>
    /// Menu is triggered opened
    /// </summary>
    public void OpenMenu()
    {
        Cursor.visible = true;
        this.Menu.SetActive(true);
        this.player.DisablePlayerControl();
    }

    /// <summary>
    /// When is closed
    /// </summary>
    public void CloseMenu()
    {
        // Can't close if it's the end credit
        if(this.creditsBG.activeSelf) {
            return;
        }

        this.isMenuOpened = false;
        this.Menu.SetActive(true);
        this.player.EnablePlayerControl();
    }

	// <summary>
    /// Closes the app
    /// </summary>
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
