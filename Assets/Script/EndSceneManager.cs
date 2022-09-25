using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text score_cor;
    public Text score_high;

    AudioSource audioSource;
    public AudioClip sound_;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound_);

        int corans = PlayerPrefs.GetInt("corans");
        int highscore = PlayerPrefs.GetInt("highans",0);
        Text Text_cor = score_cor.GetComponent<Text> ();
        Text Text_high = score_high.GetComponent<Text> ();

        if(corans > highscore){
            PlayerPrefs.SetInt("highans",corans);
            Text_cor.text = corans.ToString();
            Text_high.text = corans.ToString();
        }else{
            Text_cor.text = corans.ToString();
            Text_high.text = highscore.ToString();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE_ret();
        SceneManager.LoadScene("StartScene");
    }
}
