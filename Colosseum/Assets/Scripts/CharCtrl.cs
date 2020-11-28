using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private float fHP = 100f;

    [SerializeField]
    private bool isDefend = false;

    [SerializeField]
    private bool isAlive = true;

    [SerializeField]
    private Attack atk = null;

    [SerializeField]
    private SkinnedMeshRenderer CharMeshRenderer;

    public void Init()
    {
        gameObject.name = myView.Owner.NickName;
        GameObject inst = Instantiate(NameTag);
        inst.transform.SetParent(GameMng.instance.WorldCanvas);
        nameTagTransform = inst.transform;
        inst.transform.Find("Text").GetComponent<Text>().text = gameObject.name;
        myAni.SetFloat("HP", fHP);
        ExitGames.Client.Photon.Hashtable table = myView.Owner.CustomProperties;
        byte num = (byte)table["playerNum"];
        Debug.Log(" 플레이어 캐릭터 Init "+myView.Owner.NickName + (byte)table["playerNum"]);
        switch (num)
        {
            case 0:
                CharMeshRenderer.material = Resources.Load("footmanHPBlue", typeof(Material)) as Material;
                break;
            case 1:
                CharMeshRenderer.material = Resources.Load("footmanHPGreen", typeof(Material)) as Material;
                break;
            case 2:
                CharMeshRenderer.material = Resources.Load("footmanHPRed", typeof(Material)) as Material;
                break;
            case 3:
                CharMeshRenderer.material = Resources.Load("footmanHPYellow", typeof(Material)) as Material;
                break;
        }
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;
        if (myView.Owner != PhotonNetwork.LocalPlayer)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Mouse X");
        myAni.SetFloat("ForwardSpeed" , v);
        myAni.SetFloat("RightSpeed", h);
        Vector3 moveDirection = transform.TransformDirection(new Vector3(h, 0, v));
        moveDirection *= 10f;
        myCharCtrl.Move(moveDirection * Time.deltaTime);
        transform.Rotate(transform.up, x);
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

    public void AttackStart()
    {
        atk.AtkStart();
    }

    public void AttackEnd()
    {
        atk.AtkEnd();
    }

    public void DefendStart()
    {
        isDefend = true;
    }

    public void DefendEnd()
    {
        isDefend = false;
    }

    public void Hit(float fDamage)
    {
        myAni.SetTrigger("Hit");
        GetDamage(fDamage);
    }

    private void GetDamage(float fDamage)
    {
        if (isDefend)
            return;
        fHP -= fDamage;
        myAni.SetFloat("HP", fHP);
        if (fHP <= 0)
            Die();
    }

    private void Die()
    {
        isAlive = false;
        myAni.SetTrigger("Die");
    }

    private void LateUpdate()
    {
        if (!isAlive)
            return;
        nameTagTransform.position = transform.position + new Vector3(0, 5f, 0);
        nameTagTransform.rotation = transform.rotation;
    }

    private void OnDestroy()
    {
        if (nameTagTransform != null)
        {
            Destroy(nameTagTransform.gameObject);
        }
    }
}
