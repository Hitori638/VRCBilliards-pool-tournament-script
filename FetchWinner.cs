using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class FetchWinner : UdonSharpBehaviour
{
    [SerializeField] public TextMeshProUGUI[] Tables;
    [SerializeField] public TextMeshProUGUI WinDetection;
    [SerializeField] public TextMeshPro[] Round2;
    [SerializeField] public TextMeshPro[] Round3;
    [SerializeField] public TextMeshPro[] Round4;
    [SerializeField] public TextMeshPro Winner;
    [SerializeField] private int tableid;
   

    private bool FirstUpdate = false;


    public void OnWinnerUpdate()
    {
        Debug.Log("I have been called!");
        DetermineWinner();
        Round4Logic();
        Round3Logic();
        Round2Logic();
        TournamentConcluded();
        
    }

    public void Round2Logic()
{
    if (!FirstUpdate)
    {
        bool eightplayers = EightplayerGamemode();
        if (eightplayers)
        {
            FirstUpdate = true;
            Debug.Log("detected 8playermode");
        }

        if (!FirstUpdate && WinDetection.text != null)
        {
            string currentText = WinDetection.text;
            string winnerName = GetPlayerName(currentText);

            bool isAlreadyInRound2 = false;
            
            for (int i = 0; i < Round2.Length; i++)
            {
                if (Round2[i].text == winnerName)
                {
                    isAlreadyInRound2 = true;
                    break;
                }
            }

            if (!isAlreadyInRound2 && winnerName != Round2[tableid].text)
            {
                Round2[tableid].text = winnerName;
                WinDetection.text="";
                FirstUpdate = true;
            }
        }
    }
}


    public bool EightplayerGamemode(){
        int count = 0;
        for (int i=0;i<8;i++){
            if (!string.IsNullOrEmpty(Round2[i].text))
            count++;
        

        }
        if (count==8){
            return true;
        }
        else{
            return false;
        }
    }
    public void Round3Logic()
    {
       
            for (int i = 0; i < Tables.Length; i++)
            {
                for (int j = 0; j < Round2.Length; j++)
                {
                    if (GetPlayerName(Tables[i].text) == Round2[j].text)
                    {

                        if (j % 2 == 0)
                        {   
                           
                            if (!string.IsNullOrEmpty(Round2[j].text) && !string.IsNullOrEmpty(Round2[j + 1].text))
                            {
                                
                                Round3[j / 2].text = Round2[j].text;
                                Tables[i].text= "";
                            }
                        }
                        else
                        {
                            
                            if (!string.IsNullOrEmpty(Round2[j].text) && !string.IsNullOrEmpty(Round2[j -1].text))
                            {
                                Round3[j / 2].text = Round2[j].text;
                                Tables[i].text= "";
                            }
                        }

                        break;
                    }
                }
            }

            
        }
    

    public void Round4Logic()
    {
        
        
            for (int i = 0; i < Tables.Length; i++)
            {
                for (int j = 0; j < Round3.Length; j++)
                {
                    if (GetPlayerName(Tables[i].text) == Round3[j].text)
                    {
                        if (j % 2 == 0)
                        {
                            if (!string.IsNullOrEmpty(Round3[j].text) && !string.IsNullOrEmpty(Round3[j + 1].text))
                            {
                                Round4[j / 2].text = Round3[j].text;
                                Tables[i].text= "";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Round3[j].text) && !string.IsNullOrEmpty(Round3[j - 1].text))
                            {
                                Round4[j / 2].text = Round3[j].text;
                                Tables[i].text= "";
                            }
                        }

                        break;
                    }
                }
            }

        
    }

    public void DetermineWinner()
    {
         for (int i = 0; i < Tables.Length; i++)
            {
                for (int j = 0; j < Round4.Length; j++)
                {
                    if (GetPlayerName(Tables[i].text) == Round4[j].text)
                    {
                        if (j % 2 == 0)
                        {
                            if (!string.IsNullOrEmpty(Round4[j].text) && !string.IsNullOrEmpty(Round4[j + 1].text))
                            {
                                Winner.text = Round4[j].text;
                                Tables[i].text= "";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Round4[j].text) && !string.IsNullOrEmpty(Round4[j - 1].text))
                            {
                               Winner.text = Round4[j].text;
                               Tables[i].text= "";
                            }
                        }

                        break;
                    }
                }
            }
    }

    public void TournamentConcluded(){
        if (!string.IsNullOrEmpty(Winner.text)){
        FirstUpdate = false;

        }
    }

    public string GetPlayerName(string fullText)
    {
        int suffixIndex = fullText.LastIndexOf(" wins!");
        if (suffixIndex >= 0)
        {
            return fullText.Substring(0, suffixIndex);
        }
        else
        {
            return fullText; // If the suffix is not found, return the full text.
        }
    }
}
