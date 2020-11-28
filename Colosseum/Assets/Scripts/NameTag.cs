using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NameTag : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Text readyText;
    [SerializeField]
    private Image img1;
    [SerializeField]
    private Image img2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaster()
    {
        readyText.text = "Master";
    }

    public void SetReady(bool b)
    {
        if (b)
            readyText.text = "Ready";
        else
            readyText.text = "Wait";
    }

    public void SetText(string name)
    {
        text.text = name;
    }

    public void SetVisible(bool set)
    {
        text.enabled = set;
        readyText.enabled = set;
        img1.enabled = set;
        img2.enabled = set;
    }
}
