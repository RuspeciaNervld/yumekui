using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance = null;
    public TextAsset csv = null;
    public GameObject dialogueBox;
    public Text dialogueText, nameText;
    public Image speakerImg;
    public struct CsvLine {
        public int blockId;
        public char sign;
        public int id;
        public int to;
        public string speaker;
        public string img;
        public string content;
        public string dub;
        public CsvLine(int blockId, char sign, int id, int to, string speaker, string img, string content, string dub) {
            this.blockId = blockId;
            this.sign = sign;
            this.id = id;
            this.to = to;
            this.speaker = speaker;
            this.img = img;
            this.content = content;
            this.dub = dub;
        }
    }
    [TextArea(1, 3)]
    [SerializeField] private List<string> dialogueLines = null ;
    [SerializeField] private int currentLine;
    [SerializeField] private float textInterval;
    [SerializeField] private bool isScrolling;

    [SerializeField] private List<CsvLine> csvLines = new List<CsvLine>();
    [SerializeField] private int currentIdx;
    [SerializeField] private string currentContent;

    private Coroutine currentTask;

    private void Awake() {
        DialogueManager.Instance = this;
    }

    private void Start() {
        //todo 测试用
        //ShowDialogue(dialogueLines);
        //LoadCsvFile("SL/TestDialogues.csv");
        //ShowDialogueBlock(0);
    }
    private void Update() {
        if (dialogueBox.activeInHierarchy) {
            //if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && dialogueText.text == dialogueLines[currentLine]) {
            //    if (isScrolling == false) {
            //        currentLine++;
            //        if (currentLine < dialogueLines.Count) {
            //            CheckName();
            //            CheckImg();
            //            StartCoroutine(ScrollingText()); //! 开始显示文字
            //        } else {
            //            dialogueBox.SetActive(false);
            //        }
            //    } else {
            //        StopCoroutine(ScrollingText());
            //        dialogueText.text = dialogueLines[currentLine];
            //        isScrolling = false;
            //    }
            //}
            if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) /*&& currentContent == csvLines[preIdx].content*/) {
                if (isScrolling == false) {
                    upDateNextLine();
                    currentTask = StartCoroutine(ScrollingText2());
                    if (currentIdx==0) { //! 这个对话块完成了
                        dialogueBox.SetActive(false);
                    }
                } else {
                    Debug.Log("强制完成");
                    StopCoroutine(currentTask);
                    isScrolling = false;
                    dialogueText.text = currentContent;
                }
            }
        }
    }

    /// <summary>
    /// 已弃用
    /// 提供给外界调用的，输入格式化的字符串就可以完成对话内容展示
    /// n-表示说话的人名，不加表示说话的内容
    /// </summary>
    public void ShowDialogue(List<string> newLines) {
        dialogueLines = newLines;
        currentLine = 0;

        CheckName();
        CheckImg();

        //dialogueText.text = dialogueLines[currentLine];//line by line
        StartCoroutine(ScrollingText());

        dialogueBox.SetActive(true);
    }

    public void ShowDialogueBlock(int index) {
        int i = 0;
        while (csvLines[i].blockId != index) {
            i++;
        }
        currentIdx = i;
        dialogueBox.SetActive(true);
    }
    private void upDateNextLine() {
        switch (csvLines[currentIdx].sign) {
            case '@':
                currentContent = csvLines[currentIdx].content;
                nameText.text = csvLines[currentIdx].speaker;
                if (csvLines[currentIdx].img == "") {
                    speakerImg.sprite = null;
                    speakerImg.color = new Color(255, 255, 255, 75);
                } else {
                    speakerImg.sprite = ResourceManager.Instance.GetAssetCache<Sprite>("Arts/" + csvLines[currentIdx].img);
                    speakerImg.color = new Color(255, 255, 255, 255);
                }
                if (csvLines[currentIdx].dub != "") {
                    AudioClip clip = ResourceManager.Instance.GetAssetCache<AudioClip>("Media/Dub/" + csvLines[currentIdx].dub);
                    AudioManager.Instance.playDub(clip);
                }
                currentIdx = csvLines[currentIdx].to; //! 进入下一句
                break;
            case '#':
                while (csvLines[currentIdx].sign == '#') {
                    //todo 加载出一些选项和功能
                    currentIdx++;
                }
                //todo 之后应该在选项里给出下一句话的指令这里就直接给了
                //todo 假设我们选了最后一个选项
                currentIdx = csvLines[currentIdx].to;
                break;
        }
    }

    /// <summary>
    /// 根据输入内容直接生成一句对话
    /// </summary>
    /// <param name="speaker">说话者名称</param>
    /// <param name="content">说话的内容</param>
    /// <param name="imgPath">立绘路径，为""（空）时不显示立绘</param>
    public void ShowOneLine(string speaker,string content,string imgPath, string dubPath) {
        StartCoroutine(ScrollingText2());
        nameText.text = speaker;
        if (imgPath == "") {
            speakerImg.sprite = null;
            speakerImg.color = new Color(255, 255, 255, 75);
        } else {
            speakerImg.sprite = ResourceManager.Instance.GetAssetCache<Sprite>("Arts/" + imgPath);
            speakerImg.color = new Color(255, 255, 255, 255);
        }
        if (csvLines[currentIdx].dub != "") {
            AudioClip clip = ResourceManager.Instance.GetAssetCache<AudioClip>("Media/Dub/" + dubPath);
            AudioManager.Instance.playDub(clip);
        }
    }

    private void CheckName() {
        if (dialogueLines[currentLine].StartsWith("n-")) {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
    private void CheckImg() {
        if (dialogueLines[currentLine].StartsWith("i-")) {
            string path = dialogueLines[currentLine].Replace("i-", "");
            if(path == "") {
                speakerImg.sprite = null;
                speakerImg.color = new Color(255, 255, 255, 75);
            } else {
                speakerImg.sprite = ResourceManager.Instance.GetAssetCache<Sprite>("Arts/"+path);
                speakerImg.color = new Color(255, 255, 255, 255);
            }
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

    private IEnumerator ScrollingText2() {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in currentContent.ToCharArray()) {
            dialogueText.text += letter;//letter by letter
            yield return new WaitForSeconds(textInterval);
        }
        isScrolling = false;
    }

    public void LoadCsvFile(string path) {
        csv = ResourceManager.Instance.GetAssetCache<TextAsset>(path);
        string[] all = csv.text.Split('\n');
        
        for (int i =1;i<all.Length-1;i++) {
            string[] cell = all[i].Split(',');
            CsvLine csv = new CsvLine(int.Parse(cell[0]), cell[1][0], int.Parse(cell[2]), int.Parse(cell[3]), cell[4], cell[5], cell[6], cell[7]);
            csvLines.Add(csv);
        }
    }

    /// <param name="path">对话csv文件的路径，如："SL/TestDialogues.csv"</param>
    public void init(string path) {
        dialogueBox =GameObject.Find("Canvas/TestUI/DialoguePanel");
        dialogueText = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        nameText = dialogueBox.transform.GetChild(1).GetComponent<Text>();
        speakerImg = dialogueBox.transform.GetChild(2).GetComponent<Image>();

        LoadCsvFile(path);
        //nameText = GameObject.Find("TestDialogue/DialoguePanel/Text_speaker").GetComponent<Text>();
    }

}
