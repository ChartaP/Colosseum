using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string name)
    {
        text.text = name;
    }

    public void SetVisible(bool set)
    {
        text.enabled = set;
        img.enabled = set;
    }
}
