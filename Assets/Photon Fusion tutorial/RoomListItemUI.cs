using UnityEngine;
using TMPro;
using Fusion;

public class RoomListItemUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private string _roomName;

    public void Setup(SessionInfo session)
    {
        _roomName = session.Name;
        infoText.text = $"{session.Name} ({session.PlayerCount}/{session.MaxPlayers})";
    }

    public void OnClick()
    {
        // Find the UI controller in the scene and tell it to join this room
        FindObjectOfType<MenuUIController>().JoinSelectedRoom(_roomName);
    }
}