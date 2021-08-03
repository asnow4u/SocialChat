using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WebCamController : MonoBehaviour
{

    static WebCamTexture backCam;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {

        view = transform.root.GetComponent<PhotonView>();

        if (view.IsMine) {

          if (backCam == null)
          {
            backCam = new WebCamTexture();
          }

          GetComponent<Renderer>().material.mainTexture = backCam;

          if (!backCam.isPlaying)
          {
            backCam.Play();
          }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
