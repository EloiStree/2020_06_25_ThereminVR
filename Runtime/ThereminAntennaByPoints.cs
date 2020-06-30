using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThereminAntennaByPoints : ThereminAntennaByVectorsAbstract
{
    public Transform[] m_points;
    [Header("Debug")]
    public Transform[] m_pointSortByDistance;
    public Transform m_target;
   

    void Update()
    {
        ResetCalculatedValue();
        if (m_points.Length == 0)
            return;

        m_pointSortByDistance = m_points.OrderBy(k => GetDistanceOfTheremin( k.position)).ToArray();
        m_target = m_pointSortByDistance[0];
        m_lastCalculated.SetAsFound (true);
        m_lastCalculated.SetNearestPoint( m_target.position);
        m_lastCalculated.SetDistance(GetDistanceOfTheremin(m_target.position));
        Debug.DrawLine(m_start.position, m_target.position, Color.cyan, Time.deltaTime);
        Debug.DrawLine(m_end.position, m_target.position, Color.cyan, Time.deltaTime);
        //Vector3 target = m_end.position;
        //IEnumerable<Transform> points = m_points.OrderBy(k => Vector3.Distance(target, k.position));
        //Vector3 p = points.First();
        //
    }
}
