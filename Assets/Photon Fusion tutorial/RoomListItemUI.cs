using TMPro;
using UnityEngine;

public class RoomListItemUI : MonoBehaviour
{
    public TMP_Text roomInfoText;

    private string roomName;

    public void Setup(string name, int current, int max)
    {
        roomName = name;
        roomInfoText.text = $"{name} — {current} / {max}";
    }

    public void OnJoinClicked()
    {
        FusionLauncher.Instance.JoinRoom(roomName);
    }
}