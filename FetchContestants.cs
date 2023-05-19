using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class FetchContestants : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textObjects;
    [SerializeField] public TextMeshPro[] newTMProTextArray; // Changed the variable to an array

    public string[] Contestants;

    private void Start()
    {
        Contestants = new string[textObjects.Length];
    }

    public override void OnDeserialization()
    {
        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null && !string.IsNullOrEmpty(textObjects[i].text))
            {
                Contestants[i] = textObjects[i].text;
            }
        }
    }

    private void Update()
    {
        bool textChanged = false;

        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null && textObjects[i].text != Contestants[i] && !string.IsNullOrEmpty(textObjects[i].text))
            {
                Contestants[i] = textObjects[i].text;
                Debug.Log("Text #" + (i + 1) + " changed to: " + Contestants[i]);
                textChanged = true;
            }
        }

        if (textChanged && newTMProTextArray != null && newTMProTextArray.Length == Contestants.Length) // Added array length check
        {
            for (int i = 0; i < Contestants.Length; i++) // Loop through Contestants array
            {
                newTMProTextArray[i].text = Contestants[i]; // Assign contestant names to corresponding newTMProText objects
            }
        }
    }

    public void ClearContestants()
    {
        Contestants = new string[textObjects.Length];
        Debug.Log("Contestants array cleared.");
    }
}
