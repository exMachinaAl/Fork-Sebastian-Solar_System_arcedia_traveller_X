using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

// [System.Serializable]
// public class OCQuestCategory
// {
//     public List<SO_StoryQuest> storyQuest = new List<SO_StoryQuest>();
//     public List<SO_QuestCreator> mainQuest = new List<SO_QuestCreator>();
//     public List<SO_QuestCreator> sideQuest = new List<SO_QuestCreator>();
// }
public enum QuestState
{
    Inactive,
    Active,
    Completed
}

[System.Serializable]
public class QuestStepRuntime
{
    public string stepId;
    public bool completed;
}
public enum QuestExecutionState
{
    Idle,
    WaitingDialog,
    WaitingCondition,
    Transition
}


[System.Serializable]
public class QuestRuntime
{
    public SO_QuestCreator questData;
    public QuestExecutionState execState;
    public int currentStepIndex;
    public QuestState state;
    public List<QuestStepRuntime> stepStates;
}



public class Manager_Quest : MonoBehaviour
{
    public static Manager_Quest Instance;

    [SerializeField] private AudioSource audioNpcTalk;
    public QuestRuntime activeQuest;

    void Awake()
    {
        if (Instance == null && Manager_Quest.Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    // Quest activeQuest;
    // int count;
    // int mainStepQst;
    // int numStepQst;
    // bool objectiveDone = false;
    // SO_QuestCreator currentQst;
    // OCStepQuest currentStepQst;

    //sysytem testing
    // public GameObject planePrefab;



    //### new Mnahaer QuestV2
    bool IsDialogOnlyStep(OCStepQuest step)
    {
        return step.completionMode == QuestCompletionMode.Dialog;
    }

    
    QuestRuntime CreateRuntime(SO_QuestCreator data)
    {
        QuestRuntime runtime = new QuestRuntime();
        runtime.questData = data;
        runtime.currentStepIndex = 0;
        runtime.state = QuestState.Active;

        runtime.stepStates = new List<QuestStepRuntime>();
        foreach (var step in data.questStep)
        {
            runtime.stepStates.Add(new QuestStepRuntime
            {
                stepId = step.stepId,
                completed = false
            });
        }

        return runtime;
    }
    public void StartQuest(SO_QuestCreator quest)
    {
        activeQuest = CreateRuntime(quest);

        // Manager_UI.Instance.SetCurrentQuest(quest.questTitle, quest.questDescription);

        ActivateCurrentStep();
    }

    void ActivateCurrentStep()
    {
        var step = GetCurrentStep();

        Debug.Log("Step aktif: " + step.subTitleQuest);
        // Manager_UI.Instance.HintNextProgessStep(step.subTitleQuest);

        if (step.npcTalkBefore != null && step.npcTalkBefore.Count > 0)
        {
            activeQuest.execState = QuestExecutionState.WaitingDialog;

            PlayDialogue(step.npcTalkBefore, () =>
            {
                // 🔑 JIKA STEP HANYA DIALOG → SELESAIKAN LANGSUNG
                if (IsDialogOnlyStep(step))
                {
                    CompleteStep();
                }
                else
                {
                    activeQuest.execState = QuestExecutionState.WaitingCondition;
                }
            });
        }
        else
        {
            activeQuest.execState = QuestExecutionState.WaitingCondition;
        }
    }


    void OnDialogBeforeFinished()
    {
        activeQuest.execState = QuestExecutionState.WaitingCondition;
    }

    void CompleteStep()
    {
        var step = GetCurrentStep();

        StartCoroutine(CompleteStepRoutine(step));
    }

    IEnumerator CompleteStepRoutine(OCStepQuest step)
    {
        activeQuest.execState = QuestExecutionState.Transition;

        // Dialog AFTER (jika ada)
        if (step.npcTalkAfter != null && step.npcTalkAfter.Count > 0)
        {
            bool done = false;

            PlayDialogue(step.npcTalkAfter, () =>
            {
                done = true;
            });

            yield return new WaitUntil(() => done);
        }

        activeQuest.currentStepIndex++;

        if (activeQuest.currentStepIndex >= activeQuest.questData.questStep.Count)
        {
            activeQuest.state = QuestState.Completed;
            Debug.Log("QUEST SELESAI");
        }
        else
        {
            ActivateCurrentStep();
        }
    }


    bool isPlaying = false;
    public void PlayDialogue(List<OCNpcTalk> dialog, Action callback)
    {
        if (isPlaying) return;
        StartCoroutine(DialogRoutine(dialog, () =>
        {
            callback();
        }));
    }

    IEnumerator DialogRoutine(List<OCNpcTalk> dialog, Action callback)
    {
        isPlaying = true;

        foreach (var t in dialog)
        {
            if (t.dialogMode == DialogMode.Interrupt)
            {
                bool clicked = false;

                // Manager_UI.Instance.UIPanelInformation.ShowUI(
                //     "-1",           // ← boleh pakai stepId atau dialogId
                //     t.npcT,
                //     "OK",
                //     () => clicked = true
                // );
                yield return new WaitForSeconds(2f);
                clicked = true;

                yield return new WaitUntil(() => clicked);
            }
            else
            {
                // Manager_UI.Instance.ShowYapping(t.npcT);
                // Subtitle kecil (non-blocking)
                // ShowSubtitle(t.npcT);
                yield return new WaitForSeconds(2f);
                // Manager_UI.Instance.HideYapping();
            }
        }

        isPlaying = false;
        callback?.Invoke();
    }




    void OnEnable()
    {
        ST_QuestEventsV1.OnEnterArea += HandleEnterArea;
        ST_QuestEventsV1.OnButtonPressed += HandleButton;
        ST_QuestEventsV1.OnItemCollected += HandleItemCollected;
        ST_QuestEventsV1.OnDialogFinished += HandleDialogFinished;
    }

    void OnDisable()
    {
        ST_QuestEventsV1.OnEnterArea -= HandleEnterArea;
        ST_QuestEventsV1.OnButtonPressed -= HandleButton;
        ST_QuestEventsV1.OnItemCollected -= HandleItemCollected;
        ST_QuestEventsV1.OnDialogFinished -= HandleDialogFinished;
    }

    OCStepQuest GetCurrentStep()
    {
        return activeQuest.questData.questStep[activeQuest.currentStepIndex];
    }

    void HandleEnterArea(string triggerId)
    {
        var step = GetCurrentStep();
        if (activeQuest.execState != QuestExecutionState.WaitingCondition)
            return;
        if (step.completionMode != QuestCompletionMode.EnterArea) return;
        if (step.triggerId != triggerId) return;

        CompleteStep();
    }

    void HandleButton(string buttonId)
    {
        var step = GetCurrentStep();
        if (activeQuest.execState != QuestExecutionState.WaitingCondition)
            return;
        if (step.completionMode != QuestCompletionMode.PressButton) return;
        if (step.triggerId != buttonId) return;

        CompleteStep();
    }

    Dictionary<string, int> itemProgress = new();
    void HandleItemCollected(string itemId, int amount)
    {
        var step = GetCurrentStep();
        if (activeQuest.execState != QuestExecutionState.WaitingCondition)
            return;
        if (step.completionMode != QuestCompletionMode.CollectItem) return;
        if (step.itemId != itemId) return;

        if (!itemProgress.ContainsKey(itemId))
            itemProgress[itemId] = 0;

        itemProgress[itemId] += amount;

        if (itemProgress[itemId] >= step.requiredAmount)
            CompleteStep();
    }
    void HandleDialogFinished(string stepId)
    {
        var step = GetCurrentStep();
        if (activeQuest.execState != QuestExecutionState.WaitingCondition)
            return;
        if (step.completionMode != QuestCompletionMode.Dialog) return;
        if (step.stepId != stepId) return;

        CompleteStep();
    }
}