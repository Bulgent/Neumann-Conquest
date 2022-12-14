using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SeManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSourceSE;
    public AudioClip deci;
    public AudioClip ret;
 
    public static SeManager Instance
    {
        get; private set;
    }
 
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
 
    private void Start()
    {
        audioSourceSE = this.GetComponent<AudioSource>();
    }
 
    public void SettingPlaySE()
    {
        audioSourceSE.PlayOneShot(deci);
    }

    public void SettingPlaySE_ret()
    {
        audioSourceSE.PlayOneShot(ret);
    }

}