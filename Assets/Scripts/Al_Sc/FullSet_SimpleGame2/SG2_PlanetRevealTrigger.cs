using UnityEngine;

public class SG2_PlanetRevealTrigger : MonoBehaviour
{
    public string factId;
    public Outline outline;
    public string[] QstoryId;


    // void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;

    //     foreach (var o in GetComponentsInChildren<Outline>())
    //         o.enabled = true;

    //     SG2_PlanetManager.Instance.RevealFact(factId);
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;

    //     foreach (var o in GetComponentsInChildren<Outline>())
    //         o.enabled = false;

    //     SG2_PlanetManager.Instance.RevealFact(factId);
    // }


    // void Start()
    // {
    //     Game_TriggerEventStatic.OutlineActive += ActiveOulineCurrentMission;
    // }

    void OnEnable()
    {
        Game_TriggerEventStatic.OutlineActive += ActiveOulineCurrentMission;
    }

    void OnDisable()
    {
        Game_TriggerEventStatic.OutlineActive -= ActiveOulineCurrentMission;
    }

    public void ActiveOulineCurrentMission()
    {
        if (!SG2_PlanetManager.Instance.IsRightPlanetFact(factId)) return;

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
        }

        outline.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SG2_PlanetManager.Instance.RevealFact(factId);

        RunStoryQuest();

        gameObject.SetActive(false);
    }

    public void RunStoryQuest()
    {
        if (QstoryId != null)
        {
            foreach (var stepEldge in QstoryId)
            {
                ST_QuestEventsV1.OnEnterArea?.Invoke(stepEldge);
            }
        }
    }
}
