using UdonSharp;
using UnityEngine;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class ClearContestants : UdonSharpBehaviour
{
    public TextMeshPro[] textMeshArray;
    
    private void Interact()
    {
        if (Networking.IsMaster)
        {
           SendCustomNetworkEvent(NetworkEventTarget.All, "ClearContestant"); 
        }
    }
    
    public void ClearContestant()
    {
        foreach (TextMeshPro textMesh in textMeshArray)
        {
            textMesh.text = string.Empty;
        }
    }
}
