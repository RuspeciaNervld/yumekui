using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : ISingleton<DialogueManager> {
    public GameObject dialogueBox;
    public Text dialogueText, nameText;
    [TextArea(1, 3)]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int currentLine;
    [SerializeField] private float textInterval;
    private bool isScrolling;

    private void Start() {
        CheckName();
        dialogueText.text = dialogueLines[currentLine];
    }
    private void Update() {
        if (dialogueBox.activeInHierarchy) {
            if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && dialogueText.text == dialogueLines[currentLine]) {
                if (isScrolling == false) {
                    currentLine++;
                    if (currentLine < dialogueLines.Length) {
                        CheckName();
                        //dialogueText.text = dialogueLines[currentLine];//line by line
                        StartCoroutine(ScrollingText());
                    } else {
                        dialogueBox.SetActive(false);
                    }
                } else {
                    StopCoroutine(ScrollingText());
                    dialogueText.text = dialogueLines[currentLine];
                    isScrolling = false;
                }
            }
        }
    }
    public void ShowDialogue(string[] newLines) {
        dialogueLines = newLines;
        currentLine = 0;

        CheckName();

        //dialogueText.text = dialogueLines[currentLine];//line by line
        StartCoroutine(ScrollingText());

        dialogueBox.SetActive(true);
    }
    private void CheckName() {
        if (dialogueLines[currentLine].StartsWith("n-")) {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
    private IEnumerator ScrollingText() {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray()) {
            dialogueText.text += letter;//letter by letter
            yield return new WaitForSeconds(textInterval);
        }
        isScrolling = false;
    }
}
