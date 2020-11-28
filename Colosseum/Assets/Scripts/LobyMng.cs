using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobyMng : MonoBehaviourPunCallbacks
{
    public static LobyMng instance;

    [SerializeField]
    private NameTag[] nameTagArr = new NameTag[4];
    [SerializeField]
    private GameObject[] playerObjectArr = new GameObject[4];
    [SerializeField]
    private Animator[] AniArr = new Animator[4];
    [SerializeField]
    private ReadyBtn readyBtn = null;

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
        for (int i =0; i < 4; i++)
        {
            nameTagArr[i].SetVisible(false);
            playerObjectArr[i].SetActive(false);
        }
        MultiMng.instance.JoinRoom();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLoby();
    }

    public void UpdateLoby()
    {
        int i = 0;
        
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            try
            {
                Debug.Log(PhotonNetwork.NickName);
                Debug.Log(i+"번 플레이어 "+ player.NickName +" 세팅");
                SetPlayerVisible(true, i);
                nameTagArr[i].SetText(player.NickName);
                if(player == PhotonNetwork.LocalPlayer)
                {
                    MultiMng.instance.PlayerNum = (byte)i;
                }
                ExitGames.Client.Photon.Hashtable table = player.CustomProperties;
                if (player == PhotonNetwork.MasterClient)
                    nameTagArr[i].SetMaster();
                else
                    nameTagArr[i].SetReady((bool)table["isReady"]);
                AniArr[i].SetBool("Ready", (bool)table["isReady"]);
            }
            catch
            {

            }
            i++;
        }
        for (; i < 4; i++)
        {
            SetPlayerVisible(false, i);
        }
    }

    private void SetPlayerVisible(bool s,int i)
    {
        if (s)
        {
            nameTagArr[i].SetVisible(true);
            playerObjectArr[i].SetActive(true);
        }
        else
        {
            nameTagArr[i].SetVisible(false);
            playerObjectArr[i].SetActive(false);
        }
    }

    public void GameStart()
    {
        MultiMng.instance.IntoColosseum();
    }

    public void OnDestroy()
    {
        instance = null;
    }
}
