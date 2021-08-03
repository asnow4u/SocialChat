using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerController : MonoBehaviour
{

    void Awake()
    {
      PhotonNetwork.ConnectUsingSettings("v1.0");

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnConnectedToMaster()
    {
      PhotonNetwork.JoinLobby();
      Debug.Log("Joining Lobby"); //GOOD
    }

    public void OnJoinedLobby() {
      SceneManager.LoadScene("Lobby");
      Debug.Log("Joined Lobby"); //GOOD
    }
}
