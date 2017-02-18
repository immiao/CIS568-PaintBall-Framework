using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour
{
 
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    // TODO-2.a: the same as 1.b
    //   and join a room
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");

    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joinedttttttttttttttttttttttttttttttttttttttttttttttt");
        GetComponent<MobileShooter>().Activate();
    }
    public override void OnJoinedLobby()
    {
        //PhotonNetwork.CreateRoom(null);
        if (PhotonNetwork.JoinRandomRoom())
            Debug.Log("Join room succeededyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
        else
            Debug.Log("Join room fails");
    }
    //public override void OnJoinedRoom()
    //{
    //    GetComponent<MobileShooter>().Activate();
    //}


}
