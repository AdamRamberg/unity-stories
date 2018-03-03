using System.Linq;
using UnityEngine;

namespace UnityStories 
{
	public abstract class Story : ScriptableObject
	{
		public abstract string Name { get; }
		public Story[] subStories;
		public Story this[string s] 
		{
			get 
			{
				if (subStories == null) return null;
				return subStories.FirstOrDefault(story => story.Name == s);
			}
		}

		public T Get<T>(string key) where T : Story
		{
			var story = this[key];

			if (story != null) 
			{
				return (T) story;
			}

			return default(T);
		}

		public virtual void InitStory() {}
		public virtual void ActionHandler(StoryAction action){}
	}
}
