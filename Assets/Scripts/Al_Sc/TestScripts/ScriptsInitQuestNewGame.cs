using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ScriptsInitQuestNewGame : MonoBehaviour
{
    [Header("Settings")]
    public SO_QuestCreator ScriptObjectQuestName;
    public float timeToAddSc = 1.5f; 
    void Start()
    {
        StartCoroutine(StartIn(timeToAddSc));
    }

    IEnumerator StartIn(float timeD)
    {
        yield return new WaitForSeconds(timeD);

        Manager_Quest.Instance.StartQuest(ScriptObjectQuestName);
    }
}
