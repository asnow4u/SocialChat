using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class RoomController : MonoBehaviourPunCallbacks
{

    public GameObject createInput;
    public GameObject joinRoom;

    public void CreateRoom()
    {
      PhotonNetwork.CreateRoom(createInput.GetComponent<TMP_InputField>().text);
    }

    public void JoinRoom() {
      PhotonNetwork.JoinRoom(joinRoom.GetComponent<TMP_InputField>().text);
    }


    public override void OnJoinedRoom()
    {
      PhotonNetwork.LoadLevel("SampleScene");
    }
}
