using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DialogMode
{
    Subtitle,
    Interrupt
}

[System.Serializable]
public class OCCollectionQuestItem
{
    public string idItem;
    public int valueItem;
}

[System.Serializable]
public class OCNpcTalk
{
    public string npcName;
    public DialogMode dialogMode;
    [TextArea] public string npcT;
    public AudioClip npcV;
}

[System.Serializable]
public class OCRewardQuest
{
    public int scienceCredit;
}

public enum QuestCompletionMode
{
    Dialog,
    EnterArea,
    PressButton,
    CollectItem
}

[System.Serializable]
public class OCStepQuest
{
    [Header("Identity")]
    public string stepId;

    [Header("UI")]
    public string subTitleQuest;

    [Header("Completion")]
    public QuestCompletionMode completionMode;

    [Header("Collect Item (optional)")]
    public OCCollectionQuestItem objective; // default
    public string itemId;
    public int requiredAmount;

    [Header("Area / Button (optional)")]
    public string triggerId;

    [Header("Dialog")]
    public List<OCNpcTalk> npcTalkBefore;
    public List<OCNpcTalk> npcTalkAfter;

    [Header("Reward")]
    public OCRewardQuest rewardPerStep;
}

[CreateAssetMenu(fileName = "Story_", menuName = "Quest/Quest creator")]
public class SO_QuestCreator : ScriptableObject
{

    


    // [System.Serializable]
    // public class OCStepQuest
    // {
    //     public string subTitleQuest;
    //     public OCCollectionQuestItem objective;
    //     public List<OCNpcTalk> npcTalkBefore = new List<OCNpcTalk>();
    //     public List<OCNpcTalk> npcTalkAfter = new List<OCNpcTalk>();
    //     public OCRewardQuest rewardPerStep;
    //     public QuestCompletionMode completionMode;
    //     public bool isDone;
    // }

    // public string questTitle;
    // public string questDescription;
    // public List<OCStepQuest> questStep;
    // // public List<OCStepQuest> questStep = new List<OCStepQuest>();
    [Header("Identity")]
    public string questId;

    [Header("UI")]
    public string questTitle;
    [TextArea] public string questDescription;

    [Header("Steps")]
    public List<OCStepQuest> questStep;
    
}
