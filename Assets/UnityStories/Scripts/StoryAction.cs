namespace UnityStories 
{
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
