using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypingManager : MonoBehaviour
{
    [Header("INPUT TEXT")]
    public string m_sentance;
    [Space]
    public TMP_InputField m_input_field;
    [Space]
    public List<string> m_currunt_list;


    public bool m_wait_till_input;
    [Header("Sriptable Data holder")]
    public DataHolder m_data_holder;

    [Header("Sriptable Events")]
    public ScriptEvent m_value_change_event;
    public ScriptEvent m_player_move;
    public ScriptEvent m_game_state_event;

    [Space]
    public bool m_input_enable;

    private int m_count;




    private void OnEnable()
    {
        m_input_field.onValueChanged.AddListener(_ValueChanged);
    }


    private void OnDisable()
    {
        m_input_field.onValueChanged.RemoveListener(_ValueChanged);
    }


    public void _GameStateChangeEvent(string m_state)
    {

     //   Debug.Log(m_state);

        switch (m_state)
        {
            case _DataStore.m_game_start:
                _StartGame();
                break;

            case _DataStore.m_sentance_change:
                _SetupSentance();
                break;
        }
    }


    public void _StartGame()
    {
        m_data_holder.m_currunt_index = 0;
        m_data_holder.m_currunt_sentance_no = 0;
        _SetupSentance();
    }


    public void _SetupSentance()
    {

        m_data_holder.m_currunt_index = -1;
        m_player_move._Raise();
        m_data_holder.m_currunt_index = 0;

        m_sentance = m_data_holder.m_all_sentances[m_data_holder.m_currunt_sentance_no];
        m_count = m_sentance.Length;
        m_currunt_list.Clear();


        for (int i = 0; i < m_count; i++)
        {
            m_currunt_list.Add(m_sentance[i].ToString());
        }

        m_count = m_currunt_list.Count;
        m_data_holder.m_currunt_sentance_data = m_currunt_list;
        m_data_holder._GenratePathObjects();
        m_input_enable = true;
    }

    private void _ValueChanged(string m_value)
    {
        if (m_data_holder.m_currunt_index >= m_count)
        {
            _SentanceFinished();
            return;
        }


        if (!m_input_enable)
        {
            return;
        }

     //   Debug.Log(m_currunt_list[m_data_holder.m_currunt_index] + "   " + m_value);

        if (m_value ==m_currunt_list[m_data_holder.m_currunt_index])
        {
           // Debug.Log("Right");
            //SET PATH TO GREEN
            m_player_move._Raise();
            m_data_holder._ColmpletedPath();
            m_input_field.text = "";
        }
        else
        {
           // Debug.Log("Wrong ");
            m_input_field.text = "";
        }
    }

    /// <summary>
    /// CHange To next Sentance
    /// </summary>
    void _SentanceFinished()
    {
        m_input_enable = false;
        m_data_holder.m_currunt_sentance_no++;
      //  Debug.Log(m_data_holder.m_all_sentances.Count);

        if (m_data_holder.m_currunt_sentance_no >= m_data_holder.m_all_sentances.Count)
        {
            Debug.Log("Finished All Sentances");
            m_game_state_event._Raise(_DataStore.m_game_complete);
            return;
        }

        m_game_state_event._Raise(_DataStore.m_senance_complete);
    }
}
