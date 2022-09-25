using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; 
using UnityEngine.SceneManagement;
using TMPro;
public class CreateEpuation : MonoBehaviour
{
    //変数宣言
    public Text timelabel;
    public Text corlabel;
    public Text fallabel;
    public Camera cam_; 
    //AudioSource audioSource;
    //public AudioClip sound_;
    public Button[] buttons = new Button[4];
    Button[] buttons_ = new Button[4];

    Camera cam;
    //方程式のテキスト(textmeshpro)
    public GameObject textobject_name;
    private TextMeshProUGUI textbox_name;
    Text TimeText;
    //制限時間
    float time_ = 180.0f;
    //
    float countdown = 3.0f;
    bool first_take = true;

    public Image correct_icon_;
    public Image false_icon_;
    
    //1問目のみ表示
    void Start()
    {
        //正解数, 不正解数の生成
        int falans = 0;
        int corans = 0;
        Text corText = corlabel.GetComponent<Text> ();
        Text falText = fallabel.GetComponent<Text> ();
        PlayerPrefs.SetInt("falans",falans);
        PlayerPrefs.SetInt("corans",corans);
        corText.text = falans.ToString();
        falText.text = corans.ToString();

        //制限時間の生成
        TimeText = timelabel.GetComponent<Text> ();

        //背景色変更準備
        cam = cam_.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;

        //audioSource = GetComponent<AudioSource>();

        //Image correct_icon = correct_icon_.GetComponent<Image>();
        //Image false_icon = false_icon_.GetComponent<Image>();
        //correct_icon.enabled = false;
       // false_icon.enabled = false;

        correct_icon_.gameObject.SetActive(false);
        false_icon_.gameObject.SetActive(false);

        //ボタンのゲット
        for(int i = 0; i<buttons.Length;i++){
            buttons_[i] = buttons[i].GetComponent<Button>();
        }
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown >= 1){
            TimeText.text = countdown.ToString("F0");

            for(int i = 0; i<buttons.Length;i++){
                buttons_[i].interactable = false;
            }

        } else {
            //制限時間
            time_ -= Time.deltaTime;
            TimeText.text = time_.ToString("F1");

            for(int i = 0; i<buttons.Length;i++){
                buttons_[i].interactable = true;
            }

            //問題の生成(1回目のみ)
            if (first_take == true){
                //解の個数決定
                int [] kai_kosu = Random_CN(1,3);
                int value_kai = kai_kosu[0];
                //方程式の生成
                string equ = Equation(value_kai);
                textbox_name = textobject_name.GetComponent<TextMeshProUGUI>();
                textbox_name.text = equ;
                //別スクリプトでの解個数の共有
                PlayerPrefs.SetInt("kai", value_kai);

                first_take = false;
            }
            //if(PlayerPrefs.GetInt("falans") >= 2){
            //    cam.backgroundColor = new Color(0.89f, 0.51f, 0, 1);
            //}
            //終了条件
            if (PlayerPrefs.GetInt("falans") >= 3 || time_ < 0){
                int cor_ = PlayerPrefs.GetInt("corans");
                int fal_ = PlayerPrefs.GetInt("falans");
                //audioSource.PlayOneShot(sound_);
                //称号取得条件
                if (fal_ == 0){
                    if (cor_ >= 30){
                        PlayerPrefs.SetInt("Galois",1);
                        PlayerPrefs.SetInt("Lagrange",1);
                        PlayerPrefs.SetInt("Abel",1);
                        PlayerPrefs.SetInt("Euclid",1);
                        PlayerPrefs.SetInt("関数電卓",1);
                        PlayerPrefs.SetInt("アナログ計算機",1);
                        PlayerPrefs.SetInt("そろばん",1);
                    } else if (cor_ >= 20){
                        PlayerPrefs.SetInt("Lagrange",1);
                        PlayerPrefs.SetInt("Abel",1);
                        PlayerPrefs.SetInt("Euclid",1);
                        PlayerPrefs.SetInt("関数電卓",1);
                        PlayerPrefs.SetInt("アナログ計算機",1);
                        PlayerPrefs.SetInt("そろばん",1);
                    } else if (cor_ >= 15){
                        PlayerPrefs.SetInt("Abel",1);
                        PlayerPrefs.SetInt("Euclid",1);
                        PlayerPrefs.SetInt("関数電卓",1);
                        PlayerPrefs.SetInt("アナログ計算機",1);
                        PlayerPrefs.SetInt("そろばん",1);
                    } else if (cor_ >= 10){
                        PlayerPrefs.SetInt("Euclid",1);
                        PlayerPrefs.SetInt("アナログ計算機",1);
                        PlayerPrefs.SetInt("そろばん",1);
                    } else {
                        ;
                    }
                }else{
                    if (cor_ >= 15){
                        PlayerPrefs.SetInt("関数電卓",1);
                        PlayerPrefs.SetInt("アナログ計算機",1);
                        PlayerPrefs.SetInt("そろばん",1);
                    } else if (cor_ >= 10){
                        PlayerPrefs.SetInt("アナログ計算機",1);
                        PlayerPrefs.SetInt("そろばん",1);
                    } else if (cor_ >= 7){
                        PlayerPrefs.SetInt("そろばん",1);
                    } else{
                        ;
                    }
                }
                SceneManager.LoadScene("EndScene");
            }
        }
    }

//解のランダム生成(有理数, 0無し)
    public static int [] Random_CN(int min_, int max_){
            // min_～max_までの並んだデータを作成
            int length_ = max_ - min_ + 1;
            int[] data = new int[length_];
            for (int i = 0; i < data.Length; i++)
            {
            data[i] = i + min_;
            }

            // シャッフル
            for (int i = data.Length - 1; i > 0; i--)
            {
            int j = Random.Range(0,data.Length - 1);
            int tmp = data[i];
            data[i] = data[j];
            data[j] = tmp;
            }
            
            var list = new List<int>();
            for (int i=0; i<data.Length; i++) {
            list.Add(data[i]);
            }
            //0を削除
            if(list.Contains(0)){
                list.Remove(0);
            }

            // 配列にして返却
            return list.ToArray();
        }
    
    public static string Equation(int quantity){
        //係数, 解の個数定義
        int x_3, x_2, x_1, x_0;

        //解の範囲指定
        int maxnum = 5;
        int minnum = -5;


        //解が1つの時
        if (quantity == 1){
            //解をランダムで設定
            int [] kai = Random_CN(minnum,maxnum);
            int a = kai[0];

            //方程式の場合分け
            int choice = Random.Range(1,3);
            if(choice == 1){
            //(ax-1)^3 = 0
                x_3 = a*a*a;
                x_2 = -3*a*a;
                x_1 = 3*a;
                x_0 = -1;
            }else{
            //(x-a)^3 = 0
                x_3 = 1;
                x_2 = -3*a;
                x_1 = 3*a*a;
                x_0 = -a*a*a;
            }

        //解が2つの時
        } else if (quantity == 2){
            //解をランダムで設定
            int [] kai = Random_CN(minnum,maxnum);
            int a = kai[0];
            int b = kai[1];

            //方程式の場合分け
            int choice = Random.Range(1,4);
            if(choice == 1){
            //(x-a)(x-b)^2=0
                x_3 = 1;
                x_2 = -a-2*b;
                x_1 = b*b+2*a*b;
                x_0 = -a*b*b;
            } else if(choice == 2) {
            //(ax-1)(x-b)^2=0
                x_3 = a;
                x_2 = -2*a*b-1;
                x_1 = a*b*b+2*b;
                x_0 = -b*b;
            } else {
            //(ax-1)(bx-1)^2=0
                x_3 = a*b*b;
                x_2 = -2*a*b-b*b;
                x_1 = a+2*b;
                x_0 = -1;
            }

        //解が3つの場合
        } else {
            //解をランダムで設定
            int [] kai = Random_CN(minnum,maxnum);
            int a = kai[0];
            int b = kai[1];
            int c = kai[2];

            //方程式の場合分け①
            int choice = Random.Range(1,4);
            if (choice == 1 || choice == 2){
                //方程式の場合分け②
                int choice_1 = Random.Range(1,5);

                if(choice_1 == 1){
                //(ax-1)(bx-1)(cx-1)=0
                    x_3 = a*b*c;
                    x_2 = -1*(a*b + b*c + c*a);
                    x_1 = a+b+c;
                    x_0 = -1;
                } else if(choice_1 == 2){
                //(x-a)(x-b)(x-c)=0
                    x_3 = 1;
                    x_2 = -a-b-c;
                    x_1 = a*b+b*c+c*a;
                    x_0 = -1*a*b*c;
                } else if(choice_1 == 3){
                //(x-a)(bx-1)(cx-1)=0
                    x_3 = b*c;
                    x_2 = -b-c-a*b*c;
                    x_1 = a*b+c*a+1;
                    x_0 = -1*a;
                } else {
                //(x-a)(x-b)(cx-1)=0
                    x_3 = c;
                    x_2 = -1-a*c-b*c;
                    x_1 = a+b+a*b*c;
                    x_0 = -a*b;
                }
            } else {
                
                if(a == 1){
                //重解を防ぐため
                // (bx-1)(x-b)(x-a)=0とする
                    x_3 = b;
                    x_2 = -a*b-b*b-1;
                    x_1 = a+b+a*b*b;
                    x_0 = -a*b;
                }else{
                //(ax-1)(x-a)(x-b)=0
                    x_3 = a;
                    x_2 = -a*a-a*b-1;
                    x_1 = a*a*b+a+b;
                    x_0 = -a*b;
                }
            }
        }
        
        //式の体裁を整える
        int[] array1 = new int[] { x_2, x_1, x_0 };
        string[] array2 = new string[3];
        string ans;

        //係数の正負, 1, -1の処理
        for (int i = 0; i < 2 ; i++){
            if (array1[i] == 1){
                array2[i] = string.Format("+", array1[i]);
            } else if (array1[i] > 1){
                array2[i] = string.Format("+{0}", array1[i]);
            } else if(array1[i] == -1){
                array2[i] = string.Format("-", array1[i]);
            } else {
                array2[i] = string.Format("-{0}", array1[i]*(-1));
            }
        }
        
        //定数項の処理
        if (array1[2] > 0){
            array2[2] = string.Format("+{0}", array1[2]);
        } else {
            array2[2] = string.Format("-{0}", array1[2]*(-1));
        }

        //x^3の項の処理, 整式として形成
        if (x_3 == 1) {
            if (array1[0] == 0 && array1[1] == 0){
            ans = string.Format("x<sup>3</sup>{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else if (array1[0] == 0){
                ans = string.Format("x<sup>3</sup>{2}x{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else if (array1[1] == 0){
                ans = string.Format("x<sup>3</sup>{1}x<sup>2</sup>{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else {
                ans = string.Format("x<sup>3</sup>{1}x<sup>2</sup>{2}x{3} = 0",x_3,array2[0],array2[1],array2[2]);
            }   
        } else if (x_3 == -1){
            if (array1[0] == 0 && array1[1] == 0){
            ans = string.Format("-x<sup>3</sup>{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else if (array1[0] == 0){
                ans = string.Format("-x<sup>3</sup>{2}x{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else if (array1[1] == 0){
                ans = string.Format("-x<sup>3</sup>{1}x<sup>2</sup>{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else {
                ans = string.Format("-x<sup>3</sup>{1}x<sup>2</sup>{2}x{3} = 0",x_3,array2[0],array2[1],array2[2]);
            }
        } else {
            if (array1[0] == 0 && array1[1] == 0){
            ans = string.Format("{0}x<sup>3</sup>{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else if (array1[0] == 0){
                ans = string.Format("{0}x<sup>3</sup>{2}x{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else if (array1[1] == 0){
                ans = string.Format("{0}x<sup>3</sup>{1}x<sup>2</sup>{3} = 0",x_3,array2[0],array2[1],array2[2]);
            } else {
                ans = string.Format("{0}x<sup>3</sup>{1}x<sup>2</sup>{2}x{3} = 0",x_3,array2[0],array2[1],array2[2]);
            }
        }
        
        return ans;
    }

    
}
