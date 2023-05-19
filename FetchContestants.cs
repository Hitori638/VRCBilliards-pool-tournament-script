using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class FetchContestants : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] ContestantDetection;
    [SerializeField] public TextMeshPro[] Round1; // Changed the variable to an array

    public string[] Contestants;

    private void Start()
    {
        Contestants = new string[ContestantDetection.Length];
    }

    public override void OnDeserialization()
    {
        for (int i = 0; i < ContestantDetection.Length; i++)
        {
            if (ContestantDetection[i] != null && !string.IsNullOrEmpty(ContestantDetection[i].text))
            {
                Contestants[i] = ContestantDetection[i].text;
            }
        }
    }

    private void Update()
    {
        bool textChanged = false;

        for (int i = 0; i < ContestantDetection.Length; i++)
        {
            if (ContestantDetection[i] != null && ContestantDetection[i].text != Contestants[i] && !string.IsNullOrEmpty(ContestantDetection[i].text))
            {
                Contestants[i] = ContestantDetection[i].text;
                Debug.Log("Text #" + (i + 1) + " changed to: " + Contestants[i]);
                textChanged = true;
            }
        }

        if (textChanged && Round1 != null && Round1.Length == Contestants.Length) // Added array length check
        {
            for (int i = 0; i < Contestants.Length; i++) // Loop through Contestants array
            {
                Round1[i].text = Contestants[i]; // Assign contestant names to corresponding newTMProText objects
            }
        }
    }

    public void ClearContestants()
    {
        Contestants = new string[ContestantDetection.Length];
        Debug.Log("Contestants array cleared.");
    }
}
