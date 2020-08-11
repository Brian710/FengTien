using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPath
{
    public Quest startEvent;
    public Quest endEvent;

    public QuestPath(Quest from, Quest to)
    {
        startEvent = from;
        endEvent = to;
    }
}
