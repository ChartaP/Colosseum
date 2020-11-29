using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] attackClips;
    [SerializeField]
    private AudioClip[] hitClips;
    [SerializeField]
    private AudioClip[] footstepClips;
    [SerializeField]
    private AudioClip screamClip;

    [SerializeField]
    private AudioClip[] deffenceClips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deffence()
    {
        AudioSource.PlayClipAtPoint(deffenceClips[Random.Range(0, deffenceClips.Length)], transform.position);
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(screamClip, transform.position);
    }

    public void Attack()
    {
        AudioSource.PlayClipAtPoint(attackClips[Random.Range(0, attackClips.Length)], transform.position);
    }

    public void Hit()
    {
        AudioSource.PlayClipAtPoint(hitClips[Random.Range(0, hitClips.Length)], transform.position);
    }

    public void FootStep()
    {
        AudioSource.PlayClipAtPoint(footstepClips[Random.Range(0, footstepClips.Length)], transform.position);
    }
}
