using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    
    public Text deathCounter;   // the UI death counter
    public Text youWin;         // the win screen
    public Text winMessage;     // the win message

    /*
    // Start is called before the first frame update
    void Start(){}
    // Update is called once per frame
    void Update(){}
    */

    // DisplayDeath shows the number of times the player died before finishing the level
    public void DisplayDeath(int deathNum)
    {
        // set the text of the death counter with updated number of deaths
        deathCounter.text = "Deaths: " + deathNum.ToString();
    }

    // DisplayWinScreen pops up when the player finishes the level
    public void DisplayWinScreen(int deathNum)
    {
        /* display different messages if:
            - the player didn't die at all
            - the player only dies once
            - the player dies multiple times
        */
        if (deathNum == 0)
        {
            youWin.text = "Perfect!";
            winMessage.text = "You didn't die even once!";
        }
        else
        {
            youWin.text = "You Win!";
            
            if (deathNum == 1)
                winMessage.text = "And you only died 1 time!";
            else
                winMessage.text = "And you only died " + deathNum.ToString() + " times!";
        }

        // set the display to be active/visible
        youWin.gameObject.SetActive(true);
        winMessage.gameObject.SetActive(true);
    }
}