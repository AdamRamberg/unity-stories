using System;
using UnityEngine;
using NUnit.Framework;

namespace UnityStories 
{
	public class TestStories 
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
			var storiesCreator = new StoriesCreator();
			var entryStory = ScriptableObject.CreateInstance("EntryStory") as EntryStory;
			var testStory = ScriptableObject.CreateInstance("TestStory") as TestStory;
			entryStory.subStories = new Story[1] { testStory };
			storiesCreator.entryStory = entryStory;
			store.storiesCreator = storiesCreator;
			store.CreateStories();   

			var actionListenerWasCalled = false;
			store.Listen((StoryAction storyAction) => { actionListenerWasCalled = true; });

			store.Dispatch(new TestStory.TestAction());
			Assert.That(actionListenerWasCalled, Is.True);
			Assert.That(testStory.updated, Is.True);
		}
	}
}
