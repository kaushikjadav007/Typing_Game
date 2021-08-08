using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoadHolder", menuName = "ScriptableObjects/RoadHolder", order = 100)]
public class DataHolder : ScriptableObject
{
    public GameObject m_road_prefab;
    [Header("All the sentances")]
    [Space]
    public List<string> m_all_sentances;

    [Header("Runtime genrated Values")]
    public List<string> m_currunt_sentance_data;

    [Header("Genrated Path Objects")]
    [Space]
    public List<Path> m_genrated_road_blocks;
    [Header("Flot values")]
    [Space]
    public int m_currunt_index;
    public int m_currunt_sentance_no;


    public void _GenratePathObjects()
    {

        int m_count = m_all_sentances[m_currunt_sentance_no].Length;

        //DELETE LAST PATH
        foreach (Path item in m_genrated_road_blocks)
        {
            if (item!=null)
            {
                Destroy(item.gameObject);
            }

        }

        Vector3 m_pos=Vector3.zero;
        Path m_p;
        m_genrated_road_blocks.Clear();

        //GENRATE NEW PATH
        for (int i = 0; i < m_count; i++)
        {
            GameObject go = Instantiate(m_road_prefab, m_pos, Quaternion.identity);

            go.transform.localScale = new Vector3(1f, 1f, 0.95f);
            m_p = go.GetComponent<Path>();
            m_genrated_road_blocks.Add(m_p);

            go.name = m_currunt_sentance_data[i];

            //DATA FILL
            m_p._InitialSetup(m_currunt_sentance_data[i]);

            m_pos.z += 1;
        }
    }

    public void _ColmpletedPath()
    {
        m_genrated_road_blocks[m_currunt_index].GetComponent<I_Walkable>()._Completed();
        m_currunt_index++;
    }
}
