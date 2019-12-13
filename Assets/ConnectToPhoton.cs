using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class ConnectToPhoton : MonoBehaviourPun
{
    private bool connecting = false;
    private string error = null;
    private string username = "";
    public GameObject lobbyScreen;

    private void Start()
    {
        username = PlayerPrefs.GetString("Username", "");
    }

    private void OnGUI()
    {
        if(connecting)
        {
            GUILayout.Label("Connecting...");
            return;
        }

        if(error != null)
        {
            GUILayout.Label("Failed to connect: " + error);
            return;
        }

        GUILayout.Label("Username");

        username = GUILayout.TextField(username, GUILayout.Width(200));


        if (GUILayout.Button("Connect"))
        {
            connecting = true;

            PlayerPrefs.SetString("Username", username);

            PhotonNetwork.NickName = username;
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log(PhotonNetwork.NickName + " Name");
            
            connecting = false;
            gameObject.SetActive(false);
            lobbyScreen.SetActive(true);
        }
    }
    

    
}
