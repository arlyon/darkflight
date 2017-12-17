using UnityEngine;

namespace Resource
{
    public static class Resources
    {
        public static Resource Power = new Resource("Power", Color.white, x => {});
        public static Resource Fuel = new Resource("Fuel", Color.yellow, x => {});
        public static Resource Water = new Resource("Water", Color.blue, x => {});
        public static Resource Heat = new Resource("Heat", Color.red, x => {});
    }
}