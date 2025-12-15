using UnityEngine;
using TMPro;

public class WaitingPlayerItem : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;

    public void Setup(string name)
    {
        playerNameText.text = name;
    }
}