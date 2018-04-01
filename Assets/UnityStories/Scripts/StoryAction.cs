using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UnityStories 
{
	public class StoryActionFactoryHelper<T> where T : StoryAction
	{
		private List<T> actionList = new List<T>();

		public T GetUnused()
		{
			for(var i = 0; i < actionList.Count; ++i)
			{
				if (!actionList[i].IsUsed())
				{
					actionList[i].BeforeReturnFromFactory();
					return actionList[i];
				}
			}
			
			return null;
		}

		public T CacheAndReturn(T action)
		{
			actionList.Add(action);
			return action;
		}
	}

	public abstract class StoryAction  
	{
		private int usedCounter = 1;

		public StoryAction() 
		{
			usedCounter = 1;
		}

		public abstract void ApplyToStory(Story story);

		public void KeepActionAlive() 
		{
			usedCounter += 1;
		}

		public void ReleaseActionForReuse() 
		{
			if (usedCounter <= 1) 
			{
				usedCounter = 0;
			}
			else 
			{
				usedCounter -= 1;
			}
		}

		// Use only in StoryActionFactoryHelper
		public bool IsUsed() 
		{
			return usedCounter > 0;
		}

		// Use only in StoryActionFactoryHelper
		public void BeforeReturnFromFactory() 
		{
			usedCounter = 1;
		}
	}
}
