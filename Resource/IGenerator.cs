using Components;

namespace Resource
{
    /// <summary>
    /// Implement this interface on components that generate a resource.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator : IPart
    {
        /// <summary>
        /// The amount of <see cref="Resource"/> available from the <see cref="IGenerator"/> this tick.
        /// </summary>
        /// <returns></returns>
        ResourceBank WillGenerate();

        /// <summary>
        /// Tries to take the specified amount from the generator.
        /// </summary>
        /// <returns>The amount taken.</returns>
        ResourceBank Take(ResourceBank maxAmount);
    }
}