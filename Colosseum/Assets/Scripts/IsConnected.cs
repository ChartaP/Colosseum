using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IsConnected : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MultiMng.instance != null)
        {
            if (MultiMng.isConnected)
            {
                text.text = "Connected";
            }
            else
            {
                text.text = "Unconnected";
            }
        }
    }
}
