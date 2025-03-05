using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    #region Level and LevelFactory

    public class Level
    {
        public Sprite imageLevel { get; private set; }

        public List<string> correctWordsLevel { get; private set; }
        public List<string> possibleWordsLevel { get; private set; }
        public Level(Sprite image, List<string> correctWords, List<string> possibleWords)
        {
            imageLevel = image;
            correctWordsLevel = correctWords;
            possibleWordsLevel = possibleWords;
        }

        public bool IsCorrectWord(string word)
        {
            return correctWordsLevel.Contains(word);
        }
    }

    public static class LevelFactory
    {
        public static Level CreateLevel(int index)
        {
            Sprite sprite = Resources.Load<Sprite>($"Images/level/{index}");
            var Data =  new Level(
                sprite,
                new List<string> { "Medicine", "cold" },
                new List<string> { "Give Water", "Medicine", "cold", "fever" }
            );

            return Data;
        }
    }

    #endregion

    public class LevelManager : MonoBehaviour , ILevelManager
    {
        [Header("V A L U E S")]
        public int totalLevels = 3;
        private int currentLevelIndex = 0;
        public List<Level> levels = new List<Level>();


        [Header("R E F E R E N C E S")]
        public Game gameRef;
        public event System.Action OnLevelCompleted;
        public event System.Action OnLevelFailed;

        public void Initialize()
        {
            GenerateLevels();
            UpdateUIDisplay();
        }

        void GenerateLevels()
        {
            for (int i = 0; i < totalLevels; i++)
            {
                levels.Add(LevelFactory.CreateLevel(i));
            }
        }
        public void CheckLevelProgress(string selection)
        {
            if (levels[currentLevelIndex].IsCorrectWord(selection))
            {
                OnLevelCompleted?.Invoke();
                AdvanceLevel();
            }
            else
            {
                OnLevelFailed?.Invoke();
            }
        }
        private void AdvanceLevel()
        {
            currentLevelIndex = Mathf.Min(currentLevelIndex + 1, levels.Count - 1);
            UpdateUIDisplay();
        }
        private void UpdateUIDisplay()
        {
            FindObjectOfType<UIController>()?.UpdateLevelDisplay(
                GetCurrentLevelSprite(),
                GetPreviousLevelSprite()
            );
        }
        public Sprite GetCurrentLevelSprite() => levels[currentLevelIndex].imageLevel;
        public Sprite GetPreviousLevelSprite() => currentLevelIndex > 0 ? levels[currentLevelIndex - 1].imageLevel : null;
    }

}