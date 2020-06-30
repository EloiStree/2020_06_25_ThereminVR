using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class ThereminAntennaByVectorsAbstract : MonoBehaviour
{

    public Transform m_start, m_end;
    private List<Vector3> m_observedPoint = new List<Vector3>();
    public bool m_useDebugDraw;
    public Color m_debugColor;

    public void Clear()
    {
        m_observedPoint.Clear();
    }

    public void SetVectorStored(int count)
    {

        if (count <= 0)
        {
            m_observedPoint.Clear();
            return;
        }

        while (count != m_observedPoint.Count)
        {
            if (count < m_observedPoint.Count)
                m_observedPoint.RemoveAt(m_observedPoint.Count - 1);
            if (count > m_observedPoint.Count)
                m_observedPoint.Add(new Vector3());
        }

    }
    public int GetVectorAvailable()
    {
        return m_observedPoint.Count;
    }
    public void GetVectorRef(int index, ref Vector3 point)
    {
        if (index < 0 && index >= m_observedPoint.Count)
            throw new System.ArgumentOutOfRangeException();
        point = m_observedPoint[index];
    }
    public static float GetPointDistanceFromLine(ref Vector3  point,ref Transform start, ref Transform end)
    {
        Vector3 a = start.position, b = end.position;
        Vector3 c = point;
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;
        return Vector3.Cross(side1, side2).magnitude;
    }
    public static float GetTriangleNormal(ref Vector3 a,ref  Vector3 b,ref  Vector3 c)
    {
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;
        return Vector3.Cross(side1, side2).magnitude;
    }
    public float GetDistanceOfTheremin(ref Vector3 worldPoint)
    {
        return GetDistanceOfTheremin(ref worldPoint, ref m_start, ref m_end);
    }
    public float GetDistanceOfTheremin( Vector3 worldPoint)
    {
        return GetDistanceOfTheremin(ref worldPoint, ref m_start, ref m_end);
    }

    public static float GetDistanceOfTheremin(ref Vector3 worldPoint, ref Transform start, ref Transform end)
    {

        float startDistance = Vector3.Distance(start.position, worldPoint);
        float endDistance = Vector3.Distance(end.position, worldPoint);
        float hypothenusDistance = Vector3.Distance(start.position, end.position);
        if (startDistance > hypothenusDistance)
            return endDistance;
        if (endDistance > hypothenusDistance)
            return startDistance;
        else return GetPointDistanceFromLine(ref worldPoint,ref start, ref end);
    }

    public ThereminNearestPoint GetLastComputedTheremin() { return m_lastCalculated; }
    
    [SerializeField]
    protected ThereminNearestPoint m_lastCalculated = new ThereminNearestPoint();
    protected void ResetCalculatedValue() {

        m_lastCalculated.ResetToDefault();
    }
}

[System.Serializable]
public class ThereminNearestPoint{

    [SerializeField]
    private bool m_found;
    [SerializeField]
    private Vector3 m_worldPosition;
    [SerializeField]
    private Vector3 m_thereminPosition;
    [SerializeField]
    private float m_distance;

    public bool HasNearestPoint() { return m_found; }
    public bool HasNearestPoint(float maxDistance) {
        return m_found && m_distance < maxDistance; 
    }

    public Vector3 GetWorldPositionOnTheremin() { return m_thereminPosition; }

    public Vector3 GetWorldPositionNearest(){ return m_worldPosition; }
    public float GetDistanceOfTheTheremin(){ return m_distance; }

    public void SetAsFound(bool value)
    {m_found = value;}

    public void ResetToDefault() {
        m_found = false;
        m_worldPosition = Vector3.zero;
        m_distance = float.MaxValue;
    }

    public void SetDistance(float distance)
    {
        m_distance = distance;
    }

    public void SetNearestPoint(Vector3 hitPoint)
    {
        m_worldPosition = hitPoint;
    }
    public void SetThereminStartPoint(Vector3 hitPoint)
    {
        m_thereminPosition = hitPoint;
    }
}
