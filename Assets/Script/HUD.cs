using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health, Coin }
    public InfoType type;

    Text myText;
    Slider mySlider;

    float time;
    float breakTime;


    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();    
    }

    void Update() {
        time = Time.deltaTime;
        if (!GameManager.instance.isGame)
            breakTime = Time.deltaTime;
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                myText.text = string.Format("Level {0:F0}", GameManager.instance.level);
                break;

            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;

            case InfoType.Time:
                float remainTime = 60f;
               
                if(GameManager.instance.isGame) {
                    remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                } else {
                    remainTime = GameManager.instance.gameSleepTime - breakTime;
                }

                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            
            case InfoType.Coin:
                myText.text = string.Format("{0:F0}", GameManager.instance.coin);
                break;
        }
    }

}
