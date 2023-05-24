using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class FetchContestants : UdonSharpBehaviour
{
    [SerializeField] public TextMeshProUGUI[] Contestants;
    [SerializeField] public TextMeshPro[] Round1;  // Changed the variable to an array
    [SerializeField] public TextMeshPro[] Eights;

    [SerializeField] public TextMeshPro Logger;




    

    private bool isInstanceCreator = false; // Flag to track if the player is the instance creator

    private void Start()
    {
        // Check if the local player is the instance creator
        if (Networking.LocalPlayer.isMaster)
        {
            isInstanceCreator = true;
        }
    }

    public override void Interact()
    {
        // Only allow the instance creator to press the buttons
        if (isInstanceCreator && Networking.IsMaster)
        {
            SendCustomNetworkEvent(NetworkEventTarget.All, "GamemodeSelect");
        }
    }

    
    public void GamemodeSelect()
{
    bool textChanged = false;

    for (int i = 0; i < Contestants.Length; i++)
    {
        Debug.Log("Loop iteration: " + i);
        if (Contestants[i].text != Round1[i].text && !string.IsNullOrEmpty(Contestants[i].text))
        {
            if (!IsPlayerInRound1(Contestants[i].text))
            {
                Round1[i].text = Contestants[i].text;
                Debug.Log("Text #" + (i + 1) + " changed to: " + Round1[i].text);
                textChanged = true;
            }
        }
    }

    if (textChanged)
    {
        Debug.Log("Round1 updated.");
         string gamemode = GamemodeDetection();

        if (gamemode == "8 player mode initialized")
        {
            CopyRound1ToRound2();
            Debug.Log(gamemode);
            Logger.text=gamemode;
        }
        else{
            Debug.Log(gamemode);
            Logger.text=gamemode;
        }
    }
}

  public string GamemodeDetection()
{
    int count = 0;
    for (int i = 0; i < 16; i++)
    {
        if (!string.IsNullOrEmpty(Round1[i].text))
            count++;
    }

    if (count == 8)
    {
        return "8 player mode initialized";
    }
    else if (count < 8)
    {
        return "Not enough players. You need at least " + (8 - count) + " more players.";
    }
    else if (count > 16)
    {
        return "You have too many players. Exceeding the player count by " + (count - 16) + ".";
    }
    else if (count < 16)
    {
        return "Too many players for 8-player mode. You need " + (16 - count) + " more players for 16-player mode.";
    }
    else
    {
        return "16 player mode initialized.";
    }
}


private void CopyRound1ToRound2()
{
    int eightsIndex = 0;

    for (int i = 0; i < Round1.Length; i++)
    {
        if (!string.IsNullOrEmpty(Round1[i].text))
        {
            Eights[eightsIndex].text = Round1[i].text;
            Round1[i].text = ""; // Clear the Round1 entry
            eightsIndex++;
        }
    }
}



    private bool IsPlayerInRound1(string playerName)
    {
        for (int i = 0; i < Round1.Length; i++)
        {
            if (Round1[i] != null && Round1[i].text == playerName)
            {
                return true; // Player already exists in Round1
            }
        }
        return false; // Player does not exist in Round1
    }


   

 
}
