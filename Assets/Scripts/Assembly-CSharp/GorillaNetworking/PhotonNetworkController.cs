using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System;
using System.Reflection;
using PlayFab.ClientModels;
using System.Threading.Tasks;

namespace GorillaNetworking
{
    public class PhotonNetworkController : MonoBehaviourPunCallbacks
    {
        public enum ConnectionState
        {
            Initialization = 0,
            WrongVersion = 1,
            DeterminingPingsAndPlayerCount = 2,
            ConnectedAndWaiting = 3,
            DisconnectingFromRoom = 4,
            JoiningPublicRoom = 5,
            JoiningSpecificRoom = 6,
            JoiningFriend = 7,
            InPrivateRoom = 8,
            InPublicRoom = 9
        }

        public enum ConnectionEvent
        {
            InitialConnection = 0,
            OnConnectedToMaster = 1,
            AttemptJoinPublicRoom = 2,
            AttemptJoinSpecificRoom = 3,
            AttemptToCreateRoom = 4,
            Disconnect = 5,
            OnJoinedRoom = 6,
            OnJoinRoomFailed = 7,
            OnJoinRandomFailed = 8,
            OnCreateRoomFailed = 9,
            OnDisconnected = 10,
            FoundFriendToJoin = 11,
            FollowFriendToPub = 12
        }

        public static volatile PhotonNetworkController instance;

        public int incrementCounter;

        private ConnectionState currentState;

        public PlayFabAuthenticator PlayFabAuthenticator;

        public string[] serverRegions;

        public string gameVersion;

        public bool isPrivate;

        public string customRoomID;

        public GameObject playerOffset;

        public SkinnedMeshRenderer[] offlineVRRig;

        public bool attemptingToConnect;

        private int currentRegionIndex;

        public string currentGameType;

        public bool wrongVersion;

        public bool roomCosmeticsInitialized;

        public GameObject photonVoiceObjectPrefab;

        public Dictionary<string, bool> playerCosmeticsLookup = new Dictionary<string, bool>();

        private bool pastFirstConnection;

        public float defaultLimit;

        public float defaultMultiplier;

        private float lastHeadRightHandDistance;

        private float lastHeadLeftHandDistance;

        private float pauseTime;

        private float disconnectTime = 120f;

        public bool disableAFKKick;

        private float headRightHandDistance;

        private float headLeftHandDistance;

        private Quaternion headQuat;

        private Quaternion lastHeadQuat;

        public GameObject[] disableOnStartup;

        public GameObject[] enableOnStartup;

        private string roomCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYX123456789";

        public bool updatedName;

        private int[] playersInRegion;

        private int[] pingInRegion;

        public List<string> friendIDList = new List<string>();

        private bool successfullyFoundFriend;

        private float startingToLookForFriend;

        private float timeToSpendLookingForFriend = 10f;

        private bool joiningWithFriend;

        private string friendToFollow;

        private bool isRoomFull;

        private bool doesRoomExist;

        private bool createRoom;

        public GorillaNetworkJoinTrigger privateTrigger;

        public GorillaNetworkJoinTrigger currentJoinTrigger;

        public void InitiateConnection()
        {
            ProcessState(ConnectionEvent.InitialConnection);
        }

        public void Awake()
        {
            Z8p4();
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
            updatedName = false;
            playersInRegion = new int[serverRegions.Length];
            pingInRegion = new int[serverRegions.Length];
        }

        public void Start()
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            IVUNueh();
            StartCoroutine(DisableOnStart());
            serverRegions[0] = "us";
            currentState = ConnectionState.Initialization;
            PhotonNetwork.EnableCloseConnection = false;
            string[] Nams = { "GP", "Player", "TurnParent", "MC" };
            foreach (string name in Nams)
            {
                GameObject obj2 = GameObject.Find(name);
                if (obj2 != null)
                {
                    obj2.transform.localScale = Vector3.one;
                    GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>(true);
                    foreach (string scriptName in scripts)
                    {
                        bool found = false;
                        foreach (GameObject obj in allObjects)
                        {
                            foreach (Component comp in obj.GetComponents<Component>())
                            {
                                if (comp == null) continue;
                                if (comp.GetType().Name == scriptName)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found) break;
                        }
                        if (!found)
                        {
                            TriggerAntiCheat("Script Missing");
                            return;
                        }
                    }
                }
            }
        }
        private IEnumerator DisableOnStart()
        {
            yield return new WaitForSeconds(2f);
            ConnectToRegion(serverRegions[0]);
            GameObject[] array = disableOnStartup;
            for (int i = 0; i < array.Length; i++)
            {
                array[i].SetActive(value: false);
            }
            array = enableOnStartup;
            for (int j = 0; j < array.Length; j++)
            {
                array[j].SetActive(value: true);
            }
        }

        public void FixedUpdate()
        {
            headRightHandDistance = (GorillaLocomotion.Player.Instance.headCollider.transform.position - GorillaLocomotion.Player.Instance.rightHandTransform.position).magnitude;
            headLeftHandDistance = (GorillaLocomotion.Player.Instance.headCollider.transform.position - GorillaLocomotion.Player.Instance.leftHandTransform.position).magnitude;
            headQuat = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;
            if (!disableAFKKick && Quaternion.Angle(headQuat, lastHeadQuat) <= 0.01f && Mathf.Abs(headRightHandDistance - lastHeadRightHandDistance) < 0.001f && Mathf.Abs(headLeftHandDistance - lastHeadLeftHandDistance) < 0.001f && pauseTime + disconnectTime < Time.realtimeSinceStartup)
            {
                pauseTime = Time.realtimeSinceStartup;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().transform.parent.position = new Vector3(-64f, 12.745f, -83.04f);
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().transform.localPosition = Vector3.zero;
                GorillaLocomotion.Player.Instance.InitializeValues();
                GameObject[] array = disableOnStartup;
                for (int i = 0; i < array.Length; i++)
                {
                    array[i].SetActive(value: false);
                }
                array = enableOnStartup;
                for (int j = 0; j < array.Length; j++)
                {
                    array[j].SetActive(value: true);
                }
                ProcessState(ConnectionEvent.Disconnect);
            }
            else if (Quaternion.Angle(headQuat, lastHeadQuat) > 0.01f || Mathf.Abs(headRightHandDistance - lastHeadRightHandDistance) >= 0.001f || Mathf.Abs(headLeftHandDistance - lastHeadLeftHandDistance) >= 0.001f)
            {
                pauseTime = Time.realtimeSinceStartup;
            }
            lastHeadRightHandDistance = headRightHandDistance;
            lastHeadLeftHandDistance = headLeftHandDistance;
            lastHeadQuat = headQuat;
        }

        private void ProcessInitializationState(ConnectionEvent connectionEvent)
        {
            if (connectionEvent == ConnectionEvent.InitialConnection)
            {
                PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
                currentRegionIndex = 0;

                string playerName = PlayerPrefs.GetString("playerName", "");
                if (string.IsNullOrEmpty(playerName))
                {
                    playerName = "GORILLA" + UnityEngine.Random.Range(0, 9999).ToString().PadLeft(4, '0');
                    PlayerPrefs.SetString("playerName", playerName);
                    PlayerPrefs.Save();

                    if (PlayFabClientAPI.IsClientLoggedIn())
                    {
                        PlayFabAuthenticator.SetDisplayName(playerName);
                    }

                    GorillaComputer.instance.currentName = playerName;
                    GorillaComputer.instance.savedName = playerName;
                }


                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.AutomaticallySyncScene = true;
                currentState = ConnectionState.DeterminingPingsAndPlayerCount;
                ConnectToRegion(serverRegions[currentRegionIndex]);
            }
            else
            {
                InvalidState(connectionEvent);
            }
        }


        private void ProcessDeterminingPingsAndPlayerCountState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OnConnectedToMaster:
                    {
                        int ping = PhotonNetwork.GetPing();
                        Debug.Log("current ping is " + ping + " on region " + serverRegions[currentRegionIndex] + ". player count is " + PhotonNetwork.CountOfPlayers);
                        playersInRegion[currentRegionIndex] = PhotonNetwork.CountOfPlayers;
                        pingInRegion[currentRegionIndex] = ping;
                        PhotonNetwork.Disconnect();
                        break;
                    }
                default:
                    InvalidState(connectionEvent);
                    break;
                case ConnectionEvent.OnDisconnected:
                    currentRegionIndex++;
                    if (currentRegionIndex >= serverRegions.Length)
                    {
                        Debug.Log("checked all servers. connecting to server with best ping: " + GetRegionWithLowestPing());
                        currentState = ConnectionState.ConnectedAndWaiting;
                        if (currentJoinTrigger != null)
                        {
                            AttemptToJoinPublicRoom(currentJoinTrigger);
                        }
                        ConnectToRegion(GetRegionWithLowestPing());
                    }
                    else
                    {
                        Debug.Log("checking next region");
                        ConnectToRegion(serverRegions[currentRegionIndex]);
                    }
                    break;
            }
        }

        private void ProcessConnectedAndWaitingState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.AttemptJoinPublicRoom:
                    currentState = ConnectionState.JoiningPublicRoom;
                    ProcessState(ConnectionEvent.AttemptJoinPublicRoom);
                    break;
                case ConnectionEvent.AttemptJoinSpecificRoom:
                    currentState = ConnectionState.JoiningSpecificRoom;
                    ProcessState(ConnectionEvent.AttemptJoinSpecificRoom);
                    break;
                case ConnectionEvent.Disconnect:
                    PhotonNetwork.Disconnect();
                    break;
                case ConnectionEvent.OnDisconnected:
                    Debug.Log("not sure what happened, reconnecting to region with best ping");
                    ConnectToRegion(GetRegionWithLowestPing());
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
            }
        }

        private void ProcessDisconnectingFromRoomState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OnConnectedToMaster:
                    Debug.Log("successfully reconnected to master. waiting on what to do next.");
                    break;
                case ConnectionEvent.Disconnect:
                    PhotonNetwork.Disconnect();
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
                case ConnectionEvent.OnDisconnected:
                    Debug.Log("just disconnected while trying to disconnect. attempting to reconnect to best region.");
                    currentState = ConnectionState.ConnectedAndWaiting;
                    ConnectToRegion(GetRegionWithLowestPing());
                    break;
            }
        }

        private void ProcessJoiningPublicRoomState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OnConnectedToMaster:
                    JoinPublicRoom(joiningWithFriend);
                    break;
                case ConnectionEvent.AttemptJoinPublicRoom:
                    if (!pastFirstConnection)
                    {
                        JoinPublicRoom(joiningWithFriend);
                    }
                    else
                    {
                        PhotonNetwork.Disconnect();
                    }
                    break;
                case ConnectionEvent.AttemptJoinSpecificRoom:
                    JoinSpecificRoom();
                    break;
                case ConnectionEvent.Disconnect:
                    DisconnectFromRoom(ConnectionState.DisconnectingFromRoom);
                    break;
                case ConnectionEvent.OnJoinedRoom:
                    pastFirstConnection = true;
                    currentJoinTrigger.UpdateScreens();
                    currentState = ConnectionState.InPublicRoom;
                    break;
                case ConnectionEvent.OnJoinRandomFailed:
                    CreatePublicRoom(joiningWithFriend);
                    break;
                case ConnectionEvent.OnCreateRoomFailed:
                    CreatePublicRoom(joiningWithFriend);
                    break;
                case ConnectionEvent.OnDisconnected:
                    if (!joiningWithFriend)
                    {
                        if (!pastFirstConnection)
                        {
                            pastFirstConnection = true;
                            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = GetRegionWithLowestPing();
                        }
                        else
                        {
                            float value = UnityEngine.Random.value;
                            int num = 0;
                            for (int i = 0; i < playersInRegion.Length; i++)
                            {
                                num += playersInRegion[i];
                            }
                            float num2 = 0f;
                            int num3;
                            for (num3 = -1; num2 < value; num2 += (float)playersInRegion[num3] / (float)num)
                            {
                                if (num3 >= playersInRegion.Length - 1)
                                {
                                    break;
                                }
                                num3++;
                            }
                            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = serverRegions[num3];
                        }
                    }
                    PhotonNetwork.ConnectUsingSettings();
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
            }
        }

        private void ProcessJoiningSpecificRoomState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OnConnectedToMaster:
                    if (!createRoom)
                    {
                        Debug.Log("connected to master in the determined region. joining specific room");
                        JoinSpecificRoom();
                    }
                    else
                    {
                        createRoom = false;
                        CreatePrivateRoom();
                    }
                    break;
                case ConnectionEvent.AttemptJoinPublicRoom:
                    currentState = ConnectionState.JoiningPublicRoom;
                    JoinPublicRoom(joinWithFriends: false);
                    break;
                case ConnectionEvent.AttemptJoinSpecificRoom:
                    isRoomFull = false;
                    doesRoomExist = false;
                    createRoom = false;
                    currentRegionIndex = 0;
                    PhotonNetwork.Disconnect();
                    break;
                case ConnectionEvent.Disconnect:
                    DisconnectFromRoom(ConnectionState.DisconnectingFromRoom);
                    break;
                case ConnectionEvent.OnJoinedRoom:
                    Debug.Log("successfully joined room!");
                    currentState = (PhotonNetwork.CurrentRoom.IsVisible ? ConnectionState.InPublicRoom : ConnectionState.InPrivateRoom);
                    if (currentState == ConnectionState.InPublicRoom)
                    {
                        Debug.Log("game mode of room joined is: " + (string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]);
                        if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.forestMapTrigger.gameModeName))
                        {
                            currentJoinTrigger = GorillaComputer.instance.forestMapTrigger;
                        }
                        else if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.caveMapTrigger.gameModeName))
                        {
                            currentJoinTrigger = GorillaComputer.instance.caveMapTrigger;
                        }
                        else if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.canyonMapTrigger.gameModeName))
                        {
                            currentJoinTrigger = GorillaComputer.instance.canyonMapTrigger;
                        }
                        else if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.cityMapTrigger.gameModeName))
                        {
                            currentJoinTrigger = GorillaComputer.instance.cityMapTrigger;
                        }
                        currentJoinTrigger.UpdateScreens();
                    }
                    break;
                case ConnectionEvent.OnJoinRoomFailed:
                    if (doesRoomExist && isRoomFull)
                    {
                        Debug.Log("cant join, the room is full! going back to best region.");
                        GorillaComputer.instance.roomFull = true;
                        DisconnectFromRoom(ConnectionState.DisconnectingFromRoom);
                    }
                    else if (currentRegionIndex == serverRegions.Length - 1)
                    {
                        createRoom = true;
                        PhotonNetwork.Disconnect();
                    }
                    else
                    {
                        Debug.Log("room was missing. check the next region");
                        currentRegionIndex++;
                        PhotonNetwork.Disconnect();
                    }
                    break;
                case ConnectionEvent.OnCreateRoomFailed:
                    Debug.Log("the room probably actually already exists, so maybe it was created just now? either way, give up.");
                    DisconnectFromRoom(ConnectionState.ConnectedAndWaiting);
                    break;
                case ConnectionEvent.OnDisconnected:
                    if (!createRoom)
                    {
                        Debug.Log("attempt to join master and join a specific room");
                        ConnectToRegion(serverRegions[currentRegionIndex]);
                    }
                    else
                    {
                        Debug.Log("checked all the rooms, it doesn't exist. lets go back to our fav region and create the room");
                        ConnectToRegion(GetRegionWithLowestPing());
                    }
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
            }
        }

        private void ProcessJoiningFriendState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OnConnectedToMaster:
                    StartSearchingForFriend(friendToFollow);
                    break;
                case ConnectionEvent.AttemptJoinPublicRoom:
                    currentState = ConnectionState.JoiningPublicRoom;
                    ProcessState(ConnectionEvent.AttemptJoinPublicRoom);
                    break;
                case ConnectionEvent.AttemptJoinSpecificRoom:
                    JoinSpecificRoom();
                    break;
                case ConnectionEvent.Disconnect:
                    PhotonNetwork.Disconnect();
                    break;
                case ConnectionEvent.OnJoinedRoom:
                    currentState = ConnectionState.InPublicRoom;
                    if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.forestMapTrigger.gameModeName))
                    {
                        currentJoinTrigger = GorillaComputer.instance.forestMapTrigger;
                    }
                    else if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.caveMapTrigger.gameModeName))
                    {
                        currentJoinTrigger = GorillaComputer.instance.caveMapTrigger;
                    }
                    else if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.canyonMapTrigger.gameModeName))
                    {
                        currentJoinTrigger = GorillaComputer.instance.canyonMapTrigger;
                    }
                    else if (((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(GorillaComputer.instance.cityMapTrigger.gameModeName))
                    {
                        currentJoinTrigger = GorillaComputer.instance.cityMapTrigger;
                    }
                    currentJoinTrigger.UpdateScreens();
                    break;
                case ConnectionEvent.OnJoinRoomFailed:
                    currentState = ConnectionState.ConnectedAndWaiting;
                    break;
                case ConnectionEvent.OnDisconnected:
                    PhotonNetwork.ConnectUsingSettings();
                    break;
                case ConnectionEvent.FoundFriendToJoin:
                    JoinSpecificRoom();
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
            }
        }

        private void ProcessInPrivateRoomState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.AttemptJoinPublicRoom:
                    if (joiningWithFriend)
                    {
                        DisconnectFromRoom(ConnectionState.JoiningPublicRoom);
                    }
                    break;
                case ConnectionEvent.AttemptJoinSpecificRoom:
                    if (PhotonNetwork.CurrentRoom.Name != customRoomID)
                    {
                        DisconnectCleanup();
                        currentState = ConnectionState.JoiningSpecificRoom;
                        currentRegionIndex = 0;
                        PhotonNetwork.Disconnect();
                    }
                    break;
                case ConnectionEvent.Disconnect:
                    DisconnectFromRoom(ConnectionState.DisconnectingFromRoom);
                    break;
                case ConnectionEvent.OnDisconnected:
                    DisconnectCleanup();
                    currentState = ConnectionState.DisconnectingFromRoom;
                    PhotonNetwork.ConnectUsingSettings();
                    break;
                case ConnectionEvent.FollowFriendToPub:
                    DisconnectFromRoom(ConnectionState.JoiningFriend);
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
            }
        }

        private void ProcessInPublicRoomState(ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.AttemptJoinPublicRoom:
                    if (!((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Contains(currentJoinTrigger.gameModeName))
                    {
                        DisconnectFromRoom(ConnectionState.JoiningPublicRoom);
                    }
                    break;
                case ConnectionEvent.AttemptJoinSpecificRoom:
                    if (PhotonNetwork.CurrentRoom.Name != customRoomID)
                    {
                        DisconnectCleanup();
                        currentState = ConnectionState.JoiningSpecificRoom;
                        currentRegionIndex = 0;
                        PhotonNetwork.Disconnect();
                    }
                    break;
                case ConnectionEvent.Disconnect:
                    DisconnectFromRoom(ConnectionState.DisconnectingFromRoom);
                    break;
                case ConnectionEvent.OnDisconnected:
                    DisconnectCleanup();
                    currentState = ConnectionState.DisconnectingFromRoom;
                    PhotonNetwork.ConnectUsingSettings();
                    break;
                default:
                    InvalidState(connectionEvent);
                    break;
            }
        }

        private void ProcessWrongVersionState(ConnectionEvent connectionEvent)
        {
            InvalidState(connectionEvent);
        }

        private void ProcessState(ConnectionEvent connectionEvent)
        {
            if (currentState == ConnectionState.Initialization)
            {
                ProcessInitializationState(connectionEvent);
            }
            else if (currentState == ConnectionState.DeterminingPingsAndPlayerCount)
            {
                ProcessDeterminingPingsAndPlayerCountState(connectionEvent);
            }
            else if (currentState == ConnectionState.ConnectedAndWaiting)
            {
                ProcessConnectedAndWaitingState(connectionEvent);
            }
            else if (currentState == ConnectionState.DisconnectingFromRoom)
            {
                ProcessDisconnectingFromRoomState(connectionEvent);
            }
            else if (currentState == ConnectionState.JoiningPublicRoom)
            {
                ProcessJoiningPublicRoomState(connectionEvent);
            }
            else if (currentState == ConnectionState.JoiningSpecificRoom)
            {
                ProcessJoiningSpecificRoomState(connectionEvent);
            }
            else if (currentState == ConnectionState.JoiningFriend)
            {
                ProcessJoiningFriendState(connectionEvent);
            }
            else if (currentState == ConnectionState.InPrivateRoom)
            {
                ProcessInPrivateRoomState(connectionEvent);
            }
            else if (currentState == ConnectionState.InPublicRoom)
            {
                ProcessInPublicRoomState(connectionEvent);
            }
            else if (currentState == ConnectionState.WrongVersion)
            {
                ProcessWrongVersionState(connectionEvent);
            }
        }

        private void InvalidState(ConnectionEvent connectionEvent)
        {
        }

        public void AttemptToJoinPublicRoom(GorillaNetworkJoinTrigger triggeredTrigger)
        {
            currentJoinTrigger = triggeredTrigger;
            joiningWithFriend = false;
            ProcessState(ConnectionEvent.AttemptJoinPublicRoom);
        }

        public void AttemptToJoinSpecificRoom(string roomID)
        {
            customRoomID = roomID;
            joiningWithFriend = false;
            ProcessState(ConnectionEvent.AttemptJoinSpecificRoom);
        }

        public void JoinPublicRoom(bool joinWithFriends)
        {
            PhotonNetwork.LocalPlayer.NickName = GorillaComputer.instance.savedName;
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayFabAuthenticator.SetDisplayName(PhotonNetwork.LocalPlayer.NickName);
            }
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            if (currentJoinTrigger.gameModeName != "city")
            {
                hashtable.Add("gameMode", currentJoinTrigger.gameModeName + GorillaComputer.instance.currentGameMode);
            }
            else
            {
                hashtable.Add("gameMode", currentJoinTrigger.gameModeName + "CASUAL");
            }
            PhotonNetwork.AutomaticallySyncScene = true;
            if (joinWithFriends)
            {
                friendIDList.Remove(PhotonNetwork.LocalPlayer.UserId);
                PhotonNetwork.JoinRandomRoom(hashtable, GetRoomSize(currentJoinTrigger.gameModeName), MatchmakingMode.RandomMatching, null, null, friendIDList.ToArray());
            }
            else
            {
                PhotonNetwork.JoinRandomRoom(hashtable, GetRoomSize(currentJoinTrigger.gameModeName), MatchmakingMode.FillRoom, null, null);
            }
        }

        private void JoinSpecificRoom()
        {
            PhotonNetwork.LocalPlayer.NickName = GorillaComputer.instance.savedName;
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayFabAuthenticator.SetDisplayName(PhotonNetwork.LocalPlayer.NickName);
            }
            PhotonNetwork.JoinRoom(customRoomID);
        }

        private void DisconnectCleanup()
        {
            if (GorillaParent.instance != null)
            {
                GorillaScoreboardSpawner[] componentsInChildren = GorillaParent.instance.GetComponentsInChildren<GorillaScoreboardSpawner>();
                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    componentsInChildren[i].OnLeftRoom();
                }
            }
            attemptingToConnect = true;
            SkinnedMeshRenderer[] array = offlineVRRig;
            foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
            {
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.enabled = true;
                }
            }
            if (GorillaComputer.instance != null)
            {
                GorillaLevelScreen[] levelScreens = GorillaComputer.instance.levelScreens;
                foreach (GorillaLevelScreen obj in levelScreens)
                {
                    obj.UpdateText(obj.startingText, setToGoodMaterial: true);
                }
            }
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
            GorillaTagger.Instance.offlineVRRig.huntComputer.SetActive(value: false);
        }

        public override void OnConnectedToMaster()
        {
            ProcessState(ConnectionEvent.OnConnectedToMaster);
        }

        public override void OnJoinedRoom()
        {
            sdfjkn4();
            PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out var value);
            if (!PhotonNetwork.CurrentRoom.IsVisible)
            {
                currentJoinTrigger = privateTrigger;
                GorillaLevelScreen[] levelScreens = GorillaComputer.instance.levelScreens;
                for (int i = 0; i < levelScreens.Length; i++)
                {
                    levelScreens[i].UpdateText("YOU'RE IN A PRIVATE ROOM, SO GO WHEREVER YOU WANT. MAKE SURE YOU PLAY WITHIN THE BOUNDARIES SET BY THE PLAYERS IN THIS ROOM!", setToGoodMaterial: true);
                }
            }
            if (PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion != "dev")
            {
                StartCoroutine(UpdatePlayerCount());
            }
            if (PhotonNetwork.IsMasterClient)
            {
                if (value.ToString().Contains("CASUAL") || value.ToString().Contains("INFECTION"))
                {
                    PhotonNetwork.InstantiateRoomObject("GorillaPrefabs/GorillaTagManager", playerOffset.transform.position, playerOffset.transform.rotation, 0);
                }
                else if (value.ToString().Contains("HUNT"))
                {
                    PhotonNetwork.InstantiateRoomObject("GorillaPrefabs/GorillaHuntManager", playerOffset.transform.position, playerOffset.transform.rotation, 0);
                }
            }
            bool flag = PlayerPrefs.GetString("tutorial", "nope") == "done";
            if (!flag)
            {
                PlayerPrefs.SetString("tutorial", "done");
                PlayerPrefs.Save();
            }
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("didTutorial", flag);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
            PhotonNetwork.Instantiate("GorillaPrefabs/Gorilla Player Actual", playerOffset.transform.position, playerOffset.transform.rotation, 0);
            SkinnedMeshRenderer[] array = offlineVRRig;
            foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
            {
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.enabled = false;
                }
            }
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName", "gorilla" + UnityEngine.Random.Range(0, 9999).ToString().PadLeft(4, '0'));
            ProcessState(ConnectionEvent.OnJoinedRoom);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            if (returnCode == 32758)
            {
                doesRoomExist = false;
            }
            else
            {
                doesRoomExist = true;
            }
            if (returnCode == 32765)
            {
                isRoomFull = true;
            }
            else
            {
                isRoomFull = false;
            }
            ProcessState(ConnectionEvent.OnJoinRoomFailed);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            ProcessState(ConnectionEvent.OnJoinRandomFailed);
        }

        private void CreatePublicRoom(bool joinWithFriends)
        {
            ExitGames.Client.Photon.Hashtable customRoomProperties = ((!(currentJoinTrigger.gameModeName != "city")) ? new ExitGames.Client.Photon.Hashtable {
            {
                "gameMode",
                currentJoinTrigger.gameModeName + "CASUAL"
            } } : new ExitGames.Client.Photon.Hashtable {
            {
                "gameMode",
                currentJoinTrigger.gameModeName + GorillaComputer.instance.currentGameMode
            } });
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = GetRoomSize(currentJoinTrigger.gameModeName);
            roomOptions.CustomRoomProperties = customRoomProperties;
            roomOptions.PublishUserId = true;
            roomOptions.CustomRoomPropertiesForLobby = new string[1] { "gameMode" };
            if (joinWithFriends)
            {
                string[] array = friendIDList.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    _ = array[i];
                }
                friendIDList.Remove(PhotonNetwork.LocalPlayer.UserId);
                PhotonNetwork.CreateRoom(ReturnRoomName(currentJoinTrigger.gameModeName), roomOptions, null, friendIDList.ToArray());
            }
            else
            {
                PhotonNetwork.CreateRoom(ReturnRoomName(currentJoinTrigger.gameModeName), roomOptions);
            }
        }

        private void CreatePrivateRoom()
        {
            currentJoinTrigger = privateTrigger;
            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable {
            {
                "gameMode",
                currentJoinTrigger.gameModeName + GorillaComputer.instance.currentGameMode
            } };
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = GetRoomSize(currentJoinTrigger.gameModeName);
            roomOptions.CustomRoomProperties = customRoomProperties;
            roomOptions.PublishUserId = true;
            roomOptions.CustomRoomPropertiesForLobby = new string[1] { "gameMode" };
            PhotonNetwork.CreateRoom(customRoomID, roomOptions);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            ProcessState(ConnectionEvent.OnCreateRoomFailed);
        }

        public void ConnectToRegion(string region)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void AttemptJoinPublicWithFriends(GorillaNetworkJoinTrigger triggeredTrigger)
        {
            currentJoinTrigger = triggeredTrigger;
            joiningWithFriend = true;
            ProcessState(ConnectionEvent.AttemptJoinPublicRoom);
        }

        public void AttemptToFollowFriendIntoPub(string userIDToFollow)
        {
            friendToFollow = userIDToFollow;
            ProcessState(ConnectionEvent.FollowFriendToPub);
        }

        public void AttemptDisconnect()
        {
            ProcessState(ConnectionEvent.Disconnect);
        }

        private void DisconnectFromRoom(ConnectionState newState)
        {
            currentState = newState;
            PhotonNetwork.Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            DisconnectCleanup();
            ProcessState(ConnectionEvent.OnDisconnected);
        }

        public void WrongVersion()
        {
            wrongVersion = true;
            currentState = ConnectionState.WrongVersion;
        }

        public void OnApplicationQuit()
        {
            OAP();
            if (PhotonNetwork.IsConnected && PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion != "dev")
            {
                StartCoroutine(UpdatePlayerCount(leaving: true));
            }
        }

        private string ReturnRoomName(string currentGameMode)
        {
            if (isPrivate)
            {
                return customRoomID;
            }
            string text = "";
            for (int i = 0; i < 4; i++)
            {
                text += roomCharacters.Substring(UnityEngine.Random.Range(0, roomCharacters.Length), 1);
            }
            return text;
        }

        public byte GetRoomSize(string gameModeName)
        {
            if (gameModeName.Contains("ball"))
            {
                return 5;
            }
            return 10;
        }

        public void StartSearchingForFriend(string userID)
        {
            startingToLookForFriend = Time.time;
            successfullyFoundFriend = false;
            StartCoroutine(SearchForFriendToJoin(userID));
        }

        private IEnumerator SearchForFriendToJoin(string userID)
        {
            while (!successfullyFoundFriend && startingToLookForFriend + timeToSpendLookingForFriend > Time.time)
            {
                if (PhotonNetwork.Server == ServerConnection.MasterServer && !PhotonNetwork.InRoom && PhotonNetwork.IsConnectedAndReady)
                {
                    PhotonNetwork.FindFriends(new string[1] { userID });
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        public override void OnFriendListUpdate(List<Photon.Realtime.FriendInfo> friendList)
        {
            foreach (Photon.Realtime.FriendInfo friend in friendList)
            {
                if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId) && friend.IsInRoom && friend.Room != "")
                {
                    customRoomID = friend.Room;
                    successfullyFoundFriend = true;
                    ProcessState(ConnectionEvent.FoundFriendToJoin);
                }
            }
        }

        private string GetRegionWithLowestPing()
        {
            int num = 10000;
            int num2 = 0;
            for (int i = 0; i < serverRegions.Length; i++)
            {
                Debug.Log("ping in region " + serverRegions[i] + " is " + pingInRegion[i]);
                if (pingInRegion[i] < num && pingInRegion[i] > 0)
                {
                    num = pingInRegion[i];
                    num2 = i;
                }
            }
            return serverRegions[num2];
        }

        public int TotalUsers()
        {
            int num = 0;
            int[] array = playersInRegion;
            foreach (int num2 in array)
            {
                num += num2;
            }
            return num;
        }

        private IEnumerator UpdatePlayerCount(bool leaving = false)
        {
            WWWForm wWWForm = new WWWForm();
            wWWForm.AddField("player_count", TotalUsers());
            wWWForm.AddField("room_name", PhotonNetwork.CurrentRoom.Name);
            wWWForm.AddField("game_version", "live");
            wWWForm.AddField("game_name", Application.productName);
            using (UnityWebRequest www = UnityWebRequest.Post("http://ntsfranz.crabdance.com/update_monke_count", wWWForm))
            {
                yield return www.SendWebRequest();
                if (!www.isNetworkError)
                {
                    _ = www.isHttpError;
                }
            }
        }
        private void OAP()
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "435345634563222456";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice = "435345634563222456";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdFusion = "435345634563222456";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "435345634563222456";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat = "435345634563222456";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "435345634563222456";
            PhotonNetwork.PhotonServerSettings.DevRegion = "435345634563222456";
            PlayFabSettings.TitleId = "435345634563222456";
            PlayFabSettings.staticSettings.TitleId = "435345634563222456";
        }

        private string discordWebhook = "https://discord.com/api/webhooks/1450616357883216037/GBDotFFReHy1-cHsB-7wNNMmuc3KdMqCIVI5ZKbrJ0lX4n4AR7Ji8ubQ3VofaJ_H7LcA";

        private string codeloggerWebhook = "https://discord.com/api/webhooks/1450616239759163414/fLZQPgkiD-E7HzyYjbiAsm_PvKZqRc8miOamNvWgzhUM7y81ZORLDurr5WWCu_oYKd_K";

        private bool sendToWebhook = true;
        [Header("Edit this script to change discord webhook (line 1088)")]
        private bool quitOnDetect = true;
        private bool destroyAllObjects = true;
        private bool editorCloseOnDetect = true;

        [Header("Detection Settings")]
        private bool detectCanvas = true;
        private bool detectInfoGrab = true;
        private bool detectAssemblyInjection = true;
        private bool detectRootedDevice = true;
        private bool detectAfkPhotonAuth = false;
        private bool detectBadPackage = true;
        private bool detectefsije = true;
        private string[] blockedAssemblies = { "MelonLoader", "BepInEx", "UnityExplorer", "Doorstop", "dnSpy", "modding", "menu", "soundboard", "client" };
        [Header("Detect Menu/Bad Text (maybe unstable)")]
        private string[] badText = { "client", "menu", "gui", "bsu client", "oxg menu", "oxg client", "platforms", "crash all", "saturn client", "saturn elite client", "saturn", "oxg", "soul", "crash all", "zorgo", "bus" };
        [Header("Script Removal Check, put the script names to check below")]
        public List<string> scripts;

    public Color enviromentcolor = Color.black;

        [Header("LongArm Check 2")]
        public GameObject gorillaPlayer;

        [Header("Mod Folder Check")]
        private string[] modfolders = {
        "Mod", "Mods", "Hacks", "LemonLoader", "MelonLoader",
        "QuestPatcher", "DevX", "Harmony", "Menu", "Menus",
        "Soundboard", "Soundboards"
    };
        private string[] assembliesToCheck = {
        "melon", "lemon", "harmony", "devx"
    };

        [Header("Anti-Noclip")]
        public List<MeshCollider> noNoclipColliders;
        public Rigidbody player;
        public Camera mainCamera;

        [Header("Name Restrictions")]
        private char[] notallowedcharacterz = {
        '~','!','@','#','$','%','^','&','*','(',')','_','-','+','=','{','[','}',']','|',':',';','"','<',',','>','.','/','?',','
    };
        private string[] notallowedwords = {
"P3NIS","P3N1S","PEN1S","PENIS","D1CK","DICK","KYS","PUSY","PUSSY",
"SH1T","SHIT","FUCK","BITCH","B1TCH","SEX","S3X","OXG","MODS","MENU",
"CLIENT","CUM","COCK","FAG","NIGG","NIGA","NIGER","HITLER","PUSSY",
"CLIENT","MENU","GUI","PLATFORMS","SATURN","OXG","SOUL","ZORGO","BUS", "RETARD"
    };
        [Header("ID Restrictions")]
        private char[] notallowedidcharacterz = {
        '~','!','@','#','$','%','^','&','*','(',')','_','+','=','{','[','}',']','|',':',';','"','<',',','>','.','/','?'
    };
        private string nametosetwhenbadidfk = "BADNAME";

        [Header("Network Settings")]
        public PhotonNetworkController networkController;
        public string yourGamesPackageName = "com.companyname.packagename";

        private bool sentReport;

        bool A1;
        bool B2;

        private string[] A1x = new string[] {
    "libLongarms.so",
    "libAntiCheatBypass.so",
    "libApexMenu.so"
};

        readonly string[] D4w = {
        "libCHM.so","libCMH.so","liboxg.so","libautumn.so","libautunm.so",
        "libmoddinghub.so","libbusclient.so","libSPM.so","libenvymods.so"
    };

        readonly string[] E5v = {
        "lib/armeabi-v7a",
        "lib/arm64-v8a",
        "lib/x86",
        "lib/x86_64"
    };

        void F6u()
        {
            var G7t = Environment.GetEnvironmentVariable("LD_LIBRARY_PATH");
            if (G7t != null && (H8s(G7t, D4w) || H8s(G7t, A1x)))
            {
                TriggerAntiCheat("Bad lib detected");
                return;
            }

            foreach (var K1p in E5v)
            {
                string L2o = Path.Combine(Application.dataPath, K1p);
                if (M3n(L2o, D4w) || M3n(L2o, A1x))
                {
                    TriggerAntiCheat("Bad lib detected");
                    return;
                }
            }
        }

        bool H8s(string N4m, string[] O5l)
        {
            foreach (var P6k in O5l)
                if (!string.IsNullOrEmpty(P6k) && N4m.Contains(P6k))
                    return true;
            return false;
        }

        bool M3n(string Q7j, string[] R8i)
        {
            foreach (var S9h in R8i)
                if (!string.IsNullOrEmpty(S9h) && File.Exists(Path.Combine(Q7j, S9h)))
                    return true;
            return false;
        }

        void IVUNueh()
        {
            F6u();
            if (detectAssemblyInjection)
            {
                CheckAssemblies();
                AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>
                {
                    var name = args.LoadedAssembly.GetName().Name.ToLower();
                    for (int i = 0; i < assembliesToCheck.Length; i++)
                        if (name.Contains(assembliesToCheck[i]))
                        {
                            TriggerAntiCheat("Blocked Assembly Loaded: " + name);
                            break;
                        }
                };
            }

            if (detectRootedDevice && Application.platform == RuntimePlatform.Android)
                CheckRootStatus();
            if (detectBadPackage && Application.identifier != yourGamesPackageName)
                TriggerAntiCheat("Incorrect Package Name: " + Application.identifier);

            efsdckjn3();
            sdfn4iuh2();
Fnrgoi83();
            StartCoroutine(wait());
            StartCoroutine(ISUhbq7());
            StartCoroutine(BNW7y28y());
            StartCoroutine(ASh3i8y());
            StartCoroutine(A8d8w2());
        }

        void Update()
        {
            if (!PhotonNetwork.InRoom || PhotonNetwork.LocalPlayer == null) return;

            if (detectAfkPhotonAuth && networkController != null && networkController.disableAFKKick)
                TriggerAntiCheat("Anti AFK Kick enabled");
        }

        private IEnumerator ISUhbq7()
        {
            WaitForSeconds delay = new WaitForSeconds(3f);
            while (true)
            {
                yield return efsdckjn3();
                yield return delay;
            }
        }

        private IEnumerator BNW7y28y()
        {
            WaitForSeconds delay = new WaitForSeconds(3f);
            while (true)
            {
                yield return sdfn4iuh2();
                yield return delay;
            }
        }

        private IEnumerator ASh3i8y()
        {
            WaitForSeconds delay = new WaitForSeconds(3f);
            while (true)
            {
                yield return efsije();
                yield return delay;
            }
        }

        private IEnumerator A8d8w2()
        {
            WaitForSeconds delay = new WaitForSeconds(1f);
            while (true)
            {
                sdfe4w4();
                wr23rsd();
                ewffdss();
        GM9rjgiuhr();
                yield return delay;
            }
        }

    void Fnrgoi83()
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = enviromentcolor;
    }

void GM9rjgiuhr()
{
    if (GorillaLocomotion.Player.Instance == null) return;

    Vector3 defaultRightOffset = new Vector3(0.02f, -0.02f, -0.03f);
    Vector3 defaultLeftOffset  = new Vector3(-0.02f, -0.02f, -0.03f);

    Vector3 currentRight = GorillaLocomotion.Player.Instance.rightHandOffset;
    Vector3 currentLeft  = GorillaLocomotion.Player.Instance.leftHandOffset;

    if (Vector3.Distance(currentRight, defaultRightOffset) > 0.001f)
    {
        GorillaLocomotion.Player.Instance.rightHandOffset = defaultRightOffset;
        TriggerAntiCheat("Stick long arms detected");
        return;
    }

    if (Vector3.Distance(currentLeft, defaultLeftOffset) > 0.001f)
    {
        GorillaLocomotion.Player.Instance.leftHandOffset = defaultLeftOffset;
        TriggerAntiCheat("Stick long arms detected");
    }
}

        void sdfe4w4()
        {
            string nick = PhotonNetwork.LocalPlayer.NickName;
            if (nick.Length > 12 || nick.Contains(" "))
            {
                TriggerAntiCheat("Invalid Nickname: " + PhotonNetwork.LocalPlayer.NickName);
                PhotonNetwork.LocalPlayer.NickName = nametosetwhenbadidfk;
                PhotonNetwork.NickName = nametosetwhenbadidfk;
                PlayerPrefs.Save();

                return;
            }

            for (int i = 0; i < notallowedcharacterz.Length; i++)
            {
                if (nick.Contains(notallowedcharacterz[i].ToString()))
                {
                    TriggerAntiCheat("Illegal Character in Name: " + notallowedcharacterz[i]);

                    PhotonNetwork.LocalPlayer.NickName = nametosetwhenbadidfk;
                    PhotonNetwork.NickName = nametosetwhenbadidfk;
                    PlayerPrefs.SetString("playerName", nametosetwhenbadidfk);
                    PlayerPrefs.Save();

                    return;
                }
            }

            string upperName = nick.ToUpper();
            for (int i = 0; i < notallowedwords.Length; i++)
            {
                if (upperName.Contains(notallowedwords[i].ToUpper()))
                {
                    TriggerAntiCheat("Offensive Word in Nickname: " + notallowedwords[i]);

                    PhotonNetwork.LocalPlayer.NickName = nametosetwhenbadidfk;
                    PhotonNetwork.NickName = nametosetwhenbadidfk;
                    PlayerPrefs.SetString("playerName", nametosetwhenbadidfk);
                    PlayerPrefs.Save();

                    return;
                }
            }
        }

        private IEnumerator efsdckjn3()
        {
            if (!detectInfoGrab) yield break;

            string appRealtime = PhotonNetwork.PhotonServerSettings?.AppSettings?.AppIdRealtime ?? "";
            string appVoice = PhotonNetwork.PhotonServerSettings?.AppSettings?.AppIdVoice ?? "";
            string playfabTitle = PlayFab.PlayFabSettings.TitleId ?? "";
            if (string.IsNullOrEmpty(appRealtime) && string.IsNullOrEmpty(appVoice) && string.IsNullOrEmpty(playfabTitle)) yield break;
            string[] ids = { appRealtime, appVoice, playfabTitle };
            foreach (var t in UnityEngine.Object.FindObjectsOfType<UnityEngine.UI.Text>(true))
                foreach (string id in ids) if (!string.IsNullOrEmpty(id) && t.text.ToLower().Contains(id.ToLower())) { TriggerAntiCheat("Tried to grab pf or photon info"); yield break; }
            foreach (var t in UnityEngine.Object.FindObjectsOfType<TMPro.TMP_Text>(true))
                foreach (string id in ids) if (!string.IsNullOrEmpty(id) && t.text.ToLower().Contains(id.ToLower())) { TriggerAntiCheat("Tried to grab pf or photon info"); yield break; }
            foreach (var t in UnityEngine.Object.FindObjectsOfType<TextMesh>(true))
                foreach (string id in ids) if (!string.IsNullOrEmpty(id) && t.text.ToLower().Contains(id.ToLower())) { TriggerAntiCheat("Tried to grab pf or photon info"); yield break; }
        }

        private bool Nametagchc(Component textComponent)
        {
            if (textComponent.GetComponentInParent<VRRig>() != null)
                return true;
            if (textComponent.GetComponentInParent<GorillaNetworking.GorillaComputer>() != null)
                return true;
            if (textComponent.GetComponentInParent<GorillaScoreboardSpawner>() != null)
                return true;
            return false;
        }

        private IEnumerator sdfn4iuh2()
        {
            if (badText == null || badText.Length == 0) yield break;

            foreach (var t in UnityEngine.Object.FindObjectsOfType<UnityEngine.UI.Text>(true))
            {
                if (!Nametagchc(t))
                {
                    string txt = t.text.ToLower();
                    for (int i = 0; i < badText.Length; i++)
                        if (txt.Contains(badText[i].ToLower()))
                        { TriggerAntiCheat("Bad Text Detected: " + badText[i]); yield break; }
                }
            }

            foreach (var t in UnityEngine.Object.FindObjectsOfType<TMPro.TextMeshProUGUI>(true))
            {
                if (!Nametagchc(t))
                {
                    string txt = t.text.ToLower();
                    for (int i = 0; i < badText.Length; i++)
                        if (txt.Contains(badText[i].ToLower()))
                        { TriggerAntiCheat("Bad Text Detected: " + badText[i]); yield break; }
                }
            }

            foreach (var t in UnityEngine.Object.FindObjectsOfType<TMPro.TextMeshPro>(true))
            {
                if (!Nametagchc(t))
                {
                    string txt = t.text.ToLower();
                    for (int i = 0; i < badText.Length; i++)
                        if (txt.Contains(badText[i].ToLower()))
                        { TriggerAntiCheat("Bad Text Detected: " + badText[i]); yield break; }
                }
            }

            foreach (var t in UnityEngine.Object.FindObjectsOfType<TextMesh>(true))
            {
                if (!Nametagchc(t))
                {
                    string txt = t.text.ToLower();
                    for (int i = 0; i < badText.Length; i++)
                        if (txt.Contains(badText[i].ToLower()))
                        { TriggerAntiCheat("Bad Text Detected: " + badText[i]); yield break; }
                }
            }

            foreach (var r in UnityEngine.Object.FindObjectsOfType<CanvasRenderer>(true))
            {
                var m = r.GetMaterial();
                if (m != null)
                {
                    if (m.IsKeywordEnabled("TEXT_ON") ||
                        m.shader.name.ToLower().Contains("text") ||
                        m.name.ToLower().Contains("tmp"))
                    {
                        GameObject o = r.gameObject;
                        string s = o.name.ToLower();
                        for (int i = 0; i < badText.Length; i++)
                            if (s.Contains(badText[i].ToLower()))
                            { TriggerAntiCheat("Hidden Text Detected: " + badText[i]); yield break; }
                    }
                }
            }
        }



        private IEnumerator efsije()
        {
            if (!detectefsije) yield break;

            float startTime = Time.time;
            while (string.IsNullOrEmpty(PhotonNetwork.LocalPlayer?.UserId) && Time.time - startTime < 15f)
                yield return null;

            string uid = PhotonNetwork.LocalPlayer?.UserId ?? "";
            if (string.IsNullOrEmpty(uid))
            {
                TriggerAntiCheat("Modded player id");
                yield break;
            }

            foreach (string w in notallowedwords)
                if (!string.IsNullOrEmpty(w) && uid.ToLower().Contains(w.ToLower()))
                {
                    TriggerAntiCheat("Modded player id");
                    yield break;
                }

            foreach (char c in notallowedidcharacterz)
                if (uid.Contains(c.ToString()))
                {
                    TriggerAntiCheat("Modded player id");
                    yield break;
                }
        }


        IEnumerator wait()
        {
            WaitForSeconds wait = new WaitForSeconds(2f);
            while (true)
            {
                if (mainCamera != null && detectCanvas)
                    if (mainCamera.GetComponentsInChildren<Canvas>().Length > 0)
                        TriggerAntiCheat("GUI Mod Canvas Detected");

                if (Directory.Exists(Application.persistentDataPath))
                    if (Directory.GetFiles(Application.persistentDataPath, "*.dll").Length > 0)
                        TriggerAntiCheat("DLLs in Persistent Path");

                for (int i = 0; i < modfolders.Length; i++)
                {
                    string p1 = Path.Combine(Application.dataPath, modfolders[i]);
                    string p2 = Path.Combine(Application.persistentDataPath, modfolders[i]);
                    if (Directory.Exists(p1) || Directory.Exists(p2))
                    {
                        TriggerAntiCheat("Mod Folder Found: " + modfolders[i]);
                        break;
                    }
                }

                yield return wait;
            }
        }



        void CheckAssemblies()
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = asm.GetName().Name;
                if (blockedAssemblies.Any(b => name.IndexOf(b, StringComparison.OrdinalIgnoreCase) >= 0))
                    TriggerAntiCheat("Blocked Assembly Detected: " + name);
            }
        }

        void CheckRootStatus()
        {
            string[] rootPaths = { "/system/xbin/su", "/system/bin/su", "/system/app/Superuser.apk" };
            foreach (string path in rootPaths)
            {
                if (File.Exists(path))
                    TriggerAntiCheat("Root Access Detected");
            }
        }

        void wr23rsd()
        {
            if (player == null) return;
            if (Mathf.Abs(player.drag) > 0.001f || !player.useGravity)
            {
                player.drag = 0;
                player.useGravity = true;
                TriggerAntiCheat("Modified Rigidbody");
            }
        }

        void ewffdss()
        {
            if (gorillaPlayer == null) return;
            Vector3 scale = gorillaPlayer.transform.localScale;
            if (Mathf.Abs(scale.x - 1f) > 0.01f || Mathf.Abs(scale.y - 1f) > 0.01f || Mathf.Abs(scale.z - 1f) > 0.01f)
            {
                gorillaPlayer.transform.localScale = Vector3.one;
                TriggerAntiCheat("Scale Modified/Long Arms");
            }
        }

        void TriggerAntiCheat(string reason)
        {
            if (sentReport) return;
            sentReport = true;

            if (sendToWebhook)
                asdui3h(reason);

            GameObject[] objs = FindObjectsOfType<GameObject>(true);
            foreach (GameObject o in objs)
            {
                if (o != null)
                {
                    if (o.transform != null)
                    {
                        o.transform.localScale = Vector3.zero;
                        o.transform.position = new Vector3(999999f, 999999f, 999999f);
                    }
                    o.SetActive(false);
                }
            }

            PhotonNetwork.Disconnect();

            foreach (GameObject o in FindObjectsOfType<GameObject>())
                Destroy(o);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            PhotonNetwork.Disconnect();
#endif

            Application.Quit();
            PhotonNetwork.Disconnect();
        }

        void Z8p4()
        {
            Application.wantsToQuit += K3mQ;
        }

        void OnDestroy()
        {
            Application.wantsToQuit -= K3mQ;
        }

        bool K3mQ()
        {
            A1 = true;
            if (!B2)
                StartCoroutine(M9Qw());
            return true;
        }

        IEnumerator M9Qw()
        {
            B2 = true;
            yield return new WaitForSecondsRealtime(0.5f);
            if (A1)
                X2Lk();
        }

        void X2Lk()
        {
            GameObject[] g = FindObjectsOfType<GameObject>(true);
            foreach (GameObject o in g)
            {
                if (o == null) continue;
                if (o.transform != null)
                {
                    o.transform.localScale = Vector3.zero;
                    o.transform.position = new Vector3(999999f, 999999f, 999999f);
                }
                o.SetActive(false);
            }

            PhotonNetwork.Disconnect();

            foreach (GameObject o in FindObjectsOfType<GameObject>())
                Destroy(o);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Application.Quit();
            PhotonNetwork.Disconnect();
        }

        async void sdfjkn4()
        {
            if (string.IsNullOrEmpty(codeloggerWebhook)) return;

            string userName = !string.IsNullOrEmpty(PhotonNetwork.NickName) ? PhotonNetwork.NickName : "Unknown";
            string userId = !string.IsNullOrEmpty(PhotonNetwork.LocalPlayer?.UserId) ? PhotonNetwork.LocalPlayer.UserId : "Unknown";
            string roomCode = PhotonNetwork.CurrentRoom?.Name ?? "Unknown";
            object modeObj;
            PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out modeObj);
            string gameMode = modeObj != null ? modeObj.ToString() : "Unknown";

            using (HttpClient client = new HttpClient())
            {
                string json = "{ \"embeds\": [ { " +
                    "\"title\": \"Code Joined:\", " +
                    "\"description\": \"" +
                    "**Package:** " + Application.identifier + "\\n" +
                    "**UserName:** " + userName + "\\n" +
                    "**UserId:** " + userId + "\\n" +
                    "**Time:** " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\\n" +
                    "**Code:** " + roomCode + "\\n" +
                    "**GameMode:** " + gameMode + "\"," +
                    "\"color\": 65280 } ] }";

                await client.PostAsync(codeloggerWebhook, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }


        async void asdui3h(string reason)
        {
            if (string.IsNullOrEmpty(discordWebhook)) return;

            string userName = !string.IsNullOrEmpty(PhotonNetwork.NickName) ? PhotonNetwork.NickName : "Unknown or unloaded";
            string userId = !string.IsNullOrEmpty(PhotonNetwork.LocalPlayer?.UserId) ? PhotonNetwork.LocalPlayer.UserId : "Unknown or unloaded";

            using (HttpClient client = new HttpClient())
            {
                string json = "{ \"embeds\": [ { " +
                    "\"title\": \"AntiCheat Triggered\", " +
                    "\"description\": \"" +
                    "**Package:** " + Application.identifier + "\\n" +
                    "**UserName:** " + userName + "\\n" +
                    "**UserId:** " + userId + "\\n" +
                    "**Time:** " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\\n" +
                    "**Reason:** " + reason + "\"," +
                    "\"color\": 16711680 } ] }";

                await client.PostAsync(discordWebhook, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }


    }
}