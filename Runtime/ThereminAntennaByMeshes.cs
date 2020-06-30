using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThereminAntennaByMeshes : ThereminAntennaByVectorsAbstract
{

    public SkinnedMeshRenderer [] m_trackedRenderer;
    [Header("Debug")]
    public RenderToTrack[] m_tracked;

    [System.Serializable]
    public class RenderToTrack { 

        public SkinnedMeshRenderer m_render;
        public int m_meshPointCount=0;
        public UnityEngine.Mesh m_mesh=null;
        public Vector3[] m_meshPoint= new Vector3[0];
        public Vector3[] m_worldSpaceMesh = new Vector3[0];
        public Vector3 m_nearestWorldPoint = new Vector3();
        public Vector3 m_thereminWorldPoint = new Vector3();
        public Vector3 m_meshPosition = new Vector3();
        public Quaternion m_meshRotation = Quaternion.identity;
        public float m_distance=float.MaxValue;
    }
    private void Awake()
    {

        m_tracked = new RenderToTrack[m_trackedRenderer.Length];
        for (int i = 0; i < m_trackedRenderer.Length; i++)
        {
            m_tracked[i] = new RenderToTrack(); 
            m_tracked[i].m_render = m_trackedRenderer[i];
            SetBakedToTrack(ref m_tracked[i]);
        }

    }

    private static void SetBakedToTrack(ref RenderToTrack t)
    {
        t.m_mesh = new Mesh();
        t.m_render.BakeMesh(t.m_mesh);
        t.m_worldSpaceMesh = new Vector3[t.m_mesh.vertexCount];
    }

    public void Update()
    {
        ResetCalculatedValue();
        if (m_tracked.Length == 0)
            return;
        for (int j = 0; j < m_tracked.Length; j++)
        {
            RenderToTrack t = m_tracked[j];
            t.m_meshPosition = t.m_render.transform.position;
            t.m_meshRotation = t.m_render.transform.rotation;
            t.m_render.BakeMesh(t.m_mesh);
            t.m_meshPoint = t.m_mesh.vertices;
            for (int i = 0; i < t.m_meshPoint.Length; i++)
            {
                RelocateToWorld(ref t.m_meshPoint[i], ref t.m_worldSpaceMesh[i]

                    ,ref t.m_meshPosition, ref t.m_meshRotation);
            }
            if (t.m_worldSpaceMesh.Length == 0) {
                return; 
            }
            t.m_distance = float.MaxValue;
            float minDistanceTmp = float.MaxValue;
            for (int i = 0; i < t.m_worldSpaceMesh.Length; i++)
            {
               minDistanceTmp = GetDistanceOfTheremin(ref t.m_worldSpaceMesh[i]);
                if(minDistanceTmp<t.m_distance )
                {
                    t.m_nearestWorldPoint = t.m_worldSpaceMesh[i];
                    t.m_distance = minDistanceTmp; 
                }
            }
        }

        RenderToTrack tt = m_tracked.OrderBy(k => k.m_distance).First();
        Vector3 m_nearestWorldPoint = tt.m_nearestWorldPoint;
        Vector3 m_thereminPoint = tt.m_thereminWorldPoint;

        m_lastCalculated.SetAsFound(true);
        m_lastCalculated.SetNearestPoint(m_nearestWorldPoint);
        m_lastCalculated.SetThereminStartPoint();
        m_lastCalculated.SetDistance(GetDistanceOfTheremin(m_nearestWorldPoint));
        Debug.DrawLine(m_start.position, m_nearestWorldPoint, Color.blue, Time.deltaTime);
        Debug.DrawLine(m_end.position, m_nearestWorldPoint, Color.blue, Time.deltaTime);

    }


    Vector3 p;
    private void RelocateToWorld(ref Vector3 local, ref Vector3 relocated, ref Vector3 meshPosition, ref Quaternion meshRotation)
    {
        p = meshRotation * local;
        relocated.x = meshPosition.x + p.x;
        relocated.y = meshPosition.y + p.y;
        relocated.z = meshPosition.z + p.z;
    }
   

   
}
