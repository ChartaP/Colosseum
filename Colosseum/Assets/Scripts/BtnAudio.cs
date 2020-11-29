using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip btnClip;

    public void OnClick()
    {
        AudioSource.PlayClipAtPoint(btnClip, Camera.main.transform.position);
    }
}
