using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DataHolder m_data;
    [Space]
    public bool m_move;
    [Space]
    public Animator m_chiken_anim;
    [Space]
    public float m_speed;

    [Space]
    public float m_incrimental;
    [Space]
    public float m_d;

    private Vector3 m_pos;
    private Vector3 m_temp_pos;

    private bool m_stop_on_last;
    private bool m_onwin_platform;

    public void _MovePlayer()
    {

        //Debug.Log("Move");

        if (m_data.m_currunt_index==-1)
        {
            transform.position = new Vector3(0f, 0f, -1.5f);
            return;
        }

        m_pos = m_data.m_genrated_road_blocks[m_data.m_currunt_index].transform.position;
        m_move = true;
        m_chiken_anim.SetBool("Run", true);
        m_chiken_anim.SetBool("Eat", false);
    }

    private void Update()
    {
        if (!m_move)
        {
            return;
        }


        if (transform.position.z>=m_pos.z)
        {
            m_temp_pos = m_pos;
            transform.position = m_temp_pos;
            m_chiken_anim.SetBool("Run", false);
            m_speed = 2f;
            m_move = false;

            if (m_stop_on_last)
            {
                m_chiken_anim.SetBool("Run", false);
                m_chiken_anim.SetBool("Eat", true);
                m_stop_on_last = false;
            }
        }

        m_d=(Vector3.Distance(transform.position, m_pos));

        if (m_d >2)
        {
            m_speed += Time.deltaTime * m_incrimental;
        }
        else
        {
            if (m_speed>2)
            {
                m_speed -= Time.deltaTime;
            }
        }


        m_temp_pos = transform.position;
        m_temp_pos.z += Time.deltaTime * m_speed;
        transform.position = m_temp_pos;
    }

    public void _GameStateChangeEvent(string m_state)
    {
       // Debug.Log(m_state);

        switch (m_state)
        {
            case _DataStore.m_game_start:
                m_chiken_anim.SetBool("Run", false);
                m_chiken_anim.SetBool("Eat", false);
              
                break;

            case _DataStore.m_sentance_change:

                m_chiken_anim.SetBool("Run", false);
                m_chiken_anim.SetBool("Eat", false);

                break;
                 
            case _DataStore.m_senance_complete:

                Debug.Log("Complete");
                m_stop_on_last = true;
                m_pos = m_data.m_genrated_win_platform.position;
                m_move = true;

                break;
            case _DataStore.m_game_complete:

                m_stop_on_last = true;
                m_pos = m_data.m_genrated_win_platform.position;
                m_move = true;

                break;
        }
    }
}
