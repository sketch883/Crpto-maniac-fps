// NetworkManager.cs

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {
    // Singleton instance
    public static NetworkManager Instance;

    // Player Prefab
    public GameObject playerPrefab;

    // List of connected players
    private List<GameObject> players = new List<GameObject>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        // Start the network
        NetworkServer.Listen(7777);
        NetworkServer.AddPlayerForConnection();
    }

    public void OnPlayerConnected(NetworkConnection conn) {
        GameObject player = Instantiate(playerPrefab);
        players.Add(player);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public void OnPlayerDisconnected(NetworkConnection conn) {
        GameObject player = conn.playerControllers[0].gameObject;
        players.Remove(player);
        Destroy(player);
    }

    // Add methods for player synchronization and network events as needed
}