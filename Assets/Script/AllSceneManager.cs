using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
public class AllSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    AudioClip sound_;

    public SeManager seManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //AudioSourceを入れること
    public void OnClick_Equation()
    {
        //AudioSource audioSource;
        //public AudioClip sound_;
        ////audioSource = GetComponent<AudioSource>();
        ////audioSource.PlayOneShot(sound_);
        //yield return new WaitForSeconds(1.0f);
        ////seManager.SettingPlaySE();
        //Thread.Sleep(100);
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE();
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClick_Syougou()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(sound_);
        //Thread.Sleep(50);
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE();
        SceneManager.LoadScene("Syougou");
    }
    public void OnClick_End()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound_);
        SceneManager.LoadScene("EndScene");
    }
    public void OnClick_Start()
    {
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE_ret();
        SceneManager.LoadScene("StartScene");
    }
    public void OnClick_HowToPlay()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(sound_);
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE();
        SceneManager.LoadScene("How to Play");
    }
}

