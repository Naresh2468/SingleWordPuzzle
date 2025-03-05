using System.Collections;
using UnityEngine;
using TMPro;

namespace Game
{
    public class TextDisplay : MonoBehaviour
    {
        [Header("V A L U E S")]
        public byte startIndex;
        private byte currentStatementIndex = 0;

        [Header("R E F E R E N C E S")]
        public TMP_Text textDisplay;
        public GameObject Self;
        public Game gameRef;

        private void Start()
        {
            SelfActive(true);
            StartCoroutine(DisplayStartTextSequence());// Start the typewriter effect when the script starts
        }

        private IEnumerator DisplayStartTextSequence()
        {
            for (int i = 0; i < startIndex; i++)
            {
                yield return StartCoroutine(TypeText(gameRef.gameContent.textStatements[i]));
                yield return new WaitForSeconds(1.5f);
            }
            gameRef.uiRef.UpdateMainDisplay(gameRef.gameContent.Contents[1]);
            gameRef.uiRef.ShowInteractCanvas(true);
            SelfActive(false);
        }

        public void PassDialogue(byte value)
        {
            SelfActive(true);
            gameRef.uiRef.ShowInteractCanvas(false);
            StartCoroutine(DisplayTextSequenceBasedIndex(value));
        }

        private IEnumerator DisplayTextSequenceBasedIndex(byte Value)
        {
           
            yield return StartCoroutine(TypeText(gameRef.gameContent.textStatements[Value]));
            yield return new WaitForSeconds(1.5f);
            SelfActive(false);
            gameRef.WinGame();
        }

        public void WinPassDialogue(byte value)
        {
            SelfActive(true);
            gameRef.uiRef.ShowInteractCanvas(false);
            StartCoroutine(DisplayWinSequenceBasedIndex(value));
        }

        private IEnumerator DisplayWinSequenceBasedIndex(byte Value)
        {

            yield return StartCoroutine(TypeText(gameRef.gameContent.textStatements[Value]));
            yield return new WaitForSeconds(1.5f);
            SelfActive(false);
        }

        private IEnumerator TypeText(string statement)
        {
            textDisplay.text = ""; // Clear the text before starting the typewriter effect

            foreach (char letter in statement)
            {
                textDisplay.text += letter; // Add one character at a time
                yield return new WaitForSeconds(0.05f); // Adjust speed of the typing effect
            }
        }
        public void SelfActive(bool value) => Self.SetActive(value);
    }
}
