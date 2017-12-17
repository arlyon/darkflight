using Control.Controllers;
using Resource;
using UnityEngine;
using Resources = Resource.Resources;

namespace Components
{
    /**
     * Main engine, afterburner
     * 
     * Engines come in many different shapes and sizes.
     *
     * Interesting ideas:
     *     power efficiency vs fuel efficiency
     * 
     */
    [RequireComponent(typeof(Ship))]
    public class Engine : MonoBehaviour, IEngine
    {
        public float Acceleration { get; private set; }
        public float Efficiency { get; private set; }
        public float TopSpeed { get; private set; }
        public Color Color { get; private set; }

        public float quality;
        public Size size;

        private ResourceBank _consumes;
        private ResourceBank _generates;
        private float _throttle;
        private bool _afterBurner;

        // Use this for initialization
        private void Start () {
            _consumes = ResourceBank.Empty;
            _consumes.Add(Resources.Power, 100);
            _consumes.Add(Resources.Fuel, 1);
            
            _generates = ResourceBank.Empty;
            _generates.Add(Resources.Heat, 20);
            
            Color = new Color(1,1,1);
            GenerateStats();
        }

        private void Update()
        {
            HandleForce();
            HandleParticleSystem();
            HandleSoundSystem();
        }

        private void HandleForce()
        {
            GetComponentInParent<Rigidbody>().AddRelativeForce(Vector3.forward * _throttle * Acceleration);
        }

        private void HandleSoundSystem()
        {
            var sound = GetComponent<AudioSource>();
            sound.volume = _throttle / 10 * 3;
            sound.pitch = 0.5f + _throttle / 2;
        }

        private void HandleParticleSystem()
        {
            var emission = GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = _throttle * (_afterBurner ? 80 : 50);
        }

        private void GenerateStats()
        {
            this.Acceleration =  1 * this.quality;
            this.Efficiency = 1 / this.quality;
            this.TopSpeed = 0.5f * this.quality;
        }

        public Size GetSize()
        {
            return this.size;
        }

        /// <summary>
        /// Takes the -1 - 1 float and converts it to a throttle value.
        /// </summary>
        /// <param name="dataAx">The value from the input.</param>
        public void SetThrottle(float dataAx)
        {
            _throttle = (dataAx + 1) / 2;
        }

        public void SetAfterBurner(bool dataButton)
        {
            _afterBurner = dataButton;
        }

        /// <inheritdoc />
        /// <summary>
        /// Calculates how many resources to consume based on throttle.
        /// </summary>
        /// <returns>The amount to consume.</returns>
        public ResourceBank WillConsume()
        {
            return _consumes * _throttle;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gives resources to the consumer.
        /// </summary>
        /// <param name="maxAmount">The amount to give.</param>
        /// <returns>The amount unused.</returns>
        public ResourceBank Give(ResourceBank maxAmount)
        {
            return ResourceBank.Empty;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns how much the engine will generate.
        /// </summary>
        /// <returns></returns>
        public ResourceBank WillGenerate()
        {
            return _generates * _throttle;
        }

        /// <inheritdoc />
        /// <summary>
        /// Takes resources from the generator.
        /// </summary>
        /// <param name="maxAmount">The amount to take.</param>
        /// <returns>The amount taken.</returns>
        public ResourceBank Take(ResourceBank maxAmount)
        {
            return maxAmount;
        }
    }
}