using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    private Text myText;
    private Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                UpdateExp();
                break;
            case InfoType.Level:
                UpdateLevel();
                break;
            case InfoType.Kill:
                UpdateKill();
                break;
            case InfoType.Time:
                UpdateTime();
                break;
            case InfoType.Health:
                UpdateHealth();
                break;
        }
    }

    private void UpdateExp()
    {
        float curExp = GameManager.instance.exp;
        float maxExp = GameManager.instance.nextExp[
            Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
        mySlider.value = curExp / maxExp;
    }

    private void UpdateLevel()
    {
        myText.text = $"Lv. {GameManager.instance.level:F0}";
    }

    private void UpdateKill()
    {
        myText.text = $"{GameManager.instance.kill:F0}";
    }

    private void UpdateTime()
    {
        float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        myText.text = $"{min:D2}:{sec:D2}";
    }

    private void UpdateHealth()
    {
        float curHealth = GameManager.instance.health;
        float maxHealth = GameManager.instance.maxHealth;
        mySlider.value = curHealth / maxHealth;
    }
}
