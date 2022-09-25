using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GetPyramid : MonoBehaviour
{
    //Dammy解のばらつき
    int kairange = 10;
    // Start is called before the first frame update

    //解のボタン取得
    public Button[] buttons_pyr = new Button[4];
    Button[] buttons_pyr_ = new Button[4];

    void Start()
    {
        //座標, スカラー三重積の生成
        var Coodinate = CreatePyramid(10);
        //ダミー解の生成
        var Anses = DammyAns(Coodinate["Parallelepiped"]);
        //解の被り解消&絶対値化
        var Anses_kai = AbsoluteAns(Coodinate["Parallelepiped"], Anses[0], Anses[1], Anses[2]);
        Debug.Log(Anses_kai["kai"]);
        Debug.Log(Anses_kai["D1"]);
        Debug.Log(Anses_kai["D2"]);
        Debug.Log(Anses_kai["D3"]);
        //ボタンの文字変更
        WriteText(Anses_kai["kai"],Anses_kai["D1"],Anses_kai["D2"],Anses_kai["D3"]);
    }

    // Update is called once per frameDebug.Log(
    void Update()
    {
        
    }

    void main(){
        //座標, スカラー三重積の生成
        var Coodinate = CreatePyramid(10);
        //ダミー解の生成
        var Anses = DammyAns(Coodinate["Parallelepiped"]);
        //解の被り解消&絶対値化
        var Anses_kai = AbsoluteAns(Coodinate["Parallelepiped"], Anses[0], Anses[1], Anses[2]);
    }

    internal void WriteText(int kai_p, int numa, int numb, int numc){
        //解を持つボタンを決定
        int kai_b = Random.Range(0, 4);
        PlayerPrefs.SetInt("kai_pr",kai_b);

        //解のリスト, ボタンのリスト生成
        var list_dammy = new List<int>{numa, numb, numc};
        var list_dammy_bot = new List<int>{0,1,2,3};
        list_dammy_bot.Remove(kai_b);

        //解を持つボタンの出力
        //分母無い時
        if (CreateDivision(kai_p)["bunbo"] == 1){
            buttons_pyr[kai_b].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CreateDivision(kai_p)["bunshi"].ToString();
            buttons_pyr[kai_b].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            buttons_pyr[kai_b].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        //分母ある時
        } else {
            buttons_pyr[kai_b].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CreateDivision(kai_p)["bunshi"].ToString();
            buttons_pyr[kai_b].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = CreateDivision(kai_p)["bunbo"].ToString();
        }

        //ダミーボタンの出力
        for(int i = 0; i< 4; i++){
            //分母無い時
            if(CreateDivision(list_dammy[i])["bunbo"] == 1){
                buttons_pyr[list_dammy_bot[i]].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CreateDivision(list_dammy[i])["bunshi"].ToString();
                buttons_pyr[list_dammy_bot[i]].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                buttons_pyr[list_dammy_bot[i]].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            //分母ある時
            } else {
                buttons_pyr[list_dammy_bot[i]].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CreateDivision(list_dammy[i])["bunshi"].ToString();
                buttons_pyr[list_dammy_bot[i]].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = CreateDivision(list_dammy[i])["bunbo"].ToString();
            }
        }
    }

    public Dictionary<string, int>CreatePyramid(int num)
    {
        //numは座標の範囲
        var Coodinate = new Dictionary<string, int>();
        string[] Coodinate_name = {"ax","ay","az","bx","by","bz","cx","cy","cz","dx","dy","dz"};

        foreach(var monoCoodinate in Coodinate_name)
        {
            int numb = Random.Range(-1*num, num+1);
            Coodinate.Add(monoCoodinate,numb);
        }
        int px = Coodinate["ax"] - Coodinate["dx"];
        int py = Coodinate["ay"] - Coodinate["dy"];
        int pz = Coodinate["az"] - Coodinate["dz"];
        int qx = Coodinate["bx"] - Coodinate["dx"];
        int qy = Coodinate["by"] - Coodinate["dy"];
        int qz = Coodinate["bz"] - Coodinate["dz"];
        int rx = Coodinate["cx"] - Coodinate["dx"];
        int ry = Coodinate["cy"] - Coodinate["dy"];
        int rz = Coodinate["cz"] - Coodinate["dz"];

        //外積の計算
        int cross_x = py*qz-pz*qy;
        int cross_y = pz*qx-px*qz;
        int cross_z = px*qy-py*qx;

        // 1/6する前
        int Parallelepiped = cross_x*rx + cross_y*ry + cross_z*rz;

        Coodinate.Add("Parallelepiped",Parallelepiped);

        //負の値の可能性あり
        return Coodinate;
    }

    public Dictionary<string, int>CreateDivision(int num)
    {
        //6で割った時の分子と分母を返します
        var divisions = new Dictionary<string, int>();
        if(num%6 == 0){
            divisions.Add("bunshi",num/6);
            divisions.Add("bunbo",1);
        } else if(num%3 == 0){
            divisions.Add("bunshi",num/3);
            divisions.Add("bunbo",2);
        } else if(num%2 == 0){
            divisions.Add("bunshi",num/2);
            divisions.Add("bunbo",3);
        } else{
            divisions.Add("bunshi",num);
            divisions.Add("bunbo",6);
        }

        return divisions;
    }

    public int[] DammyAns(int a)
    {
        //kairangeは5以上(10くらいが適当?)
        //解のばらつき方決定
        int choice = Random.Range(1, 4);
        int[] arr = new int[3];

        if (choice == 1){
            int [] Dammy_D = Random_CN(-1*kairange, kairange+1);
            arr[0] = a + Dammy_D[0];
            arr[1] = a + Dammy_D[1];
            arr[2] = a + Dammy_D[2];
        } else if(choice == 2){
            int [] Dammy_D = Random_CN(2, kairange+1);
            arr[0] = a + Dammy_D[0];
            arr[1] = a + Dammy_D[1];
            arr[2] = a + Dammy_D[2];
        } else {
            int [] Dammy_D = Random_CN(-1*kairange, -1);
            arr[0] = a + Dammy_D[0];
            arr[1] = a + Dammy_D[1];
            arr[2] = a + Dammy_D[2];
        }
        //値が負の場合もあります
        return arr;
    }

    public Dictionary<string, int>AbsoluteAns(int ans, int a,int b,int c)
    {
        int[] anss = {Mathf.Abs(ans), Mathf.Abs(a), Mathf.Abs(b), Mathf.Abs(c)};
        var absans = new Dictionary<string, int>();
        int [] kair = Random_CN(kairange+1, kairange+15);
        absans.Add("kai",Mathf.Abs(ans));

        //被りの値を取得
        var duplicates = anss.GroupBy(name => name).Where(name => name.Count() > 1)
            .Select(group => group.Key).ToList();

        //それぞれの被りの値について
        if(duplicates.Count != 0){
            //それぞれの解の絶対値について
            int con__ = 0;
            foreach(var duplicate in duplicates){
                //被りの個数カウント
                int con = anss.Count(n => n == duplicate) - 1;
                for(int i = 1; i<4; i++){
                    //被ってたら解の+20くらいの値に変更
                    if (anss[i] == duplicate){
                        anss[i] = Mathf.Abs(ans) + kair[con__];
                        con -= 1;
                    }
                    //被りがなくなったら次へ
                    if(con == 0){
                        break;
                    }
                    //乱数を被りなく
                    con__ += 1;
                }
            }
        }

        absans.Add("D1", anss[1]);
        absans.Add("D2", anss[2]);
        absans.Add("D3", anss[3]);

        return absans;
    }

    public static int [] Random_CN(int min_, int max_)
    {
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
}
