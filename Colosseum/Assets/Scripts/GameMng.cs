using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameMng : MonoBehaviourPunCallbacks
{
    public static GameMng instance;
    [SerializeField]
    private GameObject playerCharacter = null;
    [SerializeField]
    private Transform worldCanvas = null;

    public Transform WorldCanvas { get { return worldCanvas; } }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("JoinRoomTry");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator JoinRoomTry()
    {

        while (true)
        {

            yield return new WaitForSecondsRealtime(1f);
            if (PhotonNetwork.IsConnectedAndReady)
            {
                JoinRandomRoom();
                break;
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
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
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 생성에 실패 \n랜덤 방 찾기");

        StartCoroutine("JoinRoomTry");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방에 접속됨");
        GameObject inst = PhotonNetwork.Instantiate(playerCharacter.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnApplicationQuit()
    {
        PhotonNetwork.LeaveLobby();
        Destroy(gameObject);
    }
}
