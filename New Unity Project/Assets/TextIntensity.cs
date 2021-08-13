using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIntensity : MonoBehaviour
{
    private int score;

    // Use this for initialization
    void Start()
    {
        score = 0;
        //GetComponent<Text>().text = "";
        setVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ñÓàÛÉLÅ[Ç…ÇÊÇÈëÄçÏ
        if (Input.GetKeyDown(KeyCode.UpArrow))
            score += 10;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            score -= 10;
        if (score < 0) score = 0;
        if (score > 100) score = 100;
        GetComponent<Text>().text = score.ToString();

       

    }

    public void setVisible(bool bb) {
        //https://techno-monkey.hateblo.jp/entry/2018/05/09/120653
        if (bb)
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 1);
        else 
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }

    public string getScore() { 
        return score.ToString();
    }
}
