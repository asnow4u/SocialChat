using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AgoraVideoChat : MonoBehaviour
{

    [SerializeField] private string appID;
    [SerializeField] private string channel = "unity3d";

    private IRtcEngine mRtcEngine = null;

    private PhotonView view;
    private uint myUid;

    [SerializeField] private GameObject userVideoSurface;


    // Start is called before the first frame update
    void Start()
    {
        //Set up Agora
        view = GetComponent<PhotonView>();

        if (!view.IsMine) {
          return;
        }

        if (mRtcEngine != null)
        {
            Debug.Log("Engine exists. Please unload it first!");
            return;
        }

        mRtcEngine = IRtcEngine.GetEngine(appID);

        //Join Channel
        mRtcEngine.EnableVideo();
        mRtcEngine.EnableVideoObserver();

        //Callback events
        mRtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccessHandler;
        mRtcEngine.OnUserJoined += OnUserJoinedHandler;
        mRtcEngine.OnLeaveChannel += OnLeaveChannelHandler;
        mRtcEngine.OnUserOffline += OnUserOfflineHandler;


        mRtcEngine.JoinChannel(channel, null, 0);

    }


    private void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        Debug.Log("jointed channel");

        if (!view.IsMine) {
          return;
        }

        myUid = uid;

        Debug.Log("OnJoinChannelSuccessHandler");
        CreateUserVideoSurface(uid);
    }

    private void OnUserJoinedHandler(uint uid, int elapsed)
    {
        if (!view.IsMine) {
          return;
        }

        Debug.Log("OnUserJoinedHandler");
        CreateUserVideoSurface(uid);
    }

    private void OnLeaveChannelHandler(RtcStats stats)
    {
        if (!view.IsMine) {
          return;
        }

        Debug.Log("destroy video object");
        //destroy video object
    }

    private void OnUserOfflineHandler(uint uid, USER_OFFLINE_REASON reason)
    {
        if (!view.IsMine) {
          return;
        }

        Debug.Log("destroy video object");
        //Destroy video object

    }


    private void CreateUserVideoSurface(uint uid)
    {
       userVideoSurface.AddComponent<VideoSurface>();
       userVideoSurface.transform.eulerAngles = new Vector3(-90f, 180f, 0f); //-90, 180, 0 Start at 90, 0, 0

       Debug.Log("Create use video");
    }

    private void ClearUserVideoSurface()
    {
       userVideoSurface.GetComponent<VideoSurface>().SetEnable(false);
    }

    private void TerminateAgoraEngine()
    {
      Debug.Log("Leaving Channel");

      if (mRtcEngine != null) {
        mRtcEngine.LeaveChannel();
        mRtcEngine = null;
        IRtcEngine.Destroy();
      }
    }

    private void OnApplicationQuit()
    {
      TerminateAgoraEngine();
    }
}
