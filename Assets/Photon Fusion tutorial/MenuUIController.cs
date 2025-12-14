using UnityEngine;
using TMPro;
using Fusion;
using System.Collections.Generic;

public class MenuUIController : MonoBehaviour
{
    [Header("Main Panels")]
    public GameObject mainMenuPanel;

    [Header("Popup Root")]
    public GameObject popupRoot;

    [Header("Popups")]
    public GameObject createRoomPopup;
    public GameObject joinRoomPopup;
    
    public TMP_InputField roomNameInput;
    
    [Header("Join Room UI")]
    public Transform roomListContent;
    public GameObject roomListItemPrefab;

    void Start()
    {
        CloseAllPopups();
    }

    // ---------- MAIN MENU BUTTONS ----------

    public void OnCreateRoomButtonClicked()
    {
        OpenCreateRoomPopup();
    }

    public void OnJoinRoomButtonClicked()
    {
        OpenJoinRoomPopup();
    }

    // ---------- POPUP CONTROL ----------

    void OpenCreateRoomPopup()
    {
        popupRoot.SetActive(true);
        createRoomPopup.SetActive(true);
        joinRoomPopup.SetActive(false);
    }

    void OpenJoinRoomPopup()
    {
        popupRoot.SetActive(true);
        createRoomPopup.SetActive(false);
        joinRoomPopup.SetActive(true);
    }

    public void CloseAllPopups()
    {
        popupRoot.SetActive(false);
        createRoomPopup.SetActive(false);
        joinRoomPopup.SetActive(false);
    }
    
    public void OnCreateRoomConfirmClicked()
    {
        string roomName = roomNameInput.text;

        if (string.IsNullOrEmpty(roomName))
            return;

        FusionLauncher.Instance.CreateRoom(roomName);
    }
    
    public void UpdateRoomList(List<SessionInfo> sessions)
    {
        // Clear old list
        foreach (Transform child in roomListContent)
            Destroy(child.gameObject);

        foreach (var session in sessions)
        {
            // Skip full rooms
            if (session.PlayerCount >= session.MaxPlayers)
                continue;

            var item = Instantiate(roomListItemPrefab, roomListContent);
            var ui = item.GetComponent<RoomListItemUI>();

            ui.Setup(session.Name, session.PlayerCount, session.MaxPlayers);
        }
    }
}