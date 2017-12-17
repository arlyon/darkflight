using UnityEngine;

namespace Control.Input
{
    /// <inheritdoc />
    /// <summary>
    /// The controller manager.
    /// </summary>
    [RequireComponent(typeof(InputManager))]
    public abstract class DeviceInput : MonoBehaviour
    {
        /// <summary>
        /// The input data.
        /// </summary>
        protected InputData Data;

        /// <summary>
        /// The input manager.
        /// </summary>
        protected InputManager Manager;

        /// <summary>
        /// A boolean check for whether there is new input data.
        /// </summary>
        protected bool HasNewData;

        /// <summary>
        /// Called before Start() to set up references and inirialize.
        /// </summary>
        protected virtual void Awake()
        {
            Manager = GetComponent<InputManager>();
            Data = new InputData(Manager.AxisNames, Manager.ButtonNames);
        }
    }
}