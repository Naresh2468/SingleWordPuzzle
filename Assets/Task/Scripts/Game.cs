using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [Header("V A L U E S")]
        public bool isGameFinished;

        [Header("R E F E R E N C E S")]
        private ILevelManager levelManager;
        public GameContent gameContent;
        public UIController uiRef;
        public UnityEvent Action;

        private void Awake()
        {
            levelManager = GetComponent<ILevelManager>();
            SetupLevelManager();
        }
        private void SetupLevelManager()
        {
            levelManager.OnLevelCompleted += HandleLevelCompleted;
            levelManager.OnLevelFailed += HandleLevelFailed;
            levelManager.Initialize();
        }

        private void OnDisable()
        {
            levelManager.OnLevelCompleted -= HandleLevelCompleted;
            levelManager.OnLevelFailed -= HandleLevelFailed;
            levelManager = null;
        }

        private void HandleLevelCompleted()
        {
            uiRef.UpdateProgress(2f);
            print("Level Completed!");// debug purpose
            UpdateGameState(true);
        }

        private void HandleLevelFailed()
        {
            print("Incorrect Selection! Try Again."); // debug purpose
            UpdateGameState(false);
            StartCoroutine(BacktoGame());
        }
        public void ProcessSelection(string selection) => levelManager.CheckLevelProgress(selection);

        private void UpdateGameState(bool success)
        {
            isGameFinished = success;
            uiRef.ShowInteractCanvas(success);
            uiRef.UpdateMainDisplay(gameContent.Contents[success ? 3 : 2]);
            uiRef.textDisplay.PassDialogue((byte)(success ? 4 : 3));
            if (!isGameFinished) Action.Invoke();
        }
        public void WinGame()
        {
            if (!isGameFinished)
            {
                uiRef.ShowInteractCanvas(true);
                
            }
            else
            {
                uiRef.UpdateMainDisplay( gameContent.Contents[4]);
                uiRef.textDisplay.WinPassDialogue(5);
                uiRef.RestartButton(isGameFinished);
            }
        }
        IEnumerator BacktoGame()
        {
            yield return new WaitForSeconds(2f);
            uiRef.UpdateMainDisplay(gameContent.Contents[1]);
            uiRef.ShowInteractCanvas(!isGameFinished);
        }
    }
}
