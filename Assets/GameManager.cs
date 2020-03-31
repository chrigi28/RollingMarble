using Assets.Scripts;
using Assets.Scripts.GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
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
            this.DisableMenus();
        }

        void OnEnable()
        {
        
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(1);
            var timerScript = gameObject.GetComponentInChildren<TimerScript>();
            this.DisableMenus();
            gameState = EGameState.Countdown;
            Time.timeScale = 1;
            timerScript.Restart();

        }

        public void SelectLevel()
        {
            //load LevelSelectionScene
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
            this.canvasScript.ShowFinish();
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
