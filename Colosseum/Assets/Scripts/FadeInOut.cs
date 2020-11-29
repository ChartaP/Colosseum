using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    private Image img;

    private Color OutColor = new Color(0, 0, 0, 1);
    private Color InColor = new Color(0, 0, 0, 0);

    private int temp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void LoadScene(int num)
    {
        temp = num;
        StartCoroutine("FadeOut");
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("페이드 아웃");
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            img.color = Color.Lerp(img.color, OutColor,0.064f);

            if (img.color.a >= 0.9)
            {
                Debug.Log("임계값 도달 알파 255");
                break;
            }
        }
        MultiMng.instance.LoadLevel(temp);

        while (true)
        {
            yield return null;
            if (SceneManager.GetActiveScene().buildIndex == temp)
            {
                Debug.Log("씬 바뀜");
                StartCoroutine("FadeIn");
                break;
            }
            else
            {
                Debug.Log("씬 안바뀜");
            }
        }
        yield break;
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("페이드 인");
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            img.color = Color.Lerp(img.color, InColor, 0.064f);

            if (img.color.a <= 0.1)
            {
                Debug.Log("임계값 도달 알파 0");
                break;
            }
        }
        Destroy(gameObject);
        yield break;
    }

}
