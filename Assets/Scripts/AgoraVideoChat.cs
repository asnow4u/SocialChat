using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AgoraVideoChat : MonoBehaviourPunCallbacks
{

    [SerializeField] private string appID;
    [SerializeField] private string channel = "unity3d";

    [SerializeField] private GameObject userParent;
    [SerializeField] private GameObject playerPrefab;
    private int numUsers;

    private IRtcEngine mRtcEngine = null;

    private uint myuid;

    // Start is called before the first frame update
    void Start()
    {
        numUsers = userParent.transform.childCount;

        //Set up Agora
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

        mRtcEngine.JoinChannel(channel, null, 0);

    }

    //Set up channel
    private void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {

        GameObject user = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,0,-18.5f), Quaternion.identity, 0);
        myuid = uid;
        user.name = myuid.ToString();
        user.transform.SetParent(userParent.transform);
        numUsers++;


        Debug.Log("OnJoinChannelSuccessHandler");
    }

    //User joins the channel
    private void OnUserJoinedHandler(uint uid, int elapsed)
    {
        Debug.Log("User Joined");
        numUsers++;

        StartCoroutine(SetUpJointVideo(uid));
    }

    //Async establish video
    private IEnumerator SetUpJointVideo(uint uid) {

      int num = userParent.transform.childCount;

      GameObject user;

      Debug.Log(myuid);
      Debug.Log(uid);

      if (num != numUsers) {

        //Check for Robot(Clone) in hyarcy and set parent to userParent
        user = GameObject.Find("Robot(Clone)");

        if (user != null) {
          user.name = uid.ToString();
          user.transform.SetParent(userParent.transform);
        }

        yield return null;
        StartCoroutine(SetUpJointVideo(uid));
      }

      else {

        user = userParent.transform.GetChild(numUsers -1).gameObject;

        Debug.Log(user);

        VideoSurface vs = user.transform.Find("Head").transform.Find("PersonView").GetComponent<VideoSurface>();
        vs.SetForUser(uid);
        vs.SetEnable(true);

        Debug.Log("OnUserJoinedHandler");
      }
    }

    //Clean up Agora
    private void TerminateAgoraEngine()
    {
        Debug.Log("Leaving Channel");

        if (mRtcEngine != null) {
          mRtcEngine.LeaveChannel();
          mRtcEngine = null;
          IRtcEngine.Destroy();
        }
      }

    //Clean up
    private void OnApplicationQuit()
    {
        TerminateAgoraEngine();
        if (mRtcEngine != null) {
          mRtcEngine.LeaveChannel();
          mRtcEngine = null;
          IRtcEngine.Destroy();
        }
    }
}
