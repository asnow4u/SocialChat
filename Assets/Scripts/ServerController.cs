using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ServerController : MonoBehaviourPunCallbacks
{

    void Awake()
    {
      PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
      PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
      SceneManager.LoadScene("Lobby");
    }
}
