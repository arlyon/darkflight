using Components;

namespace Resource
{
    /// <summary>
    /// Implement this interface for components that store energy.
    /// </summary>
    public interface IStorer : IPart
    {
        /// <summary>
        /// Returns a struct with all the recources
        /// </summary>
        /// <param name="maxAmount">The dict of amounts to take.</param>
        /// <returns>The amount taken.</returns>
        ResourceBank Retrieve(ResourceBank maxAmount);

        /// <summary>
        /// Gives resources to the <see cref="IStorer"/> to store.
        /// </summary>
        /// <param name="maxAmount">The maximum amount to give.</param>
        /// <returns>The resources that couldnt fit.</returns>
        ResourceBank Store(ResourceBank maxAmount);

        /// <summary>
        /// The amount currently available.
        /// </summary>
        /// <returns></returns>
        ResourceBank Stored();
        
        
        /// <summary>
        /// The total about of storage space.
        /// </summary>
        /// <returns></returns>
        ResourceBank Capacity();
    }
}