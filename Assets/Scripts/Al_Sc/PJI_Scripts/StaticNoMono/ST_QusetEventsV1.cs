using System;

public static class ST_QuestEventsV1
{
    public static Action<string> OnEnterArea;
    public static Action<string> OnButtonPressed;
    public static Action<string, int> OnItemCollected;
    public static Action<string> OnDialogFinished; 
}
