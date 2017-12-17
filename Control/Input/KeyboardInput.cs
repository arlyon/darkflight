using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Control.Input
{
    /// <inheritdoc />
    /// <summary>
    /// The keyboard input.
    /// </summary>
    [RequireComponent(typeof(InputManager))]
    public class KeyboardInput : DeviceInput
    {
        /// <summary>
        /// A dictionary mapping the physical axes to the axis name.
        /// </summary>
        public Dictionary<string, string> PhysicalAxes;

        /// <summary>
        /// A dictionary mapping the buttons to their name.
        /// </summary>
        public Dictionary<KeyCode, string> ButtonKeys;

        /// <summary>
        /// A dictionary mapping the virtual axis to the axis name.
        /// </summary>
        [SerializeField]
        public Dictionary<VirtualAxis, string> VirtualAxes;

        protected override void Awake()
        {
            base.Awake();
            
            PhysicalAxes = new Dictionary<string, string> {
                {"MouseX", "Pitch"}, 
                {"MouseY", "Yaw"}
            };

            ButtonKeys = new Dictionary<KeyCode, string>
            {
                {KeyCode.Space, "Afterburner"},
                {KeyCode.LeftShift, "Dampen"}
            };

            VirtualAxes = new Dictionary<VirtualAxis, string>
            {
                {new VirtualAxis {Positive = KeyCode.W, Negative = KeyCode.S}, "Thruster"},
                {new VirtualAxis {Positive = KeyCode.A, Negative = KeyCode.D}, "Roll"}
            };
        }
        
        /// <summary>
        /// If there is any input data, pass it on to the input manager.
        /// </summary>
        private void Update()
        {
            HandleButtons();
            HandleVirtualAxes();
            HandlePhysicalAxes();
            HandleStringInput();

            if (!HasNewData) return;
            Manager.PassInput(Data);
            HasNewData = false;
            Data.Clear();
        }

        private void HandleStringInput()
        {
            if (UnityEngine.Input.inputString == string.Empty) return;
            Data.InputString = UnityEngine.Input.inputString;
            HasNewData = true;
        }

        private void HandlePhysicalAxes()
        {
            foreach (var axis in PhysicalAxes.Where(axis => Math.Abs(UnityEngine.Input.GetAxisRaw(axis.Key)) > 0.05f))
            {
                Data.Axes[axis.Value] = UnityEngine.Input.GetAxisRaw(axis.Key);
                HasNewData = true;
            }
        }

        private void HandleVirtualAxes()
        {
            foreach (var axis in VirtualAxes.Where(axis => Math.Abs(axis.Key.GetValue()) > 0.05f))
            {
                Data.Axes[axis.Value] = axis.Key.GetValue();
                HasNewData = true;
            }
        }

        private void HandleButtons()
        {
            foreach (var button in ButtonKeys.Where(button => UnityEngine.Input.GetKey(button.Key)))
            {
                Data.Buttons[button.Value] = true;
                HasNewData = true;
            }
        }
    }

    /// <summary>
    /// A struct to simulate axis keys.
    /// </summary>
    [Serializable]
    public struct VirtualAxis
    {
        /// <summary>
        /// The positive axis button.
        /// </summary>
        public KeyCode Positive;

        /// <summary>
        /// The negative axis button.
        /// </summary>
        public KeyCode Negative;

        /// <summary>
        /// Returns one if positive is pressed, minus one for
        /// negative and zero if both or none are pressed.
        /// </summary>
        /// <returns></returns>
        public float GetValue()
        {
            return (UnityEngine.Input.GetKey(Positive) ? 1 : 0) -
                   (UnityEngine.Input.GetKey(Negative) ? 1 : 0);
        }
    }
}