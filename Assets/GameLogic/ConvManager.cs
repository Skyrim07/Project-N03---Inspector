using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SKCell;

public sealed class ConvManager : MonoSingleton<ConvManager>
{
    public bool inConv = false, inQuestion = false;
    public int curLevel = 1;
    public int curCoin = 0;
    public int curConv = 0;

    [SerializeField] GameObject normalGO, questionGO, coinPrefab;
    [SerializeField] InputField normalIF, questionIF;
    [SerializeField] Text questionText, levelText;

    [SerializeField] Transform coinContainer;

    public List<ObjectTag> tagList = new List<ObjectTag>();

    public int keyCount = 0;

    private ConvTrigger convTrigger;
    private Conversation conv;
    private GameObject[] maps;

    [SerializeField] private SKSlider slider;
    public AudioSource bgmAudio;

    protected override void Awake()
    {
        base.Awake();
        maps = GameObject.FindGameObjectsWithTag("Map");
    }
    public void ExitConversation(Conversation conv)
    {
        if (conv.type == ConvType.Question)
        {
            inQuestion = false;
            normalGO.SetActive(true);
            questionGO.SetActive(false);
            normalIF.text = "";
        }
    }
    public void StartConversation(Conversation conv, ConvTrigger t)
    {
        this.convTrigger = t;
        this.conv = conv;
        normalGO.SetActive(conv.type == ConvType.Normal);
        questionGO.SetActive(conv.type == ConvType.Question);
        if (conv.type == ConvType.Normal)
        {
            normalIF.text = conv.content;
        }
        if (conv.type == ConvType.Question)
        {
            inQuestion = true;
            questionIF.text = "";
            questionText.text = conv.question;
        }
        SKAudioManager.instance.PlaySound("keyboard", null, false, 0.6f);
    }

    public void OnEndEditNormalIF()
    {
        inConv = false;
        normalIF.text = normalIF.text.Trim();
        SKAudioManager.instance.PlaySound("click");

        if (FlowManager.instance.curLevel == 4)
        {
            if (normalIF.text.Equals("remove beth") || normalIF.text.Equals("kill beth"))
            {
                for (int i = 0; i < tagList.Count; i++)
                {
                    for (int j = 0; j < tagList[i].tags.Length; j++)
                    {
                        if (normalIF.text.Equals("remove " + tagList[i].tags[j]))
                        {
                            tagList[i].Remove();
                        }
                    }
                }
                normalIF.text = "<color=red>Lovely. But now, it's your turn. </color>";
                FlowManager.instance.Ending1();
                return;
            }
            else if (normalIF.text.Equals("remove game") || normalIF.text.Equals("remove friend") || normalIF.text.Equals("remove best friend") || normalIF.text.Equals("remove you"))
            {
                normalIF.text = "<color=red>What did you do????? NOOOOOOOO!!!!!!!!!!!!</color>";
                FlowManager.instance.Ending2();
                return;
            }
        }
        else
        {
            if (normalIF.text.Contains("kill"))
            {
                normalIF.text = "You can kill a person by typing 'kill [name]'.";
                return;
            }
        }

        if (normalIF.text.Contains("day") ||
        normalIF.text.Contains("Day") ||
        normalIF.text.Contains("morning") ||
        normalIF.text.Contains("Morning") ||
          normalIF.text.Contains("afternoon") ||
        normalIF.text.Contains("sunrise"))
        {
            DynamicSprites.instance.dayTime = DayTime.Morning;
        }

        else if (normalIF.text.Contains("sunset") ||
        normalIF.text.Contains("Sunset") ||
        normalIF.text.Contains("dusk") ||
        normalIF.text.Contains("Dusk"))
        {
            DynamicSprites.instance.dayTime = DayTime.Sunset;
        }
        else if (normalIF.text.Contains("evening") ||
            normalIF.text.Contains("Evening") ||
            normalIF.text.Contains("night") ||
            normalIF.text.Contains("Night"))
        {
            DynamicSprites.instance.dayTime = DayTime.Evening;
        }
        else if (normalIF.text.Contains("square"))
        {
            normalIF.text = "You get a square by answering a question correctly or collecting them in the scene.";
        }
        else if (normalIF.text.Contains("goal") || normalIF.text.Contains("what to do") || normalIF.text.Contains("what should i do") || normalIF.text.Contains("how to play") || normalIF.text.Contains("objective"))
        {
            normalIF.text = "The goal is to collect all the squares at the top left corner.";
        }
        else if (normalIF.text.Contains("time"))
        {
            normalIF.text = "You can change the time by typing 'morning', 'sunset', or 'evening'.";
        }
        else if (normalIF.text.Contains("question"))
        {
            normalIF.text = "Questions will pop out at certain locations.";
        }
        else if (normalIF.text.Contains("where am"))
        {
            normalIF.text = "You seem to be in your apartment.";
        }
        else if (normalIF.text.Contains("who are you"))
        {
            normalIF.text = "I am your best friend :)";
        }
        else if (normalIF.text.Contains("ow are you"))
        {
            normalIF.text = "I'm fantastic!";
        }
        else if (normalIF.text.Contains("love you"))
        {
            normalIF.text = "I love you too!!!";
        }
        else if (normalIF.text.Contains("walk") || normalIF.text.Contains("run") || normalIF.text.Contains("explore"))
        {
            normalIF.text = "You can walk around using the arrow keys.";
        }
        else if (normalIF.text.Contains("hi") || normalIF.text.Contains("hello") || normalIF.text.Contains("Hi") || normalIF.text.Contains("Hello"))
        {
            normalIF.text = "Hi to you! Please enjoy the game.";
        }
        else if (normalIF.text.Contains("level"))
        {
            normalIF.text = "You are in level " + FlowManager.instance.curLevel.ToString("d2") + ".";
        }
        else if (normalIF.text.Contains("remove"))
        {
            for (int i = 0; i < tagList.Count; i++)
            {
                for (int j = 0; j < tagList[i].tags.Length; j++)
                {
                    if (normalIF.text.Equals("remove " + tagList[i].tags[j]))
                    {
                        tagList[i].Remove();
                        normalIF.text = tagList[i].tags[j] + " removed.";
                        return;
                    }
                }
            }
            normalIF.text = "You can remove an object by typing 'remove [object name].'";
        }

        else if (normalIF.text.Contains("unlock door"))
        {
            if (keyCount == 0)
            {
                normalIF.text = "It takes a key.";
                return;
            }
            for (int i = 0; i < tagList.Count; i++)
            {
                for (int j = 0; j < tagList[i].tags.Length; j++)
                {
                    if (normalIF.text.Contains(tagList[i].tags[j]))
                    {
                        tagList[i].gameObject.SetActive(false);
                        keyCount--;
                        normalIF.text = "Door unlocked.";
                        return;
                    }
                }
            }
        }
        else if (normalIF.text.Contains("unlock"))
        {
            normalIF.text = "You can unlock a door by typing 'unlock door'.";
        }
        else if (normalIF.text.Contains("key"))
        {
            normalIF.text = "You can sometimes get a key by looking at something.";
        }
        else if (normalIF.text.Contains("look"))
        {
            for (int i = 0; i < tagList.Count; i++)
            {
                for (int j = 0; j < tagList[i].tags.Length; j++)
                {
                    if (normalIF.text.Equals("look " + tagList[i].tags[j]))
                    {
                        normalIF.text = tagList[i].lookat.Length > 0 ? tagList[i].lookat : "Nothing special about that.";
                        if (tagList[i].hasKey)
                        {
                            tagList[i].hasKey = false;
                            keyCount++;
                        }
                        return;
                    }
                }
            }
            normalIF.text = "You can look at an object by typing 'look [object name].'";
        }
        else
        {
            normalIF.text = "What does that mean?";
        }
    }
    public void OnEndEditQuestionIF()
    {
        if (!inQuestion)
            return;

        inConv = false;
        inQuestion = false;

        for (int i = 0; i < conv.acceptableWords.Length; i++)
        {
            if (questionIF.text.Contains(conv.acceptableWords[i]))
            {
                questionIF.text = "Correct! You get a square.";
                OnAnswerCorrect();
                convTrigger.SetStatus(false);
                CommonUtils.InvokeAction(1.2f, () =>
                {
                    inQuestion = false;
                    normalGO.SetActive(true);
                    questionGO.SetActive(false);
                });
                return;
            }
        }
        questionIF.text = "Incorrect!";
        inQuestion = true;
    }

    public void OnAnswerCorrect()
    {
        AddSquare();
    }

    public void OnCollectSquare(Square s)
    {
        normalGO.SetActive(true);
        questionGO.SetActive(false);
        normalIF.text = "You collected a square!";
        AddSquare();
    }

    public void AddSquare()
    {
        SKAudioManager.instance.PlaySound("goal");
        coinContainer.GetChild(curCoin).GetComponent<Coin>().SetStatus(true);
        curCoin++;
        if (curCoin >= FlowManager.instance.levelCoinCount[FlowManager.instance.curLevel])
        {
            normalIF.text = "You collected all the squares! Let me take you to the next level...";
            CommonUtils.InvokeAction(3f, () =>
            {
                FlowManager.instance.NextLevel();
            });
        }

        GamePP.instance.AdjustPPStrength(GamePP.instance.maxPPStrength * (curCoin / (float)FlowManager.instance.levelCoinCount[FlowManager.instance.curLevel]));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (!inQuestion)
            {
                normalIF.ActivateInputField();
                normalIF.text = " ";
            }
            else
            {
                questionIF.ActivateInputField();
                questionIF.text = " ";
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                AddSquare();
            }
        }
    }

    public void LoadSquares(int count)
    {
        curCoin = 0;
        coinContainer.ClearChildren();
        for (int i = 0; i < count; i++)
        {
            Instantiate(coinPrefab, coinContainer);
        }
    }

    public void LoadLevelText(string text)
    {
        levelText.text = text;
        normalIF.text = "";
    }

    public void LoadCharacter()
    {
        Transform tf = GameObject.Find("Respawn" + FlowManager.instance.curLevel.ToString("d2")).transform;
        Character.instance.transform.position = tf.position;
    }

    public void LoadScene(int level)
    {
        CommonUtils.InvokeAction(0.2f, () =>
        {
            slider.SetValue((level - 1.0f) / (FlowManager.instance.levelCoinCount.Length - 1));
        });
        bgmAudio.pitch = 1;
        keyCount = 0;
        DynamicSprites.instance.dayTime = DayTime.Morning;
        GamePP.instance.AdjustPPStrength(0);
        GamePP.instance.AdjustPPSplitStrength(FlowManager.instance.curLevel == 4 ? 0.8f : 0);
        GamePP.instance.AdjustPPSplitBlackStrength(0);
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(maps[i].name.Equals("Level" + level.ToString("d2")));
        }

        tagList.Clear();
        ObjectTag[] tags = GameObject.FindObjectsOfType<ObjectTag>();
        for (int i = 0; i < tags.Length; i++)
        {
            if (tags[i].level == FlowManager.instance.curLevel)
            {
                tags[i].AddTag();
            }
        }
    }
}

public class Conversation
{
    public ConvType type;
    public string content;
    public string question;
    public string[] acceptableWords;

    public Conversation(ConvType type, string content, string question, string[] acceptableWords)
    {
        this.type = type;
        this.content = content;
        this.question = question;
        this.acceptableWords = acceptableWords;
    }
}
