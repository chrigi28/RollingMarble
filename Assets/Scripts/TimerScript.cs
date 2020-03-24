using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
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

            this.CountdownText.enabled = true;
            CounterText.text = string.Empty;
            this.currentTime = this.StartTime;
            GameManager.Instance.SetGameState(EGameState.Countdown);
        }

        public void Restart()
        {
            this.Start();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.IsCountDown)
            {
                this.currentTime -= Time.deltaTime;
            
                if (this.currentTime > 0.5)
                {
                    this.CountdownText.text = currentTime.ToString("0");
                }
                else
                {
                    this.CountdownText.text = "GO!";
                    GameManager.Instance.ContinueGame();
                    this.currentTime = 0f;
                    this.animator.Play("TextFade");
                }
            }
            else
            if (GameManager.Instance.IsRunning())
            {
                if (this.currentTime > 1f)
                {
                    this.CountdownText.enabled = false;
                }

                this.currentTime += Time.deltaTime;
                this.CounterText.text = currentTime.ToString("F");
            }
        }
    }
}
