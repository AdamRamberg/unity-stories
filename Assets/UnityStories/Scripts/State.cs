using System.Linq;
using UnityEngine;

namespace UnityStories 
{
	public abstract class State : ScriptableObject
	{
		public abstract string Name { get; }
		public State[] subStates;
		public State this[string s] 
		{
			get 
			{
				if (subStates == null) return null;
				return subStates.FirstOrDefault(state => state.Name == s);
			}
		}

		public T Get<T>(string key) where T : State
		{
			var state = this[key];

			if (state != null) 
			{
				return (T) state;
			}

			return default(T);
		}

		public virtual void InitState() {}
	}
}
