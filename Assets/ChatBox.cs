using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChatBox : MonoBehaviourPun
{
    public int MaxMessage = 100;

    private Vector2 chatScroll = Vector2.zero;
    private List<string> chateMessages = new List<string>();

    private string message = "";

    private void OnGUI()
    {
        if(GUILayout.Button("Level Room"))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("SampleScene");
        }

        chatScroll = GUILayout.BeginScrollView(chatScroll, GUILayout.Width(Screen.width), GUILayout.ExpandHeight(true));

        foreach(string msg in chateMessages)
        {
            GUILayout.Label(msg);
        }

        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();

        message = GUILayout.TextField(message, GUILayout.ExpandWidth(true));

        if(GUILayout.Button("Send", GUILayout.Width(100)))
        {
            photonView.RPC("AddChat", RpcTarget.All, message);
            message = "";
        }

        GUILayout.EndHorizontal();
    }

    [PunRPC]
    void AddChat(string message, PhotonMessageInfo info)
    {
        chateMessages.Add(info.Sender.NickName + " : " + message);

        if(chateMessages.Count > MaxMessage)
        {
            chateMessages.RemoveAt(0);
        }

        chatScroll.y = 10000;
    }

    
}
