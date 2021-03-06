﻿using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;

public class OptionController : MonoBehaviour 
{
    //*************
    // This controller will focus on displaying and saving options.
    // Attributes here will be public so any method can have access to them when they are needed, for instance, Sound Volumes.
    // Saving or changing options will be split into two ways, isGameRunning true or false;
    //*************
    /// <summary>
    /// Returns true if GameController.instance.gameStatus != running
    /// </summary>
    public bool IsGameRunning
    {
        get
        {
            if (GameController.instance.GameStatus != "running") return true;
            else return false;
        }
    }
    public static OptionController instance = null;
    // GameObjects that will interact with options
    public GameObject usernameObj;
    public GameObject masterVolumeObj;
    public GameObject fullscreenObj;
    
    // Default values
    private string username = "";
    private float masterVolume = 1f;
    private bool fullscreen = true;
    //

    /// <summary>
    /// Set/Get player username value
    /// </summary>
    public string Username
    {
        set
        {
            PlayerPrefs.SetString("Username", value);
        }
        get
        {
            return PlayerPrefs.GetString("Username", username);
        }

    }
    /// <summary>
    /// Set/Get this value controlls AudioListener.volume 
    /// </summary>
    public float MasterVolume
    {
        set 
        {
            PlayerPrefs.SetFloat("MasterVolume", value);
            AudioListener.volume = value;
        }
        get 
        {
            return PlayerPrefs.GetFloat("MasterVolume", masterVolume);
        }
    }
    /// <summary>
    /// Set/Get this value controlls Screen.fullscreen
    /// </summary>
    public bool Fullscreen
    {
        set
        {
            //PlayerPrefs.SetInt("Fullscreen", value);
            if (value)
            {
                PlayerPrefs.SetInt("Fullscreen", 1);
                //MouseCursorController.instance.changeMouseCursor();
            }
            else PlayerPrefs.SetInt("Fullscreen", 0);
            Screen.fullScreen = value;
        }
        get
        {
            return Screen.fullScreen;
        }
    }
    //
    void Awake()
    {
        // Makes the current instance a static one.
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        //
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        // Load default settings such as fullscreen and resolution if needed
        if (PlayerPrefs.GetInt("Fullscreen") == 1) Fullscreen = true;
        else Fullscreen = false;
    }
    // Loading options for display purpose. They will be displayed in the extraOptions -> OptionsWrap window
    public void displayOptions()
    {
        // Change status to options that are sensitive whether the game is running or not
        this.changeSensitiveOptions();
        // Load
        masterVolumeObj.GetComponent<Slider>().value = MasterVolume;
        usernameObj.GetComponent<InputField>().text = Username;
        fullscreenObj.GetComponent<Toggle>().isOn = Fullscreen;
    }
    private void changeSensitiveOptions()
    {
        usernameObj.GetComponent<InputField>().interactable = IsGameRunning;
    }
    // Main method for saving all options // lazy way out :D
    public void saveOptions()
    {
        // Saving Master Volume sound
        if (MasterVolume != masterVolumeObj.GetComponent<Slider>().value)
        {
            MasterVolume = masterVolumeObj.GetComponent<Slider>().value;
        }
        // Saving Username
        if (Username != usernameObj.GetComponent<InputField>().text)
        {
            Username = usernameObj.GetComponent<InputField>().text;
        }
        // Saving fullscreen
        if (Fullscreen != fullscreenObj.GetComponent<Toggle>().isOn)
        {
            Fullscreen = fullscreenObj.GetComponent<Toggle>().isOn;
        }
        // After all options have been set in OptionController and PlayerPrefs
        GameController.instance.extraRoom_closeOptions();
    }
    //
}
