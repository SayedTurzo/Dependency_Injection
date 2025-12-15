using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FusionLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public static FusionLauncher Instance;
    
    [Header("UI References")]
    public MenuUIController menuUI;
    
    // The main runner that handles the connection
    private NetworkRunner _runner;

    void Awake()
    {
        // Singleton pattern to ensure only one launcher exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // On start, we automatically join the "Lobby" to see the room list
        JoinLobby();
    }

    // 1. JOIN THE MENY LOBBY (To see room lists)
    public async void JoinLobby()
    {
        if (_runner != null) _runner.Shutdown();

        // Create the runner object
        _runner = CreateRunner();
        
        Debug.Log("Connecting to Lobby...");
        await _runner.JoinSessionLobby(SessionLobby.ClientServer);
    }

    // 2. CREATE A ROOM (HOST)
    public async void CreateRoom(string roomName, int maxPlayers)
    {
        if (_runner != null) _runner.Shutdown();
        _runner = CreateRunner();

        Debug.Log($"Creating Room: {roomName} with {maxPlayers} players");

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            PlayerCount = maxPlayers,
            SceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    // 3. JOIN A ROOM (CLIENT)
    public async void JoinRoom(string roomName)
    {
        if (_runner != null) _runner.Shutdown();
        _runner = CreateRunner();

        Debug.Log($"Joining Room: {roomName}");

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = roomName,
            SceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    
    // Helper to create the runner GameObject
    private NetworkRunner CreateRunner()
    {
        GameObject go = new GameObject("NetworkRunner");
        DontDestroyOnLoad(go);
        NetworkRunner newRunner = go.AddComponent<NetworkRunner>();
        newRunner.AddCallbacks(this);
        newRunner.ProvideInput = true;
        return newRunner;
    }

    // 4. START THE GAME (Host Only)
    // This moves everyone from the "Waiting" state to the "Game" scene
    public void StartGame()
    {
        if (_runner.IsServer)
        {
            Debug.Log("Host is starting the game...");
            // Load Scene Index 1 (GameScene)
            _runner.LoadScene(SceneRef.FromIndex(1)); 
        }
    }

    // --- FUSION CALLBACKS ---

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        // Pass the list to the UI
        menuUI.UpdateRoomList(sessionList);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // 1. If I am the Server, and I just joined (my local player joined)
        if (runner.IsServer && player == runner.LocalPlayer)
        {
            // 2. Store my Player ID into the Session Properties
            var props = new Dictionary<string, SessionProperty>();
            props.Add("HostID", player.PlayerId); // Save Host ID
            
            runner.SessionInfo.UpdateCustomProperties(props);
        }
        
        // When a player joins, update the "Waiting Room" UI
        menuUI.UpdateWaitingRoomInfo(runner);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        menuUI.UpdateWaitingRoomInfo(runner);
    }

    // Required Boilerplate (Leave Empty)
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
}