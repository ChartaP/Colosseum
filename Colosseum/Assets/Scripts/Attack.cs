using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> hitList = new List<GameObject>();

    [SerializeField]
    private GameObject ownGameobject = null;

    [SerializeField]
    private float fDamage = 50f;

    private void Start()
    {
        if (!ownGameobject)
        {
            ownGameobject = transform.parent.gameObject;
        }
    }

    public void AtkStart()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    public void AtkEnd()
    {
        GetComponent<BoxCollider>().enabled = false;
        hitList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ownGameobject)
            return;
        if(other.tag == "Player")
        {
            if (!hitList.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<CharCtrl>().Hit(fDamage);
                hitList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ownGameobject)
            return;
        if (other.tag == "Player")
        {

        }
    }
}
