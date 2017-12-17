using Resource;
using UnityEngine.Purchasing.Extension;

namespace Components
{
    public interface IBattery : IStorer
    {
        bool Enabled { get; set; }
    }
}