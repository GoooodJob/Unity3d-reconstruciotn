using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.	Geomagic X Position
/// 2.	D1 displacement of the Sensor
/// 3.	D2 displacement of the Sensor
/// 4.	D3 displacement of the Sensor
/// 5.	Proximity data of the Sensor
/// 6.	Direction of movement(0 right, 1 left)
/// 7.	Number of Repetition(from 1 to 12)
/// 8.	Crack Type(0 for no crack, 1 for crack, 2 for bump and 3 for wavy pattern)
/// 9.	Number of Experiment(from 0 to 5)

/// </summary>


public class csvLines
{
    public int GeoX;
    public int D1;
    public int D2;
    public int D3;
    public int Proxy;
    public int Direction;
    public int NoRep;
    public int CrackType;
    public int NoExp;

}


