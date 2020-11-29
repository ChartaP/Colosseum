using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectBtn : MonoBehaviour
{
    public void OnClick()
    {
        MultiMng.instance.ConnectLoby();
    }
}
