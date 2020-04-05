using System.Security.Cryptography.X509Certificates;
using Assets.Scripts;
using Assets.Scripts.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public PlayerData PlayerData { get; set; }
        
        private EGameState gameState = EGameState.Paused;

        public GameObject Canvas;
        private GameObject currentLevel;
        private CanvasScript canvasScript;
        private TimerScript timerScript;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                if (Application.platform == RuntimePlatform.Android)
                {
                    Application.targetFrameRate = 30;
                }

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            this.canvasScript = this.Canvas.GetComponent<CanvasScript>();
            this.DisableMenus();
        }

        void Start()
        {
            timerScript = this.Canvas.GetComponent<TimerScript>();
            this.DisableMenus();
        }

        void OnEnable()
        {
        
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            this.StartCountDown();
        }

        public void GoToLevelSelection()
        {
            SceneManager.LoadScene(0);
        }

        public void SelectLevel(TextMeshProUGUI t)
        {
            //load LevelSelectionScene
            this.LoadLevel(int.Parse(t.text));
            this.DisableMenus();
            this.StartCountDown();
        }

        private void StartCountDown()
        {
            this.DisableMenus();
            gameState = EGameState.Countdown;
            Time.timeScale = 1;
            timerScript.Restart();
        }

        private void LoadLevel(int level)
        {
            SceneManager.LoadScene(level);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            gameState = EGameState.Paused;
            this.canvasScript.TogglePause();
        }

        public bool IsRunning()
        {
            return gameState == EGameState.Running;
        }

        public bool IsCountDown => gameState == EGameState.Countdown;

        public bool IsPaused()
        {
            return gameState == EGameState.Paused;
        }

        public void DisableMenus()
        {
            this.canvasScript.DisableMenus();
        }

        public void ContinueGame()
        {
            gameState = EGameState.Running;
            this.DisableMenus();
            Time.timeScale = 1;
        }

        public void ShowSettings()
        {
            Debug.Log("Settings Requested");
        }

        public void ShowHome()
        {

            SceneManager.LoadScene(0);
            Debug.Log("Settings Requested");
        }

        public void FinishLevel()
        {
            Debug.Log("GameFinished");
            this.canvasScript.ShowFinish(this.timerScript.GetTime());
            Time.timeScale = 0;
            gameState = EGameState.Paused;
            
            // show time
            // show Score (nr * )
            // show next start level
            // show level select 
            // show go home
        }

        public void SetGameState(EGameState state)
        {
            this.gameState = state;
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            gameState = EGameState.Paused;
            this.canvasScript.SetGameOver(true);
        }

        public void LoadPlayerData()
        {
            this.PlayerData = GameDataManager.LoadPlayerData();
        }
    }
}
