using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DataHolder m_data;


    public void _MovePlayer()
    {

        if (m_data.m_currunt_index==-1)
        {
            transform.position = new Vector3(0f, 0f, -1.5f);
            return;
        }

        transform.position = m_data.m_genrated_road_blocks[m_data.m_currunt_index].transform.position;
    }
}
