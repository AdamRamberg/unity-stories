using System.Collections.Generic;

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
}