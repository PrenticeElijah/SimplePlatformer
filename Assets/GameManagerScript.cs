using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    
    private int deaths = 0;         // number of times the player has died

    private bool levelComplete = false;     // whether or not the player has completed the level
    
    public float xGB, yGB;          // the x and y bounds of the game level
    public float xStart, yStart;    // the x and y starting position of the player
    public GameObject gamePlayer;   // the player character
    public GameObject goalCoin;     // the coin at the end of the level

    public GameObject soundManager;     // meant to handle the sound effects (SFX), child of GameManager
    public GameObject musicManager;     // meant to handle the game music, child of GameManager

    static AudioSource sfxSource;       // the AudioSource handling SFX
    static AudioSource musicSource;     // the AudioSource handling music

    static AudioClip playSFX;           // the SFX currently playing
    public AudioClip stageMusic;        // the current game music

    public UIScript gameUI;             // the game's UI Canvas
    public CameraController camCtrl;    // the game's Camera
    
    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = GameObject.FindWithTag("Player");              // find the Game Object tagged as "Player"

        sfxSource = soundManager.GetComponent<AudioSource>();       // find the AudioSource component of the Sound Manager

        musicSource = musicManager.GetComponent<AudioSource>();     // find the AudioSource component of the Music Manager
        musicSource.clip = stageMusic;                              // musicSource can now play the current music track
        musicSource.Play(0);                                        // play the track at the start of the game, set to loop

        xStart = gamePlayer.transform.position.x;                   // x-position of respawn point
        yStart = gamePlayer.transform.position.y;                   // y-position of respawn point
    }

    // Update is called once per frame
    void Update()
    {
        
        // close the game
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        // when the player finds the coin
        if (goalCoin == null)
        {
            // the player respawns where they completed the game if they fall
            xStart = gamePlayer.transform.position.x;   
            yStart = gamePlayer.transform.position.y;

            gameUI.DisplayWinScreen(deaths);    // display the win screen
            levelComplete = true;               // the player has completed the level
            camCtrl.canMove = false;            // the camera has no reason to move after the game is complete
        }
        
        // when the player falls off the map
        if (gamePlayer.transform.position.y < yGB)
            Respawn();  // respawn the player
    }

    // Respawn sets the player back wherever the respawn point is
    void Respawn()
    {
        // change the player's position
        gamePlayer.transform.position = new Vector3(xStart, yStart, gamePlayer.transform.position.z);

        // if the player has not completed the level
        if (!levelComplete)
        {
            deaths++;                       // increment the number of times the player has respawned before completing the level
            gameUI.DisplayDeath(deaths);    // change the number of deaths displayed
        }
    }

    // PlaySFX plays a desired SFX
    public static void PlaySFX(string sfx)
    {
        playSFX = Resources.Load<AudioClip>(sfx);   // load the AudioClip of the SFX
        sfxSource.PlayOneShot(playSFX);             // play the SFX once
    }
}