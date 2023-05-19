using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class FetchWinner : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textObjects;
    [SerializeField] private TextMeshPro[] newTMProTextArray;

    private void Update()
    {
        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null)
            {
                string currentText = textObjects[i].text;
                string winnerName = GetPlayerName(currentText);

                if (newTMProTextArray[i] != null && winnerName != newTMProTextArray[i].text)
                {
                    newTMProTextArray[i].text = winnerName;
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
