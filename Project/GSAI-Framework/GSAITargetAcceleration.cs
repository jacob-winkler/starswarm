﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSwarm.Project.GSAI_Framework
{
    public class GSAITargetAcceleration
    {
        /// <summary>
        /// Gets or sets an angular acceleration value.
        /// </summary>
        public float Angular { get; set; } = 0.0F;

        /// <summary>
        /// Gets or sets a linear acceleration value.
        /// </summary>
        public Vector3 Linear { get; set; } = Vector3.Zero;

        /// <summary>
        /// Sets the linear and angular components to zero.
        /// </summary>
        public void SetZero()
        {
            Linear = Vector3.Zero;
            Angular = 0.0F;
        }

        public void AddScaledAccel(GSAITargetAcceleration accel, float scalar)
        {
            Linear += accel.Linear * scalar;
            Angular += accel.Angular * scalar;
        }

        public float GetMagnitudeSquared()
        {
            return Linear.LengthSquared() + Angular * Angular;
        }

        public float GetMagnitude()
        {
            return Mathf.Sqrt(GetMagnitudeSquared());
        }
    }
}
