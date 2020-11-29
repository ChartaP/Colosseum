using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolulationSet : MonoBehaviour
{
    [SerializeField]
    private Toggle FullScreenToggle;
    [SerializeField]
    private InputField WidthInput;
    [SerializeField]
    private InputField HeightInput;
    // Start is called before the first frame update
    void Start()
    {
        //WidthInput.text = Screen.width.ToString();
        //HeightInput.text = Screen.height.ToString() ;
    }

    public void OnCheckFullScreen(bool check)
    {
        if (check)
        {
            Screen.SetResolution(1920, 1280, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);

        }
    }

    public void OnActive()
    {
        gameObject.SetActive(true);
    }

    public void OnAccept()
    {
        OnCheckFullScreen(FullScreenToggle.isOn);
        gameObject.SetActive(false);
    }
}
