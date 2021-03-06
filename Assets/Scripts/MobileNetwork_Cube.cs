﻿using UnityEngine;
public class MobileNetwork_Cube : Photon.PunBehaviour
{
    // TODO-1.b: write any functions needed to establish connection
    //   and join a room. Joining a random room will do for now if you are testing
    //   it yourself. But you can also list the rooms or require player to enter
    //   the room name in case there are more people playing
    //   your game - though it is not required for the assignment.

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void Start()
    {
        // Make sure "Auto-Join Lobby" was checked at 
        //   Assets-> Photon Unity Networking-> PhotonServerSettings
        //   so the application will automatically connect to Lobby
        //   and call OnJoinedLobby()
        PhotonNetwork.ConnectUsingSettings("0.1");
        //roomName = GenerateRoomName();
        //if (PhotonNetwork.JoinRoom("abcd"))

    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        //TODO-1.c: use PhotonNetwork.Instantiate to create a "PhoneCube" across the network
        var cube = PhotonNetwork.Instantiate("PhoneCube", new Vector3(0, 0, 0), new Quaternion(), 0);
        GetComponent<GyroController>().ControlledObject = cube;
    }
    public override void OnJoinedLobby()
    {
        //PhotonNetwork.CreateRoom(null);
        if (PhotonNetwork.JoinRandomRoom())
            Debug.Log("Join room succeeded");
        else
            Debug.Log("Join room fails");
    }

}
