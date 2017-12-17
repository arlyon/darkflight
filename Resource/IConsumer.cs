using Components;

namespace Resource
{
    /// <summary>
    /// Implement this interface on a component that consumes a resource.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConsumer : IPart
    {
        /// <summary>
        /// The amount of <see cref="Resource"/> needed by the <see cref="IConsumer"/> this tick.
        /// </summary>
        /// <returns></returns>
        ResourceBank WillConsume();

        /// <summary>
        /// Gives power to the consumer to be used.
        /// </summary>
        /// <returns>The surplus energy.</returns>
        ResourceBank Give(ResourceBank maxAmount);
    }
}