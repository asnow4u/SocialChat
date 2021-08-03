using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
      view = GetComponent<PhotonView>();

      if (!view.IsMine) {
        cam.enabled = false;
      }
    }

    // Update is called once per frame
    void Update()
    {

      if (view.IsMine)
      {
        transform.position += transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * speed * Time.fixedDeltaTime;
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime, 0);
      }
    }
}
