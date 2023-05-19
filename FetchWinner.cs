using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class FetchWinner : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textObjects;
    [SerializeField] private TextMeshPro newTMProText;

    private string[] Winners;


private void Start()
{
    Winners = new string[textObjects.Length];
}


private void Update()
{
    bool textChanged = false;

    for (int i = 0; i < textObjects.Length; i++)
    {
        if (textObjects[i] != null)
        {
            string currentText = textObjects[i].text;
            string winnerName = GetPlayerName(currentText);
            
            if (winnerName != Winners[i])
            {
                Winners[i] = winnerName;
                Debug.Log("Text #" + (i + 1) + " changed to: " + Winners[i]);
                textChanged = true;
            }
        }
    }

    if (textChanged && newTMProText != null)
    {
        newTMProText.text = string.Join("\n", Winners);
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