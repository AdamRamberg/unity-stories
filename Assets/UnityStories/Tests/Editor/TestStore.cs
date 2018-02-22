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
			Assert.AreEqual(store.MapStateToPropsHandlerCount, 0);
			var handlerWasCalled = false;
			store.Connect((State state) => { handlerWasCalled = true; });
			Assert.AreEqual(store.MapStateToPropsHandlerCount, 1);
			Assert.That(handlerWasCalled, Is.True);
		}
		
		[Test]
		public void TestDispatch() 
		{
			var store = ScriptableObject.CreateInstance("Store") as Store;
			store.state = ScriptableObject.CreateInstance("MainState") as MainState;
			var stateTest = ScriptableObject.CreateInstance("StateTest") as StateTest;
			store.state.subStates = new State[1] { stateTest };
			var reducerTest = ScriptableObject.CreateInstance("ReducerTest") as ReducerTest;
			store.reducers = new Reducer[1] { reducerTest };
			var actionListenerWasCalled = false;
			store.Listen((StoreAction storeAction) => { actionListenerWasCalled = true; });

			store.Dispatch(new ActionTest());
			Assert.That(actionListenerWasCalled, Is.True);
			Assert.That(stateTest.updated, Is.True);
			Assert.That(reducerTest.wasHandlerCalled, Is.True);
		}
	}
}
