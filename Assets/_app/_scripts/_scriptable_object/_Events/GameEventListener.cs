using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public ScriptEvent m_game_event;
    [Space]
    public UnityEvent m_simple_event;

    [Space]
    public _MyEvent m_event_with_string;

    private void OnEnable()
    {
        if (m_game_event==null)
        {
            Debug.LogError("Null Game Event " + gameObject.name);
            return;
        }

        m_game_event._RegisterEvent(this);
    }

    private void OnDisable()
    {
        if (m_game_event == null)
        {
            Debug.LogError("Null Game Event " + gameObject.name);
            return;
        }

        m_game_event._DeRegisterEvent(this);
    }

    public void _InvokeEvent()
    {
        if (m_simple_event != null)
        {
            m_simple_event.Invoke();
        }
    }

    public void _InvokeEvent(string m_chracter)
    {   
        //Debug.Log(m_chracter);
        m_event_with_string.Invoke(m_chracter);
    }
}


