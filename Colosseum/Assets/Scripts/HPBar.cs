using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Image bar;

    public CharCtrl target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if(target != null)
            {
                bar.fillAmount = target.HP / 100f;
            }
        }
        catch
        {

        }
    }
}
