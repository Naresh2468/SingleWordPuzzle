using UnityEngine;

namespace Game
{
    public interface IUIManager
    {
        void UpdateProgress(float value);
        void ShowInteractCanvas(bool show);
        void UpdateLevelDisplay(Sprite current, Sprite previous);
    }

    public interface ILevelManager
    {
        void Initialize();
        void CheckLevelProgress(string selection);
        Sprite GetCurrentLevelSprite();
        Sprite GetPreviousLevelSprite();
        event System.Action OnLevelCompleted;
        event System.Action OnLevelFailed;
    }
}