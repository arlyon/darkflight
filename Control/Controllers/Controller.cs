// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Controller.cs" company="">
//   
// </copyright>
// <summary>
//   The base controller class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Control.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// The base controller class.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Controller : MonoBehaviour
    {
        /// <summary>
        /// The new input.
        /// </summary>
        protected bool NewInput;

        /// <summary>
        /// The rigid body attached to the character.
        /// </summary>
        protected Rigidbody RigidBody;

        /// <summary>
        /// Reads the inputdata passed from the Input Manager.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public abstract void SendInput(InputData data);

        /// <summary>
        /// The awake.
        /// </summary>
        protected void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
        }
    }
}