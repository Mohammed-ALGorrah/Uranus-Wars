using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine.PlayerLoop;

public class NetworkRunnerHandler : MonoBehaviour
{
    #region Variables
    [Header("Prefab")]
    public NetworkRunner networkRunnerPrefab;
    NetworkRunner networkRunner;
    #endregion

    #region Unity Setup

    private void Awake()
    {
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();

        if (networkRunner != null)
        {
            networkRunner = networkRunnerInScene;
        }
    }
    void Start()
    {
        if (networkRunner == null)
        {
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "Network Runner";

            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient,"Test Session", NetAddress.Any(),
                                SceneManager.GetActiveScene().buildIndex, null);
            }
            

            Debug.Log("Server Network Runner Started");
        }
    }

    #endregion

    #region Network Task

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner,GameMode gameMode,string sessionName,NetAddress address,SceneRef scene,Action<NetworkRunner> initialized)
    {
        var sceneManager = GetSceneManager(runner);

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "OurLobbyID",
            Initialized = initialized,
            SceneManager = sceneManager,
            PlayerCount = 2
        });
    }
    
    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return sceneManager;
    }

    public void OnJoinLobby()
    {
        var clientTask = JoinLobby();

    }

    async Task JoinLobby()
    {
        string lobbyID = "OurLobbyID";

        var result = await networkRunner.JoinSessionLobby(SessionLobby.Custom,lobbyID);

        if (!result.Ok)
        {
            Debug.Log($"Erorr to join lobby {lobbyID}");
        }
        else
        {
            Debug.Log("Join Lobby ok");
        }
    }

    public void CreateGame(string sessionName,string sceneName)
    {
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, sessionName, NetAddress.Any(),
                                SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}"), null);
    }

    public void JoinGame(SessionInfo sessionInfo)
    {
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, sessionInfo.Name, NetAddress.Any(),
                               SceneManager.GetActiveScene().buildIndex, null);
    }
    #endregion

}
