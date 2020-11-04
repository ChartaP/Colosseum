using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEventHandler : MonoBehaviour
{
    [SerializeField]
    private CharCtrl ownChar = null;
    
    public void AttackStartEventListener()
    {
        ownChar.AttackStart();
    }

    public void AttackEndEventListener()
    {
        ownChar.AttackEnd();
    }

    public void DefendStartEventListener()
    {
        ownChar.DefendStart();
    }

    public void DefendEndEventListener()
    {
        ownChar.DefendEnd();
    }
}
