using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : BaseScript
{
    private float currentTime = 0f;
    private float StartTime = 5f;

    [SerializeField] Text CountdownText;
    [SerializeField] Text CounterText;
    private Animator animator;

    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CounterText.text = string.Empty;
        this.currentTime = this.StartTime;
        GameManager.GameState = EGameState.Countdown;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameManager.IsCountDown)
        {
            this.currentTime -= Time.deltaTime;
            
            if (this.currentTime > 0.5)
            {
                this.CountdownText.text = currentTime.ToString("0");
            }
            else
            {
                this.CountdownText.text = "GO!";
                this.gameManager.ContinueGame();
                this.currentTime = 0f;
                this.animator.Play("TextFade");
            }
        }
        else
        {
            if (this.currentTime > 0.5f)
            {
                this.CountdownText.enabled = false;
            }

            this.currentTime += Time.deltaTime;
            this.CounterText.text = currentTime.ToString("F");
        }
    }
}
