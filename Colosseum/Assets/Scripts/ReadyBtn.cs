using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ReadyBtn : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Image img;
    [SerializeField]
    private Button btn;

    [SerializeField]
    private bool isReady = false;

    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            btn.interactable = false;
            return;
        }
        if (PhotonNetwork.IsMasterClient)
        {
            isReady = false;
            if (isAllRaedy())
            {
                text.text = "GameStart";
                btn.interactable = true;
            }
            else
            {
                text.text = "Waiting onther Players";
                btn.interactable = false;
            }
        }
        else
        {
            btn.interactable = true;

            if (isReady)
            {
                text.text = "Wait";
            }
            else
            {
                text.text = "Ready";
            }
        }
    }

    public bool isAllRaedy()
    {
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            ExitGames.Client.Photon.Hashtable table = player.CustomProperties;
            if (!(bool)table["isReady"])
            {
                return false;
            }
        }
        return true;
    }

    public void OnClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LobyMng.instance.GameStart();
        }
        else
        {
            ExitGames.Client.Photon.Hashtable table = PhotonNetwork.LocalPlayer.CustomProperties;
            table["isReady"] = !(bool)table["isReady"];
            PhotonNetwork.SetPlayerCustomProperties(table);
            if ((bool)table["isReady"])
            {
                text.text = "Ready";
                isReady = true;
            }
            else
            {
                text.text = "Ready";
                isReady = false;
            }
        }
    }
}
