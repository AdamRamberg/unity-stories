using System;
using UnityEngine;
using NUnit.Framework;

namespace UnityStories 
{
	public class TestStore 
	{
		[Test]
		public void TestConnect() 
		{
			var store = ScriptableObject.CreateInstance("Store") as Store;
			Assert.AreEqual(store.GetConnectedCount(), 0);
			var handlerWasCalled = false;
			store.Connect((State state) => { handlerWasCalled = true; });
			Assert.AreEqual(store.GetConnectedCount(), 1);
			Assert.That(handlerWasCalled, Is.True);
		}
		
		[Test]
		public void TestDispatch() 
		{
			var store = ScriptableObject.CreateInstance("Store") as Store;
			var storeCreator = new StoreCreator();
			storeCreator.state = ScriptableObject.CreateInstance("MainState") as MainState;
			var stateTest = ScriptableObject.CreateInstance("StateTest") as StateTest;
			storeCreator.state.subStates = new State[1] { stateTest };
			var reducerTest = ScriptableObject.CreateInstance("ReducerTest") as ReducerTest;
			storeCreator.reducers = new Reducer[1] { reducerTest };
			store.storeCreator = storeCreator;
			store.CreateStore();

			var actionListenerWasCalled = false;
			store.Listen((StoreAction storeAction) => { actionListenerWasCalled = true; });

			store.Dispatch(new ActionTest());
			Assert.That(actionListenerWasCalled, Is.True);
			Assert.That(stateTest.updated, Is.True);
			Assert.That(reducerTest.wasHandlerCalled, Is.True);
		}
	}
}
