using UnityEngine;

namespace Control.Controllers
{
    public class Player : Controller
    {
        public Color Color;

        private void Start()
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color);
            GetComponentInChildren<Light>().color = Color;
        }

        public override void SendInput(InputData data)
        {
            
            var pitch = data.Axes.ContainsKey("Pitch") ? data.Axes["Pitch"] : 0;
            var yaw = data.Axes.ContainsKey("Yaw") ? data.Axes["Yaw"] : 0;
            RigidBody.AddRelativeForce(
                Quaternion.AngleAxis(45, Vector3.up) * new Vector3(pitch, 0, -yaw), 
                ForceMode.VelocityChange
            );
        }
    }
}