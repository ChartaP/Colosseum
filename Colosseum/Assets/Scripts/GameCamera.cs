using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private static GameObject target = null;

    public static void SetTarget(GameObject target)
    {
        GameCamera.target = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = target.transform.position;
            transform.position += new Vector3(0,10f,0);
            transform.position -= Quaternion.Euler(0,target.transform.eulerAngles.y,0) * Vector3.forward * 10f;
            transform.rotation = Quaternion.Euler(25f, target.transform.eulerAngles.y ,0f);
        }
        catch
        {
            Debug.LogError("카메라가 타겟을 잃어버렸습니다");
        }
    }
}
