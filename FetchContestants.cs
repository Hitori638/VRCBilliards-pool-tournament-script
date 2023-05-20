using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class FetchContestants : UdonSharpBehaviour
{
    [SerializeField] public TextMeshProUGUI[] ContestantDetection;
    [SerializeField] public TextMeshPro[] Round1; // Changed the variable to an array

    public bool playerPoolFull = false; // Flag to track if player pool is full

    
    public override void Interact(){
        if (Networking.IsMaster){
        SendCustomNetworkEvent(NetworkEventTarget.All, "GamemodeSelect");
    }
    }


    public void GamemodeSelect(){
        if (playerPoolFull)
        {
            // Player pool is already full, no need to check further
            return;
        }

        bool textChanged = false;

        for (int i = 0; i < ContestantDetection.Length; i++)
        {
            Debug.Log("Loop iteration: " + i);
            if (ContestantDetection[i] != null && ContestantDetection[i].text != Round1[i].text && !string.IsNullOrEmpty(ContestantDetection[i].text))
            {
                if (!IsPlayerInRound1(ContestantDetection[i].text))
                {
                    Round1[i].text = ContestantDetection[i].text;
                    Debug.Log("Text #" + (i + 1) + " changed to: " + Round1[i].text);
                    textChanged = true;

                    if (IsPlayerPoolFull())
                    {
                        // Player pool is full, stop searching for more players
                        playerPoolFull = true;
                        break;
                    }
                }
            }
        }

        if (textChanged)
        {
            Debug.Log("Round1 updated.");
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

    private bool IsPlayerPoolFull()
    {
        foreach (var player in Round1)
        {
            if (string.IsNullOrEmpty(player.text))
            {
                return false; // Player pool is not full
            }
        }
        return true; // Player pool is full
    }

    public void ClearContestants()
    {
        for (int i = 0; i < Round1.Length; i++)
        {
            Round1[i].text = ""; // Clear Round1 text
        }
        playerPoolFull = false; // Reset player pool full flag
        Debug.Log("Round1 cleared.");
    }
}
