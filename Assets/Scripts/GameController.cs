using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviour
{

    public GameObject playerPrefab;

    void Start()
    {
      PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,0,-18.5f), Quaternion.identity, 0);
    }
}
