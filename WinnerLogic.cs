using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class WinnerLogic : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] winDetection;
    [SerializeField] private TextMeshPro winner1;
    [SerializeField] private TextMeshPro winner2;
    [SerializeField] private TextMeshPro winnerText;


   private void Update()
{
    if(!string.IsNullOrEmpty(winner1.text) && !string.IsNullOrEmpty(winner2.text)) {
    bool winnerFound = false;

    for (int i = 0; i < winDetection.Length && !winnerFound; i++)
    {
        if (winDetection[i] != null && !string.IsNullOrEmpty(winDetection[i].text))
        {
            string currentText = winDetection[i].text;
            string winnerName = GetPlayerName(currentText);

            if (string.IsNullOrEmpty(winnerText.text))
            {
                if (winnerName == winner1.text)
                {
                    Debug.Log("WINNER!");
                    winnerText.text = winner1.text;
                }
                else
                {
                    winnerText.text = winner2.text;
                    Debug.Log("WINNER!");
                }

                winnerFound = true; // Set the flag to true to stop looping
            }

            }
        }
    }
}


       private string GetPlayerName(string fullText)
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
   

