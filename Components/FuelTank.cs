using Resource;
using UnityEngine;
using Resources = Resource.Resources;

namespace Components
{
	public class FuelTank : MonoBehaviour, ITank {

		private ResourceBank _canStore;
		private ResourceBank _stores;
	
		// Use this for initialization
		private void Start () {
			_canStore = ResourceBank.Empty;
			_canStore.Add(Resources.Fuel, 500);
			_stores = _canStore.GetCopy();
		}
	
		// Update is called once per frame
		private void Update () {
		
		}

		public ResourceBank Retrieve(ResourceBank maxAmount)
		{
			return maxAmount;
		}

		/// <inheritdoc />
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maxAmount"></param>
		/// <returns>The amount of resources that couldn't fit.</returns>
		public ResourceBank Store(ResourceBank maxAmount)
		{
			return ResourceBank.Empty;
		}

		public ResourceBank Stored()
		{
			return _stores;
		}

		public ResourceBank Capacity()
		{
			return _canStore;
		}
	}
}
