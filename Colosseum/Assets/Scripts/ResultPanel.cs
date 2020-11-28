using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPanel(int rank)
    {
        gameObject.SetActive(true);
        if(rank == 1)
        {
            title.text = "Winner!";
            text.text = "1/"+GameMng.instance.RoomSize;
        }
        else
        {
            title.text = "You Died";
            text.text = rank+"/" + (GameMng.instance.RoomSize);
        }
    }

}
