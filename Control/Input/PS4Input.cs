using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Control.Input
{
    /// <inheritdoc />
    /// <summary>
    /// The PS4 input.
    /// </summary>
    [RequireComponent(typeof(InputManager))]
    // ReSharper disable once InconsistentNaming
    public class PS4Input: DeviceInput
    {
        /// <summary>
        /// A dictionary mapping the physical axes to the axis name.
        /// </summary>
        public Dictionary<string, string> Axes;

        /// <summary>
        /// A dictionary mapping the buttons to their name.
        /// </summary>
        public Dictionary<KeyCode, string> Buttons;

        /// <summary>
        /// The id of the joystick.
        /// </summary>
        public int JoystickId;
        
        protected override void Awake()
        {
            base.Awake();
            
            Axes = new Dictionary<string, string> {
                {string.Format("J{0}-1", JoystickId), "Yaw"}, 
                {string.Format("J{0}-2", JoystickId), "Pitch"},
                {string.Format("J{0}-3", JoystickId), "Roll"},
                {string.Format("J{0}-6", JoystickId), "Thruster"}
            };

            Buttons = new Dictionary<KeyCode, string>
            {
                {KeyCode.Joystick1Button1, "Afterburner"},
                {KeyCode.Joystick1Button0, "Dampen"}
            };
        }

        /// <summary>
        /// If there is any input data, pass in on to the inpt manager.
        /// </summary>
        private void Update()
        {
            HandleButtons();
            HandleAxes();
            
            if (!HasNewData) return;
            Manager.PassInput(Data);
            HasNewData = false;
            Data.Clear();
        }

        private void HandleAxes()
        {
            foreach (var axis in Axes.Where(axis => Math.Abs(UnityEngine.Input.GetAxisRaw(axis.Key)) > 0.1f))
            {
                Data.Axes[axis.Value] = UnityEngine.Input.GetAxisRaw(axis.Key);
                HasNewData = true;
            }
        }

        private void HandleButtons()
        {
            foreach (var button in Buttons.Where(button => UnityEngine.Input.GetKey(button.Key)))
            {
                Data.Buttons[button.Value] = true;
                HasNewData = true;
            }
        }
    }
}