using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;

public class Room
{
    private readonly byte maxPlayersPerRoom = 4;
    private byte curPlayersPerRoom = 4;

    public Room(byte curPlayersPerRoom)
    {
        this.curPlayersPerRoom = curPlayersPerRoom;
    }
}

public class MultiMng : MonoBehaviourPunCallbacks
{
    public static MultiMng instance;

    #region @SerializeField
    [SerializeField]
    private readonly string defaultName = "Player";
    [Tooltip("wow")]
    [SerializeField]
    private string gameVersion = "1";
    #endregion


    public void SetPlayerName(Text text)
    {
        if (string.IsNullOrEmpty(text.text))
        {
            Debug.LogError("닉네임 변경 오류 : 플레이어 이름은 공백 불가");
            return;
        }
        PhotonNetwork.NickName = text.text;
        Debug.Log("닉네임 변경 " + PhotonNetwork.NickName);
    }

    public string GetPlayerName()
    {
        return PhotonNetwork.NickName;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
            instance = this;

        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        PhotonNetwork.NickName = defaultName;

        if (PhotonNetwork.IsConnected)
        {
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("콜로세움 입장");
            PhotonNetwork.LoadLevel(1);
        }
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 연결됨");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("연결이 끊어짐 이유 : {0}", cause);
        PhotonNetwork.LoadLevel(0);
    }
    public void OnApplicationQuit()
    {
        Destroy(gameObject);
    }


}
