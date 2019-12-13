using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScreen : MonoBehaviourPunCallbacks
{
    Vector2 lobbyScroll = Vector2.zero;
    RoomInfo rooms;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Join Random", GUILayout.Width(200)))
        {
            PhotonNetwork.JoinRandomRoom();
        }
        if (GUILayout.Button("Create Room", GUILayout.Width(200)))
        {
            Debug.Log("Created Room");
            CreateRoom();
        }
        GUILayout.Button("Friends", GUILayout.Width(200));

        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Tried to join a random game but failed.There must be no open games available");
        CreateRoom();
    }

    private static void CreateRoom()
    {

        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room: " + randomRoomName, roomOps);
        Debug.Log("Room: " + randomRoomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are in room");
        PhotonNetwork.LoadLevel("ChatRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        CreateRoom();
    }
}
