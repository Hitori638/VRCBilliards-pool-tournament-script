using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class FetchWinner : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] WinDetection;
    [SerializeField] private TextMeshPro[] Round2;
    [SerializeField] private TextMeshPro[] Round3;
    [SerializeField] private TextMeshPro[] Round4;
    [SerializeField] private TextMeshPro[] Winner;

    private void Update()
    {
        for (int i = 0; i < WinDetection.Length; i++)
        {
            if (WinDetection[i] != null)
            {
                string currentText = WinDetection[i].text;
                string winnerName = GetPlayerName(currentText);

                if (Round2[i] != null && winnerName != Round2[i].text)
                {
                    Round2[i].text = winnerName;
                    Debug.Log("Text #" + (i + 1) + " changed to: " + winnerName);
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