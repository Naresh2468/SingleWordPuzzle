using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class UIController : MonoBehaviour ,IUIManager
    {
        [SerializeField] private Image mainImage;
        [SerializeField] private Image currentLevelImage;
        [SerializeField] private Image previousLevelImage;
        [SerializeField] private Slider progressBar;
        [SerializeField] private GameObject interactObject,RestartGame;
        [SerializeField] internal TextDisplay textDisplay;
        [SerializeField] private Button[] selectionButtons;
        private void Start() =>  InitializeButtons();

        public void UpdateProgress(float value) => StartCoroutine(ProgressAnimation(value));

        public void UpdateMainDisplay(Sprite sprite) => mainImage.sprite = sprite;

        public void ShowInteractCanvas(bool show) => interactObject.SetActive(show);

        public void UpdateLevelDisplay(Sprite current, Sprite previous)
        {
            currentLevelImage.sprite = current;
            if (previous != null) previousLevelImage.sprite = previous;
        }
        private void InitializeButtons()
        {
            string[] words = { "Give Water", "Medicine" };
            for (int i = 0; i < selectionButtons.Length; i++)
            {
                int index = i;
                selectionButtons[i].GetComponentInChildren<TMPro.TMP_Text>().text = words[index];
                selectionButtons[i].onClick.AddListener(() =>
                    FindObjectOfType<Game>().ProcessSelection(words[index]));
            }
        }
        private IEnumerator ProgressAnimation(float target)
        {
            float start = progressBar.value;
            float elapsed = 0f;
            while (elapsed < 1f)
            {
                elapsed += Time.deltaTime;
                progressBar.value = Mathf.Lerp(start, target, elapsed);
                yield return null;
            }
        }
        public void RestartButton(bool value)
        {
            RestartGame.SetActive(value);
            Button restartBtn = RestartGame.GetComponentInChildren<Button>();
            restartBtn.onClick.AddListener(RestartGameProcess);
        }
        public void RestartGameProcess() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}