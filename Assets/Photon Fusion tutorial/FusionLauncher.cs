using System;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class FusionLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public static FusionLauncher Instance;

    private NetworkRunner runner;
    
    public MenuUIController menuUIController;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        runner = GetComponent<NetworkRunner>();
        runner.AddCallbacks(this);
        runner.ProvideInput = true;
    }

    // ----------- CREATE ROOM (HOST) -----------

    public async void CreateRoom(string roomName)
    {
        Debug.Log("Creating room: " + roomName);

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            Scene = SceneRef.FromIndex(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            ),
            SceneManager = GetComponent<NetworkSceneManagerDefault>()
        });
    }

    // ----------- JOIN ROOM (CLIENT) -----------

    public async void JoinRoom(string roomName)
    {
        Debug.Log("Joining room: " + roomName);

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = roomName,
            Scene = SceneRef.FromIndex(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            ),
            SceneManager = GetComponent<NetworkSceneManagerDefault>()
        });
    }

    // ----------- REQUIRED EMPTY CALLBACKS -----------

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) {}

    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) {}

    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) {}

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        menuUIController.UpdateRoomList(sessionList);
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, System.ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}
