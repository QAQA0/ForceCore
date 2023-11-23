using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public LevelUp uiLevelUp;
    private int levelUpLevel = 0;
    
    void OnCollisionStay2D(Collision2D collision) 
    {
        if(!GameManager.instance.isLive)
            return;
        
        if(collision.collider.CompareTag("Enemy"))
            GameManager.instance.health -= Time.deltaTime * 10;
        
        if(GameManager.instance.health < 0) {
            GameManager.instance.GameOver();
        }

    }

    void OnMouseDown()
    {
        Debug.Log("snffjwla");
        if (!GameManager.instance.isGame)
        {
            for (; levelUpLevel <= GameManager.instance.level;)
            {
                uiLevelUp.Show();
                levelUpLevel++;
            }
        }
    }
}
