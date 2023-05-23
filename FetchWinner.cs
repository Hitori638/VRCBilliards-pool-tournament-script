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
    }

    public void Round2Logic()
    {
       
        if(!FirstUpdate){
            if (WinDetection.text != null)
            {
                string currentText = WinDetection.text;
                string winnerName = GetPlayerName(currentText);

                if (Round2[tableid] != null && winnerName != Round2[tableid].text)
                {
                    Round2[tableid].text = winnerName;
                    FirstUpdate= true;
                    
                }
            }
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
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Round2[j].text) && !string.IsNullOrEmpty(Round2[j - 1].text))
                            {
                                Round3[j / 2].text = Round2[j].text;
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
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Round3[j].text) && !string.IsNullOrEmpty(Round3[j - 1].text))
                            {
                                Round4[j / 2].text = Round3[j].text;
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
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Round4[j].text) && !string.IsNullOrEmpty(Round4[j - 1].text))
                            {
                               Winner.text = Round4[j].text;
                            }
                        }

                        break;
                    }
                }
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
