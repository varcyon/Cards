using System.Collections.Generic;
using UnityEngine;

public interface IPositionable
{

	bool UpdateInRealtime { get; }
	int TotalCount
	{
		get;
	}
	List<Vector3> GetPositions ();
	Vector3 GetPosition(int i);

	void ApplyPositioning ();
}

public static class TransformExtensions
{

	/// <summary>
	/// Gets all children gameObjects.
	/// </summary>
	/// <returns>The children.</returns>
	/// <param name="component">Component.</param>
	public static List<Transform> GetChildrenTransforms (this Component component, bool includeInactive = true)
	{
		//		return component.GetComponentsInChildren<Transform>()
		//			.Where( x => x.transform.parent == component.transform ).Select ( y => y.gameObject )
		//				.ToList();
		List<Transform> list = new List<Transform>();

		foreach (Transform t in component.transform) 
		{
			if (t == component.transform)
				continue;
			if (!includeInactive && !t.gameObject.activeSelf)
				continue;

			list.Add (t);
		}

		return list;
	}

}   
