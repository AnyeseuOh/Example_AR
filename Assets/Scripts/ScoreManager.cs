using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public Slider hpBar;
    public Text scoreText;
    public int score;

    void Start()
    {
        score = 0;
        hpBar = GetComponent<Slider>();
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void Damaged ()
    {
        DOTween.To(() => hpBar.value, x => hpBar.value = x, 0, 2f);
    }
}
