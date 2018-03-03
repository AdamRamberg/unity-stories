using System.Linq;
using UnityEngine;

namespace UnityStories 
{
	public abstract class Reducer : ScriptableObject
	{
		public abstract string Name { get; }
		public virtual void Handler(Story story, StoryAction action){}
		public Reducer[] subReducers;
		public Reducer this[string s] 
		{
			get 
			{
				if (subReducers == null) return null;
				return subReducers.FirstOrDefault(reducer => reducer.Name == s);
			}
		}
	}
}
