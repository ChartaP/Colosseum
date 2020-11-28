using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameMng : MonoBehaviour
{
    public static GameMng instance;
    [SerializeField]
    private GameObject playerCharacter = null;
    [SerializeField]
    private Transform worldCanvas = null;
    [SerializeField]
    private int nRoomSize = 0;
    [SerializeField]
    private int nStartRoomsize = 0;

    public ResultPanel resultPanel;

    public int RoomSize { get { return nRoomSize; } }

    public int StartRoomSize { get { return nStartRoomsize; } }

    public int CurRank
    {
        get
        {
            int rank = 0;
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                if ((bool)player.CustomProperties["isAlive"])
                {
                    rank += 1;
                }
            }

            return rank;
        }
    }

    public Transform WorldCanvas { get { return worldCanvas; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        nRoomSize = PhotonNetwork.CurrentRoom.PlayerCount;
        nStartRoomsize = nRoomSize;
        ExitGames.Client.Photon.Hashtable table = PhotonNetwork.LocalPlayer.CustomProperties;
        byte num = (byte)table["playerNum"];
        Vector3 StartPoint = new Vector3(0,0,0);
        Quaternion StartDir = Quaternion.identity;
        switch (num)
        {
            case 0:
                StartPoint = new Vector3(0, 0, -24f);
                StartDir = Quaternion.Euler(0,0,0);
                break;
            case 1:
                StartPoint = new Vector3(-24f, 0, 0);
                StartDir = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                StartPoint = new Vector3(0, 0, 24f);
                StartDir = Quaternion.Euler(0, 180, 0);
                break;
            case 3:
                StartPoint = new Vector3(24f, 0, 0);
                StartDir = Quaternion.Euler(0, 270, 0);
                break;
        }
        GameObject inst = PhotonNetwork.Instantiate(playerCharacter.name, StartPoint, StartDir, 0);
        GameCamera.SetTarget(inst);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Win()
    {
        if (CurRank == 1)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["isAlive"] == true)
            {
                resultPanel.SetPanel(CurRank);
            }
        }
    }

    public void Retire()
    {
        resultPanel.SetPanel(CurRank+1);
    }

    public void LeaveGame()
    {
        MultiMng.instance.LeaveRoom();
    }
   
}
