using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class ConvTrigger : MonoBehaviour
{
    public bool active = true;
    public ConvType type;

    [Header("Normal Conversation")]
    public string content;

    [Header("Question Conversation")]
    public string question;
    public string[] acceptableWords;

    [SerializeField] GameObject bubble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active)
            return;
        if (collision.tag.Equals("Player"))
        {
            if (GetComponent<DayTimeSwitch>())
            {
                if (GetComponent<DayTimeSwitch>().active)
                {
                    ConvManager.instance.StartConversation(new Conversation(type, content, question, acceptableWords), this);
                }
                return;
            }
            ConvManager.instance.StartConversation(new Conversation(type, content, question, acceptableWords), this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!active)
            return;
        if (collision.tag.Equals("Player"))
        {
            if (GetComponent<DayTimeSwitch>())
            {
                if (GetComponent<DayTimeSwitch>().active)
                {
                    ConvManager.instance.ExitConversation(new Conversation(type, content, question, acceptableWords));
                }
                return;
            }
            ConvManager.instance.ExitConversation(new Conversation(type, content, question, acceptableWords));
        }
    }

    public void SetStatus(bool status)
    {
        active = status;
        if (!status)
        {
            bubble.SetActive(false);
        }
    }
}

public enum ConvType
{
    Normal,
    Question
}
