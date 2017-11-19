using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlockingObject
{
    float AlignmentWeight { get; set; }
    float CohesionWeight { get; set; }
    float SeparationWeight { get; set; }
    float PlayerWeight { get; set; }
    float Radius { get; set; }
    bool DoUpdate { get; set; }

    void UpdateDirection(Vector3 direction);
}
