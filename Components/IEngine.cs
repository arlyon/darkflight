using Resource;
using UnityEngine;

namespace Components
{
    public interface IEngine : IConsumer, IGenerator
    {
        Size GetSize();
        void SetThrottle(float dataAx);
        void SetAfterBurner(bool dataButton);
    }
}