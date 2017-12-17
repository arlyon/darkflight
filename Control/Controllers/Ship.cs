using Components;
using Resource;
using UnityEngine;

namespace Control.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// The Spacecraft Controller
    /// </summary>
    public class Ship : Controller
    {
        private ResourceNetwork _network;
        private IEngine _engine;
        private Vector3 _rotation;
        private bool _dampen;

        // Use this for initialization
        private new void Awake()
        {
            base.Awake();
            
            _network = new ResourceNetwork();
            _engine = GetComponentInChildren<IEngine>();
		
            foreach (var part in GetComponentsInChildren<IPart>())
            {
                _network.Register(part);
            }
        }
	
        // Update is called once per frame
        private void Update()
        {
            _network.Tick();
            RigidBody.AddRelativeTorque(_rotation);
            RigidBody.angularDrag = _dampen ? 3 : 1;
        }
        
        public override void SendInput(InputData data)
        {
            Debug.Log(data);
            _engine.SetThrottle(data.Axes["Thruster"]);
            _engine.SetAfterBurner(data.Buttons["Afterburner"]);
            _dampen = data.Buttons["Dampen"];
            
            var pitch = data.Axes.ContainsKey("Pitch") ? data.Axes["Pitch"] : 0;
            var yaw = data.Axes.ContainsKey("Yaw") ? data.Axes["Yaw"] : 0;
            var roll = data.Axes.ContainsKey("Roll") ? data.Axes["Roll"] : 0;
            _rotation = new Vector3(pitch, yaw, roll);
        }
    }
}