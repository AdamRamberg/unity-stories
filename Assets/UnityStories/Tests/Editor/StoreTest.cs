using UnityEngine;
using NUnit.Framework;

namespace UnityStories 
{
	public class StoreTest 
	{
		public void MapStateToProps(State state) {}

		[Test]
		public void ConnectTest() 
		{
			var store = ScriptableObject.CreateInstance("Store") as Store;
			Assert.AreEqual(store.MapStateToPropsHandlerCount, 0);
			store.Connect(MapStateToProps);
			Assert.AreEqual(store.MapStateToPropsHandlerCount, 1);
		}
		
	}
}
