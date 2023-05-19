using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class TextSerializer : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textObjects;
    [SerializeField] private TextMeshPro newTMProText;

    private string[] serializedTextArray;

    private void Start()
    {
        serializedTextArray = new string[textObjects.Length];
    }

    public override void OnDeserialization()
    {
        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null)
            {
                serializedTextArray[i] = textObjects[i].text;
            }
        }
    }

    private void Update()
    {
        bool textChanged = false;

        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null && textObjects[i].text != serializedTextArray[i])
            {
                serializedTextArray[i] = textObjects[i].text;
                Debug.Log("Text #" + (i + 1) + " changed to: " + serializedTextArray[i]);
                textChanged = true;
            }
        }

        if (textChanged && newTMProText != null)
        {
            newTMProText.text = string.Join("\n", serializedTextArray);
        }
    }
}
