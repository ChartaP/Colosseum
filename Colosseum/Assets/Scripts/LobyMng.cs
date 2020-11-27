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
                nameTagArr[i].SetVisible(true);
                nameTagArr[i].SetText(player.NickName);
                playerObjectArr[i].SetActive(true);
            }
            catch
            {

            }
            i++;
        }
        for (; i < 4; i++)
        {
            nameTagArr[i].SetVisible(false);
            playerObjectArr[i].SetActive(false);
        }
    }

    

    public void OnDestroy()
    {
        instance = null;
    }
}
