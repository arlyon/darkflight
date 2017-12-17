using System.Collections.Generic;
using Resource;
using UnityEngine;
using Resources = Resource.Resources;

namespace Components
{
    public class Generator : MonoBehaviour, IGenerator
    {

        private ResourceBank _generates;
        
        public bool Enabled { get; set; }
        
        private void Start()
        {
            this._generates = ResourceBank.Empty;
            this._generates.Add(Resources.Power, 1000);
        }

        public ResourceBank WillGenerate()
        {
            return _generates;
        }

        public ResourceBank Take(ResourceBank maxAmount)
        {
            return maxAmount;
        }
    }
}