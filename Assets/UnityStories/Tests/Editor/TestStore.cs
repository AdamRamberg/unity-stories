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
			var store = ScriptableObject.CreateInstance("Stories") as Stories;
			store.storiesCreator = new StoriesCreator();
			store.CreateStories();
			Assert.AreEqual(store.GetConnectedCount(), 0);
			var handlerWasCalled = false;
			store.Connect((Story story) => { handlerWasCalled = true; });
			Assert.AreEqual(store.GetConnectedCount(), 1);
			Assert.That(handlerWasCalled, Is.True);
		}
		
		[Test]
		public void TestDispatch() 
		{
			var store = ScriptableObject.CreateInstance("Stories") as Stories;
			var storeCreator = new StoriesCreator();
			storeCreator.entryStory = ScriptableObject.CreateInstance("EntryStory") as EntryStory;
			var storyTest = ScriptableObject.CreateInstance("StoryTest") as StoryTest;
			storeCreator.entryStory.subStories = new Story[1] { storyTest };
			var reducerTest = ScriptableObject.CreateInstance("ReducerTest") as ReducerTest;
			storeCreator.reducers = new Reducer[1] { reducerTest };
			store.storiesCreator = storeCreator;
			store.CreateStories();

			var actionListenerWasCalled = false;
			store.Listen((StoryAction storyAction) => { actionListenerWasCalled = true; });

			store.Dispatch(new StoryActionTest());
			Assert.That(actionListenerWasCalled, Is.True);
			Assert.That(storyTest.updated, Is.True);
			Assert.That(reducerTest.wasHandlerCalled, Is.True);
		}
	}
}
