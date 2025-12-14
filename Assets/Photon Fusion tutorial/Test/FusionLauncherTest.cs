using System;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class FusionLauncherTest : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("Prefabs")]
    public NetworkObject playerPrefab;

    private NetworkRunner runner;
    private HashSet<PlayerRef> spawnedPlayers = new HashSet<PlayerRef>();

    async void Start()
    {
        runner = GetComponent<NetworkRunner>();
        runner.ProvideInput = true;
        runner.AddCallbacks(this);

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "TestRoom",
            Scene = SceneRef.FromIndex(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            ),
            SceneManager = GetComponent<NetworkSceneManagerDefault>()
        });
    }

    // ---------- PLAYER / SCENE ----------

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player joined: {player}");

        // Only the server spawns players
        if (!runner.IsServer) return;

        // If this player already has a character, do nothing
        if (spawnedPlayers.Contains(player)) return;

        Vector3 spawnPos = new Vector3(player.RawEncoded * 2, 0, 0);
        runner.Spawn(playerPrefab, spawnPos, Quaternion.identity, player);

        spawnedPlayers.Add(player);
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    // ---------- AOI (DO NOT THROW) ----------

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    // ---------- INPUT ----------

    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    // ---------- CONNECTION ----------

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    // ---------- RELIABLE DATA ----------

    public void OnReliableDataReceived(
        NetworkRunner runner,
        PlayerRef player,
        ArraySegment<byte> data
    ) { }

    public void OnReliableDataReceived(
        NetworkRunner runner,
        PlayerRef player,
        ReliableKey key,
        ArraySegment<byte> data
    ) { }

    public void OnReliableDataProgress(
        NetworkRunner runner,
        PlayerRef player,
        ReliableKey key,
        float progress
    ) { }

    // ---------- MISC ----------

    public void OnConnectRequest(
        NetworkRunner runner,
        NetworkRunnerCallbackArgs.ConnectRequest request,
        byte[] token
    ) { }

    public void OnConnectFailed(
        NetworkRunner runner,
        NetAddress remoteAddress,
        NetConnectFailedReason reason
    ) { }

    public void OnUserSimulationMessage(
        NetworkRunner runner,
        SimulationMessagePtr message
    ) { }

    public void OnSessionListUpdated(
        NetworkRunner runner,
        List<SessionInfo> sessionList
    ) { }

    public void OnCustomAuthenticationResponse(
        NetworkRunner runner,
        Dictionary<string, object> data
    ) { }

    public void OnHostMigration(
        NetworkRunner runner,
        HostMigrationToken hostMigrationToken
    ) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }
}
