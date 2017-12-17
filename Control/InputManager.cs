﻿using System;
 using Control.Controllers;
using Control.Input;
using UnityEngine;

namespace Control
{
    /// <inheritdoc />
    /// <summary>
    /// The input manager.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// The axis count.
        /// </summary>
        [SerializeField]
        public string[] AxisNames;

        /// <summary>
        /// The button count.
        /// </summary>
        [SerializeField]
        public string[] ButtonNames;

        /// <summary>
        /// The controller.
        /// </summary>
        public Controller Controller;

        /// <summary>
        /// Passes input to the input manager.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public void PassInput(InputData data)
        {
            if (Controller == null) return;
            Controller.SendInput(data);
        }
    }
}