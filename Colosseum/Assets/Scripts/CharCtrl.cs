﻿using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class CharCtrl : MonoBehaviourPun
{
    [SerializeField]
    private CharacterController myCharCtrl = null;
    [SerializeField]
    private Animator myAni = null;
    [SerializeField]
    private PhotonView myView = null;

    [SerializeField]
    private Transform nameTagTransform = null;

    [SerializeField]
    private GameObject NameTag = null;

    public void Init()
    {
        gameObject.name = myView.Owner.NickName;
        GameObject inst = Instantiate(NameTag);
        inst.transform.SetParent(GameMng.instance.WorldCanvas);
        nameTagTransform = inst.transform;
        inst.transform.Find("Text").GetComponent<Text>().text = gameObject.name;
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (myView.Owner != PhotonNetwork.LocalPlayer)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        myAni.SetFloat("ForwardSpeed" , v);
        myAni.SetFloat("RightSpeed", h);
        Vector3 moveDirection = transform.TransformDirection(new Vector3(h, 0, v));
        moveDirection *= 10f;
        myCharCtrl.Move(moveDirection * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            //트리거
            myAni.SetTrigger("Attack");
        }
        if (Input.GetMouseButtonDown(1))
        {
            //트리거
            myAni.SetTrigger("Defend");
        }
    }

    private void LateUpdate()
    {
        nameTagTransform.position = transform.position + new Vector3(0, 8f, 0);
    }

    private void OnDestroy()
    {
        if (nameTagTransform != null)
        {
            Destroy(nameTagTransform.gameObject);
        }
    }
}
