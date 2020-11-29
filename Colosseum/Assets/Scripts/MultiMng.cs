using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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

    public byte playerNum = 0;

    public byte PlayerNum
    {
        set
        {
            if (playerNum == value)
                return;
            ExitGames.Client.Photon.Hashtable table = PhotonNetwork.LocalPlayer.CustomProperties;
            table["playerNum"] = value;
            PhotonNetwork.SetPlayerCustomProperties(table);
            playerNum = value;
        }
        get
        {
            return playerNum;
        }
    }

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

            resetPlayerCustomProperties();

            if (PhotonNetwork.IsConnected)
            {
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            Destroy(gameObject);
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
            LoadLevelWithFade(1);
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
            LoadLevelWithFade(0);
    }
    public void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        Destroy(gameObject);
    }
    public void JoinLoby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void JoinRoom()
    {
        StartCoroutine("JoinRoomTry");
    }

    private IEnumerator JoinRoomTry()
    {

        while (!PhotonNetwork.InRoom)
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
                yield return new WaitForSecondsRealtime(1f);
                continue;
            }
            if (PhotonNetwork.IsConnectedAndReady)
            {
                JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        yield break;
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
        //
    }

    public override void OnLeftRoom()
    {
        resetPlayerCustomProperties();
        LoadLevelWithFade(0);
    }

    public void resetPlayerCustomProperties()
    {
        ExitGames.Client.Photon.Hashtable table = PhotonNetwork.LocalPlayer.CustomProperties;
        table["isReady"] = false;
        table["isKicked"] = false;
        table["playerNum"] = (byte)0;
        table["isAlive"] = true;
        PhotonNetwork.SetPlayerCustomProperties(table);
    }

    public void IntoColosseum()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        LoadLevelWithFade(2);
    }

    public void LoadLevelWithFade(int num)
    {
        Debug.Log("페이드 동반 로드 씬");
        GameObject obj = Instantiate(Resources.Load("fadeCanvas", typeof(GameObject)) as GameObject, Vector3.zero,Quaternion.identity);
        obj.GetComponent<FadeInOut>().LoadScene(num);
    }

    public void LoadLevel(int num)
    {
        Debug.Log("로드 씬");
        PhotonNetwork.LoadLevel(num);
    }

    public void LeaveRoom()
    {
        resetPlayerCustomProperties();
        PhotonNetwork.LeaveRoom();
    }


}
