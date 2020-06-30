using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThereminAntennaBySpherecast : ThereminAntennaByVectorsAbstract
{
    public LayerMask m_collisionMask = 1;
    public float m_cylinderRadius=1;
    public float m_segmentCheck = 10;

    [Header("Debug")]
    public Collider [] m_hits;
    public Collider [] m_sortedHits;
    public GameObject [] m_sortedObjects;
    public Collider selectedObject;

    public void Update()
    {
        ResetCalculatedValue();
        Vector3 direciton = m_end.position - m_start.position;
        m_hits = Physics.OverlapCapsule(m_start.position, m_end.position,
            m_cylinderRadius,
           m_collisionMask
            );
        if (m_hits.Length == 0) {
            selectedObject = null;
            return;
        }
        Vector3 hitPoint;
        float distance;

        GetNearestCollider(m_hits, out  distance, out hitPoint, out selectedObject);

        m_lastCalculated.SetAsFound( true );
        m_lastCalculated.SetDistance(  distance );
        m_lastCalculated.SetNearestPoint( hitPoint );
        Debug.DrawLine(m_start.position, hitPoint, Color.green, Time.deltaTime);
        Debug.DrawLine(m_end.position, hitPoint, Color.green, Time.deltaTime);


    }
    public void GetNearestCollider(Collider [] collider, out float distance, out Vector3 hit, out Collider selection)
    {
        distance= float.MaxValue;
        float tmpDistance;
        Vector3 tmpHit;
        hit = Vector3.zero;
        selection = null;
        for (int i = 0; i < collider.Length; i++)
        {
            GetMinDistanceOf(collider[i], out tmpDistance, out tmpHit);
            if (tmpDistance < distance) {
                hit = tmpHit;
                selection = collider[i];
                distance = tmpDistance;
            }

        }
        

    }
    public void GetMinDistanceOf(Collider collider, out float distance, out Vector3 hit) {
        distance = float.MaxValue;
        hit = Vector3.zero;
        Vector3 start = m_start.position;
        Vector3 direction = m_end.position - m_start.position;
        Vector3 segDistance = direction / (float) m_segmentCheck;
        Vector3 closestPoint =  Vector3.zero;
        float tmpDistance=0;
        Vector3 pointToCheck;
        for (int i = 0; i < m_segmentCheck; i++)
        {
            pointToCheck = start + segDistance * i;
            closestPoint = collider.ClosestPoint(pointToCheck);
            tmpDistance = Vector3.Distance(pointToCheck, closestPoint);
            if (tmpDistance < distance) {
                hit = closestPoint;
                distance = tmpDistance;
            }
        }

    }

}
