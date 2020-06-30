using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereminAntenna : MonoBehaviour
{
    public Transform m_start, m_end;
    public ThereminAntennaByVectorsAbstract [] m_thereminChecker;



    public ThereminNearestPoint m_selection = null;
    void Update()
    {
        ThereminNearestPoint selectionTmp;
        m_selection = null;
        for (int i = 0; i < m_thereminChecker.Length; i++)
        {
            selectionTmp= m_thereminChecker[i].GetLastComputedTheremin();

            if (selectionTmp.HasNearestPoint()) {
                if (m_selection == null)
                    m_selection = selectionTmp;
                else if (selectionTmp.GetDistanceOfTheTheremin() < m_selection.GetDistanceOfTheTheremin()) {
                    m_selection = selectionTmp;
                }
            }
        }
        if (m_selection!=null && m_selection.HasNearestPoint()) { 
           
            Debug.DrawLine(m_start.position, m_selection.GetWorldPositionNearest(), Color.red, Time.deltaTime);
            Debug.DrawLine(m_end.position, m_selection.GetWorldPositionNearest(), Color.red, Time.deltaTime);
            Debug.DrawLine(m_selection.GetWorldPositionOnTheremin(), m_selection.GetWorldPositionNearest(), Color.red, Time.deltaTime);

        }


    }



}
