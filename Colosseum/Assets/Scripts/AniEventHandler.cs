using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEventHandler : MonoBehaviour
{
    [SerializeField]
    private CharCtrl ownChar = null;
    
    public void AttackStartEventListener()
    {
        ownChar.RPCFunc("AttackStart");
    }

    public void AttackEndEventListener()
    {
        ownChar.RPCFunc("AttackEnd");
    }

    public void DefendStartEventListener()
    {
        ownChar.RPCFunc("DefendStart");
    }

    public void DefendEndEventListener()
    {
        ownChar.RPCFunc("DefendEnd");
    }
}
