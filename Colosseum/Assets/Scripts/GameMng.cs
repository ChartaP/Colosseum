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
        GameObject inst = PhotonNetwork.Instantiate(playerCharacter.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        GameCamera.SetTarget(inst);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

   
}
