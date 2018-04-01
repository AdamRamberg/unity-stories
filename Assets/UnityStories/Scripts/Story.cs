using UnityEngine;

namespace UnityStories 
{
	public abstract class Story : ScriptableObject
	{
		public Story[] subStories;

		private T GetSubStory<T>() where T : Story
		{
			for (var i = 0; subStories != null && i < subStories.Length; ++i)
			{
				if (subStories[i].GetType() == typeof(T)) 
				{
					return (T) subStories[i];
				}
			}

			return null;
		}

		public T Get<T>() where T : Story
		{
			var story = GetSubStory<T>();

			if (story != null) 
			{
				return (T) story;
			}

			return default(T);
		}

		public virtual void InitStory() {}
	}
}
