using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereminRadioPitchVolumeDemo : MonoBehaviour
{
    public AudioSource m_audioSource;
    public ThereminAntenna m_soundVolume;
    public float m_soundMaxDistance = 1f;
    public ThereminAntenna m_modifyPitch;
    public float m_modificatorMaxDistance=1f;
    public ThereminNearestPoint m_horizontal;
    public ThereminNearestPoint m_vertical;




    void Update()
    {
        m_horizontal = m_soundVolume.m_selection;
        m_vertical = m_modifyPitch.m_selection;
        m_audioSource.volume = GetTopAntennaInPCT();
        m_audioSource.pitch = GetRightAntennaInPCT();
    }

    public float GetTopAntennaInPCT()
    {
        bool isSetupCorrectlu = m_soundVolume.m_selection != null && m_soundVolume.m_selection.HasNearestPoint();
        if (!isSetupCorrectlu) return 1;
        return m_soundVolume.m_selection.GetDistanceOfTheTheremin() / m_soundMaxDistance;
    }
    public float GetRightAntennaInPCT()
    {
        bool isSetupCorrectlu = m_modifyPitch.m_selection != null && m_modifyPitch.m_selection.HasNearestPoint();
        if (!isSetupCorrectlu) return 1;
        return m_modifyPitch.m_selection.GetDistanceOfTheTheremin() / m_modificatorMaxDistance;
    }
}
