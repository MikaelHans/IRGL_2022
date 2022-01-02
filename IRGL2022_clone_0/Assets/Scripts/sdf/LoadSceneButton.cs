using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;



namespace Unity.FPS.UI
{
    public class LoadSceneButton : MonoBehaviourPunCallbacks
    {
        RoomOptions DefaultRoomOptions;
        private byte max_player_in_room;
        private int max_dc_time;
        private string default_room_name;
        public Vector3 Initial_Location;
        public Button button;
        public string SceneName = "";

        private void Start()
        {          

            DefaultRoomOptions.MaxPlayers = max_player_in_room;//max player in room
            DefaultRoomOptions.PlayerTtl = max_dc_time;//max time for a player to dc
            DefaultRoomOptions.IsVisible = true;
            DefaultRoomOptions.IsOpen = true;
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Awake()
        {
            max_player_in_room = 20;
            max_dc_time = 300 * 1000;
            default_room_name = "IRGLROOM";
            DefaultRoomOptions = new RoomOptions();
        }        

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            JoinRoom();
        }

        #region connection codes

        //what happens when connection is established   
        public override void OnConnectedToMaster()
        {
            //base.OnConnectedToMaster();
            Debug.Log("connection established");
        }

        #region Join room functions
        public void JoinRoom(string roomName = "IRGLROOM")//called when user press to join room
        {
            PhotonNetwork.JoinRoom(roomName);//join room
        }

        public override void OnJoinRoomFailed(short returnCode, string message)//on failed to join room
        {
            Debug.Log("Failed To Join Room," + "\n" + " Error Code: " + returnCode + "\n" + "Error Message: " + message);//if failed to join for any reason, create room
            CreateNewRoom();
        }


        public override void OnJoinedRoom()//This is where we instantiate player
        {
            //base.OnJoinedRoom();
            Debug.Log("Success On Joining Room: " + PhotonNetwork.CurrentRoom.Name);
            //if(PhotonNetwork.IsMasterClient == true)//if master client load scene if regular auto sync will automatically change scene 
            //{
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //player.name = "Team: " + GameObject.FindGameObjectWithTag("Player Name").GetComponent<InputField>().text;
            //PhotonNetwork.NickName = "Team: " + GameObject.FindGameObjectWithTag("Player Name").GetComponent<InputField>().text;
            PhotonNetwork.LoadLevel(SceneName);
            //}        
        }

        #endregion

        #region create room functions
        public void CreateNewRoom()
        {
            PhotonNetwork.CreateRoom(default_room_name, DefaultRoomOptions);
        }

        public override void OnCreatedRoom()
        {
            //base.OnCreatedRoom();
            Debug.Log("Room Created Sucessfully");//after successfully created room, join the room
            JoinRoom(default_room_name);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            //base.OnCreateRoomFailed(returnCode, message);
            Debug.Log("Failed To Create Room," + "\n" + " Error Code: " + returnCode + "\n" + "Error Message: " + message);
            Debug.Log("Creating Again");
            CreateNewRoom();// if create room failed, create again until not fail
        }
        #endregion
        #endregion
    }
}