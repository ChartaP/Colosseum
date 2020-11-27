using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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
    private static string gameVersion = "1";
    #endregion

    public static bool isConnected
    {
        get
        {
            return PhotonNetwork.IsConnected;
        }
    }

    public static bool isInRoom
    {
        get
        {
            return true;
        }
    }

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

    public static string GetPlayerName()
    {
        return PhotonNetwork.NickName;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            PhotonNetwork.AutomaticallySyncScene = true;

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
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectLoby()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("로비 입장");
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
        if(SceneManager.GetActiveScene().buildIndex!=0)
            PhotonNetwork.LoadLevel(0);
    }
    public void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        Destroy(gameObject);
    }

    public void JoinRoom()
    {
        StartCoroutine("JoinRoomTry");
    }

    private IEnumerator JoinRoomTry()
    {

        while (true)
        {

            yield return new WaitForSecondsRealtime(1f);
            if (PhotonNetwork.IsConnectedAndReady)
            {
                JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
            }
            if (PhotonNetwork.InRoom)
            {
                break;
            }
        }
        yield return null;
    }

    public void JoinRandomRoom()
    {
        Debug.Log("랜덤 방에 접속 시도");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 접속에 실패 \n새로운 방 생성");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 생성에 실패 ");

        //StartCoroutine("JoinRoomTry");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방에 접속됨");
        //GameObject inst = PhotonNetwork.Instantiate(playerCharacter.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


}
