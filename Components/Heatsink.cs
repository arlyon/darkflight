using System.Collections;
using System.Collections.Generic;
using Components;
using Resource;
using UnityEngine;
using Resources = Resource.Resources;

public class Heatsink : MonoBehaviour, IHeatsink
{

	private ResourceBank _consumes;
	private ResourceBank _canStore;
	private ResourceBank _stores;

	// Use this for initialization
	private void Start () {
		this._consumes = ResourceBank.Empty;
		this._consumes.Add(Resources.Power, 100);

		this._canStore = ResourceBank.Empty;
		this._canStore.Add(Resources.Heat, 2000);
		this._stores = this._canStore.GetEmptyCopy();
	}
	
	/// <summary>
	/// Called once per frame. It will empty out 10 percent of the heat per frame.
	/// </summary>
	private void Update ()
	{
		_stores *= 0.9f;
	}

	public ResourceBank WillConsume()
	{
		return _consumes;
	}

	public ResourceBank Give(ResourceBank maxAmount)
	{
		return ResourceBank.Empty;
	}

	public ResourceBank Retrieve(ResourceBank maxAmount)
	{
		return maxAmount;
	}

	public ResourceBank Store(ResourceBank maxAmount)
	{
		_stores += maxAmount;
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
