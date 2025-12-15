using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fusion;

public class MenuUIController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainButtonsPanel;
    public GameObject createRoomPanel;
    public GameObject joinRoomPanel;
    public GameObject waitingRoomPanel; // New Panel for "Inside Room"
    public GameObject loadingPanel;

    [Header("Create Room Inputs")]
    public TMP_InputField roomNameInput;
    
    [Header("Join Room List")]
    public Transform contentParent;
    public GameObject roomItemPrefab;

    [Header("Waiting Room UI")]
    public Transform playerListContent; // <-- NEW: The Layout Group for players
    public GameObject playerItemPrefab; // <-- NEW: The WaitingPlayerItem prefab
    public TextMeshProUGUI playerCountText;
    public Button startGameButton; // Only visible to Host
    
    private NetworkRunner _currentRunner;

    void Start()
    {
        ShowPanel(mainButtonsPanel);
        if(loadingPanel) loadingPanel.SetActive(false);
    }

    public void OnCreateBtnClicked() => ShowPanel(createRoomPanel);
    public void OnJoinMenuBtnClicked() => ShowPanel(joinRoomPanel);
    public void OnBackBtnClicked() => ShowPanel(mainButtonsPanel);

    // CALL THIS FROM UI BUTTON "Confirm Create"
    public void OnConfirmCreateRoom()
    {
        string name = roomNameInput.text;
        if (string.IsNullOrEmpty(name)) return;
        
        ShowLoading(); // Show loading immediately
        FusionLauncher.Instance.CreateRoom(roomNameInput.text, 4);
    }

    // CALL THIS FROM THE ROOM LIST ITEM
    public void JoinSelectedRoom(string roomName)
    {
        ShowLoading();
        FusionLauncher.Instance.JoinRoom(roomName);
        //ShowPanel(waitingRoomPanel);
    }

    public void OnStartGameClicked()
    {
        FusionLauncher.Instance.StartGame();
    }

    // --- UPDATES ---

    public void UpdateRoomList(List<SessionInfo> sessions)
    {
        // Clean list
        foreach (Transform child in contentParent) Destroy(child.gameObject);

        foreach (var session in sessions)
        {
            if (session.PlayerCount >= session.MaxPlayers) continue; // Skip full rooms

            GameObject item = Instantiate(roomItemPrefab, contentParent);
            item.GetComponent<RoomListItemUI>().Setup(session);
        }
    }

    public void UpdateWaitingRoomInfo(NetworkRunner runner)
    {
        _currentRunner = runner; // Keep reference for the repeater
        // 1. Hide Loading if we are here
        loadingPanel.SetActive(false);
        
        // 2. Switch to waiting panel if not already
        if (!waitingRoomPanel.activeSelf) ShowPanel(waitingRoomPanel);
        
        RefreshPlayerList(); // Run immediately

        // // 3. Update Text
        // int currentPlayers = runner.SessionInfo.PlayerCount;
        // int maxPlayers = runner.SessionInfo.MaxPlayers;
        // playerCountText.text = $"Waiting for players... {currentPlayers} / {maxPlayers}";
        //
        // // --- FETCH THE HOST ID ---
        // int hostId = -1;
        // if (runner.SessionInfo.Properties != null && runner.SessionInfo.Properties.ContainsKey("HostID"))
        // {
        //     hostId = runner.SessionInfo.Properties["HostID"];
        // }
        //
        // // 4. REBUILD PLAYER LIST
        // foreach (Transform child in playerListContent) Destroy(child.gameObject);
        //
        // foreach (PlayerRef player in runner.ActivePlayers)
        // {
        //     GameObject item = Instantiate(playerItemPrefab, playerListContent);
        //     string name = $"Player {player.PlayerId}";
        //     
        //     // 1. Identify Yourself
        //     if (player == runner.LocalPlayer)
        //     {
        //         name += " (You)";
        //     }
        //
        //     // 2. Identify Host (Check against the stored ID)
        //     if (player.PlayerId == hostId)
        //     {
        //         name += " <color=yellow>[Host]</color>"; // Added color for visibility
        //     }
        //
        //     item.GetComponent<WaitingPlayerItem>().Setup(name);
        // }
        //
        // // Logic: Only Host can see the button. Only enable if full.
        // if (runner.IsServer)
        // {
        //     startGameButton.gameObject.SetActive(true);
        //     
        //     // Allow start if we have 4 players (or whatever max is)
        //     if (currentPlayers >= maxPlayers)
        //     {
        //         startGameButton.interactable = true;
        //         playerCountText.text = "READY TO START!";
        //     }
        //     else
        //     {
        //         startGameButton.interactable = false;
        //     }
        // }
        // else
        // {
        //     // Clients don't see the start button
        //     startGameButton.gameObject.SetActive(false);
        // }
    }
    
    // NEW: Separate function to rebuild the list
    private void RefreshPlayerList()
    {
        if (_currentRunner == null || !_currentRunner.IsRunning) return;

        // 1. Stats
        int current = _currentRunner.SessionInfo.PlayerCount;
        int max = _currentRunner.SessionInfo.MaxPlayers;
        playerCountText.text = $"Waiting for players... {current}/{max}";

        // --- 1. GET HOST ID ---
        int hostId = -1;
        
        // Try to find the "HostID" in the session properties
        if (_currentRunner.SessionInfo.Properties != null && 
            _currentRunner.SessionInfo.Properties.TryGetValue("HostID", out var prop))
        {
            hostId = (int)prop; // Explicitly cast to int
        }
        else
        {
            // Debugging: Why is it missing?
            // Debug.Log("Looking for HostID... Not found yet.");
        }

        // 3. Rebuild List
        foreach (Transform child in playerListContent) Destroy(child.gameObject);

        foreach (PlayerRef player in _currentRunner.ActivePlayers)
        {
            GameObject item = Instantiate(playerItemPrefab, playerListContent);
            string name = $"Player {player.PlayerId}";
            
            // Add (You)
            if (player == _currentRunner.LocalPlayer) name += " (You)";

            // Is this the Host?
            // Check A: Does ID match the property?
            // Check B: Fallback - If I am the server, I am the host
            // Check C (Temporary Fallback): If property is missing, assume Player 1 is Host
            // (This helps fix the visual glitch if network is slow)
            bool isHost = player.PlayerId == hostId || _currentRunner.IsServer && player == _currentRunner.LocalPlayer;
            if (hostId == -1 && player.PlayerId == 1) isHost = true; 
            if (isHost) name += " <color=yellow>[Host]</color>";

            item.GetComponent<WaitingPlayerItem>().Setup(name);
        }

        // 4. Host Button
        if (_currentRunner.IsServer)
        {
            startGameButton.gameObject.SetActive(true);
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
    }

    private void ShowPanel(GameObject panelToShow)
    {
        mainButtonsPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        joinRoomPanel.SetActive(false);
        waitingRoomPanel.SetActive(false);

        panelToShow.SetActive(true);
        
        // AUTO REFRESH: If we opened waiting room, start refreshing every 1s
        CancelInvoke(nameof(RefreshPlayerList));
        if (panelToShow == waitingRoomPanel)
        {
            InvokeRepeating(nameof(RefreshPlayerList), 0.5f, 1.0f);
        }
    }
    
    public void ShowLoading()
    {
        if(loadingPanel) loadingPanel.SetActive(true);
    }
}