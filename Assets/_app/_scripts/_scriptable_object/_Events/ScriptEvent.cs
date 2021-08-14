using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="SriptableEvent",menuName = "ScriptableObjects/Event", order =100)]

public class ScriptEvent : ScriptableObject
{
    public List<GameEventListener> m_listerners = new List<GameEventListener>();

    public void _Raise()
    {
        foreach (GameEventListener item in m_listerners)
        {
            item._InvokeEvent();
        }
    }

    public void _Raise(string m_string_param)
    {
        foreach (GameEventListener item in m_listerners)
        {
            item._InvokeEvent(m_string_param);
        }
    }

    public void _RegisterEvent(GameEventListener m_game_event)
    {
        if (!m_listerners.Contains(m_game_event))
        {
            m_listerners.Add(m_game_event);
        }
    }


    public void _DeRegisterEvent(GameEventListener m_game_event)
    {
        if (m_listerners.Contains(m_game_event))
        {
            m_listerners.Remove(m_game_event);
        }
    }
}

[System.Serializable]
public class _MyEvent : UnityEvent<string>
{

}
