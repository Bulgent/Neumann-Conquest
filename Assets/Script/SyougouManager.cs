using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SyougouManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] syougou_label = new GameObject[8];
    public GameObject[] syougou_explain = new GameObject[8];
    string[] syougou_labeltext   = {"Galois",
                                    "Lagrange",
                                    "Abel",
                                    "Euclid",
                                    "関数電卓",
                                    "アナログ計算機",
                                    "そろばん",
                                    "愚者"};
    string[] syougou_explaintext = {"Get 30 right answers without mistakes!!",
                                    "Get 20 right answers without mistakes!!",
                                    "Get 15 right answers without mistakes!!",
                                    "Get 10 right answers without mistakes!!",
                                    "Get 15 right answers!!",
                                    "Get 10 right answers!!",
                                    "Get 7 right answers!!",
                                    "Do you know intermediate value theorem ?"};
    
    string[] achievement = {"Are you a second Galois?",
                            "You are as genius as Lagrange!!",
                            "Abel may like you...",
                            "No loyal road to geometry.",
                            "Walking computer",
                            "You must be a calculator.",
                            "Are you using an abacus?",
                            "The Fool"
    };
    
    public Sprite[] m_Sprite;
    public Image[] m_Image;

    void Start()
    {
        for (int i = 0; i < syougou_labeltext.Length; i++) {
            SetText(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetText(int num){
        if(PlayerPrefs.GetInt(syougou_labeltext[num],0) == 1){
            Text label_name = syougou_label[num].GetComponent<Text>();
            label_name.text = achievement[num];
            Text explain_name = syougou_explain[num].GetComponent<Text>();
            explain_name.text = syougou_explaintext[num];
            Image Image_ = m_Image[num].GetComponent<Image>();
            Image_.sprite = m_Sprite[num];
            Image_.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnClick(){
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE_ret();
        SceneManager.LoadScene("StartScene");
    }
}
