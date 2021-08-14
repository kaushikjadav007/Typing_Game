using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("UI panels Text and Buttons")]
    public GameObject m_menu_panel;
    public GameObject m_in_gamePanel;
    public GameObject m_game_complete_panel;

    [Header("Buttons")]
    public Button m_play_button;
    public Button m_home_button;
    public Button m_next_button;

    [Header("GameStateEvent")]
    public ScriptEvent m_game_state_event;

    [Space]
    public UiDataContainer m_ui_data;

    private void Start()
    {
        _ButtonListeners();
    }

    private void OnDisable()
    {
        m_play_button.onClick.RemoveListener(_PlaybuttonClicked);
        m_next_button.onClick.RemoveListener(_NextButton);
        m_home_button.onClick.RemoveListener(_HomeButton);
    }

    void _ButtonListeners()
    {
        m_play_button.onClick.AddListener(_PlaybuttonClicked);
        m_next_button.onClick.AddListener(_NextButton);
        m_home_button.onClick.AddListener(_HomeButton);
    }

    private void _PlaybuttonClicked()
    {
        m_game_state_event._Raise(_DataStore.m_game_start);
        m_menu_panel.SetActive(false);
    }

    void _NextButton()
    {
        m_game_complete_panel.SetActive(false);
        m_game_state_event._Raise(_DataStore.m_sentance_change);
    }

    void _HomeButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void _GameStateChangeEvent(string m_state)
    {

     //   Debug.Log(m_state);

        switch (m_state)
        {
            case _DataStore.m_senance_complete:
                m_game_complete_panel.SetActive(true);
                m_next_button.gameObject.SetActive(true);
                break;

            case _DataStore.m_game_complete:
                m_game_complete_panel.SetActive(true);
                m_next_button.gameObject.SetActive(false);
                break;
        }
    }

}

public class _DataStore
{
    public   const string m_game_start = "m_game_start";
    public  const string m_game_complete = "m_game_complete";
    public const string m_senance_complete = "m_senance_complete";
    public const string m_sentance_change = "m_sentance_change";
    public  const string m_game_restart = "m_game_restart";
}