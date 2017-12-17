using System;
using UnityEngine;

namespace Resource
{
    /// <summary>
    /// The interface for resources in the ship.
    /// </summary>
    public struct Resource
    {

        private readonly Action<long> _dispose;
        
        public Resource(string name, Color color, Action<long> disposeFunc) : this()
        {
            this.Name = name;
            this.Color = color;
            this._dispose = disposeFunc;
        }
        
        /// <summary>
        /// The name of the resource.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// The color for the resource.
        /// </summary>
        public Color Color { get; private set; }

        public void Dispose(long units)
        {
            _dispose(units);
        }
    }
}