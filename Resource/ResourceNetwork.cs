using System.Collections.Generic;
using System.Linq;
using Components;
using UnityEngine;

namespace Resource
{
    public class ResourceNetwork
    {
        private readonly List<IGenerator> _generators;
        private readonly List<IStorer> _storers;
        private readonly List<IConsumer> _consumers;

        public ResourceNetwork()
        {
            this._generators = new List<IGenerator>();
            this._storers = new List<IStorer>();
            this._consumers = new List<IConsumer>();
        }
        
        /// <summary>
        /// Distributes power.
        /// </summary>
        public void Tick()
        {
            var generated = _generators
                .Select(gen => gen.WillGenerate())
                .Aggregate(ResourceBank.Empty, (sum, next) => sum + next);
            var consumed = _consumers
                .Select(con => con.WillConsume())
                .Aggregate(ResourceBank.Empty, (sum, next) => sum + next);

            var supply = generated - consumed;
            
            // attempt to source resources for shortages
            supply += GetShortage(supply.Where(pair => pair.Value < 0).ToResourceBank());

            // if, for any resource consumed, there isnt enough
            if (!(supply >= consumed))
            {
                // we dont have enough power
                // TODO handle not enough resources
            }
            else
            {
                // we have enough power
                // TODO distribute resources
            }

            var unused = StoreSurplus(supply);
            DisposeUnused(unused);
        }

        private ResourceBank GetShortage(ResourceBank shortage)
        {
            var needed = -shortage;
            return needed;
        }

        private ResourceBank StoreSurplus(ResourceBank supply)
        {
            return ResourceBank.Empty;
        }

        private static void DisposeUnused(ResourceBank supply)
        {
            foreach (var keyValuePair in supply)
            {
                keyValuePair.Key.Dispose(keyValuePair.Value);
            }
        }

        public void Register(IPart part)
        {
            if (part is IConsumer) _consumers.Add((IConsumer) part);
            if (part is IGenerator) _generators.Add((IGenerator) part);
            if (part is IStorer) _storers.Add((IStorer) part);
        }
    }
}