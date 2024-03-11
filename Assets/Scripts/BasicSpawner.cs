using Fusion.Sockets;
using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using WebSocketSharp;
using Unity.VisualScripting;
using TMPro;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{


    //  [SerializeField] private NetworkPrefabRef _playerPrefab;
    //   private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public bool connectOnAwake = false;
    [HideInInspector] public NetworkRunner runner_;

    [SerializeField] NetworkObject playerPrefab;

    public Transform RoomListParent;
    public GameObject roomInfoblock;
    void Awake()
    {
        if (connectOnAwake)
        {
           // ConnectToRunner();
            
        }
    }


    void Start()
    {
      
    }

    public async void ConnectToRunner(GameMode modemymode)
    {
        if (runner_ == null)
        {
            runner_ = gameObject.AddComponent<NetworkRunner>();


        }
        string myroom = GameObject.FindAnyObjectByType<CharacterSelection>().roomName;
        if (myroom == null)
            myroom = "TestPhoton1";

       
            await runner_.StartGame(new StartGameArgs()
            {
                GameMode = modemymode,
                SessionName = myroom,
                PlayerCount = 5,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()

            });
      
    }
    // Utility method to Join the ClientServer Lobby
    public async void JoinLobby()
    {
        if (runner_ == null)
        {
            runner_ = gameObject.AddComponent<NetworkRunner>();


        }
        var result = await runner_.JoinSessionLobby(SessionLobby.Shared);

        if (result.Ok)
        {
            Debug.Log("Lobby joinned");
            // all good
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            // runner = null;
           // runner_ = null ;
            if(gameObject.GetComponent<NetworkRunner>() != null)
                runner_ = gameObject.GetComponent<NetworkRunner>();
            else
            runner_ = gameObject.AddComponent<NetworkRunner>();
        }
    }





    // Receive the List of Sessions from the current Lobby


    //[SerializeField] private InputAction accelerate;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {

        print(player.PlayerId + "Joinned ");
           /* if (runner.IsServer)
            {
                // Create a unique position for the player
                Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
                NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
                // Keep track of the player avatars so we can remove it when they disconnect
                _spawnedCharacters.Add(player, networkPlayerObject);
            }*/

        
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        /*   if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
           {
               runner.Despawn(networkObject);
               _spawnedCharacters.Remove(player);
           }*/
        print("Player left do some thing ");
    }
    public void OnInput(NetworkRunner runner, NetworkInput input) {
   
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
        Debug.LogError(shutdownReason.ToString());
   
      //  runner_ = null   ;

    }
    public void OnConnectedToServer(NetworkRunner runner) {

        Debug.Log("Connected to Server");
     NetworkObject playerObject =    runner.Spawn(playerPrefab, Vector3.zero);
    runner.SetPlayerObject(runner.LocalPlayer, playerObject);
     
    }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {
      
      //  runner.Shutdown(true,shutdownReason:ShutdownReason.ConnectionTimeout);
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {

        Debug.Log($"Session List Updated with {sessionList.Count} session(s)");

        

        // Check if there are any Sessions to join
        if (sessionList.Count > 0)
        {

            
            for(int i = 0; i < sessionList.Count; i++)
            { 
                Debug.Log($"Joining {sessionList[i].Name}");
                Debug.Log($"Joining {sessionList[i].MaxPlayers}");
            
                    GameObject block = Instantiate(roomInfoblock, RoomListParent);// roomInfoblock
                    block.transform.GetChild(0).GetComponent<TMP_Text>().text = sessionList[i].Name;
            }

        }
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

   // private NetworkRunner _runner;

    private void OnGUI()
    {



        if (runner_ != null)
        {
            if (runner_.LobbyInfo.IsValid)
            {
                if (!runner_.IsConnectedToServer)//== null)
                {
                    if (GUI.Button(new Rect(0, 0, 200, 40), "Connect to Server"))
                    {
                        ConnectToRunner(GameMode.Shared);

                    }
                }
            }
            else
            {
             //   runner.LagCompensation()
                if (!runner_.IsConnectedToServer)
                    GUI.Label(new Rect(0, 0, 200, 40), "Loading ... ");
            }
            
        }
       if (runner_ == null)

            if (GUI.Button(new Rect(0, 0, 200, 40), "Connect to Lobby"))
            {
                JoinLobby();
            }
        
    }

  //  async void StartGame(GameMode mode)
   // {
        // Create the Fusion runner and let it know that we will be providing user input
    //    _runner = gameObject.AddComponent<NetworkRunner>();
    //    _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
      //  await _runner.StartGame(new StartGameArgs()
      //  {
       //     GameMode = mode,
        //    SessionName = "TestRoom",
        //    Scene = SceneManager.GetActiveScene().buildIndex,
        //    SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
      //  });
   // }
}