namespace UnityStories 
{
    public class GenericAction<S> : StoryAction where S : Story
    {
        public GenericAction() {}

        public virtual void Action(S story){}

        public override void ApplyToStory(Story story)
        {
            if (!(story is S)) return;

            var castedStory = (S) story;
            Action(castedStory);
        }
    }

    public class GenericAction<S, V> : StoryAction where S : Story
    {
        public V Value;

        public GenericAction() {}

        public virtual void Action(S story, V value){}

        public override void ApplyToStory(Story story)
        {
            if (!(story is S)) return;

            var castedStory = (S) story;
            Action(castedStory, Value);
        }
    }

    public class GenericAction<S, V1, V2> : StoryAction where S : Story
    {
        public V1 Value1;
        public V2 Value2;

        public GenericAction() {}

        public virtual void Action(S story, V1 value1, V2 value2){}

        public override void ApplyToStory(Story story)
        {
            if (!(story is S)) return;

            var castedStory = (S) story;
            Action(castedStory, Value1, Value2);
        }
    }

    public class GenericAction<S, V1, V2, V3> : StoryAction where S : Story
    {
        public V1 Value1;
        public V2 Value2;
        public V3 Value3;

        public GenericAction() {}

        public virtual void Action(S story, V1 value1, V2 value2, V3 value3){}

        public override void ApplyToStory(Story story)
        {
            if (!(story is S)) return;

            var castedStory = (S) story;
            Action(castedStory, Value1, Value2, Value3);
        }
    }

    public class GenericAction<S, V1, V2, V3, V4> : StoryAction where S : Story
    {
        public V1 Value1;
        public V2 Value2;
        public V3 Value3;
        public V4 Value4;

        public GenericAction() {}

        public virtual void Action(S story, V1 value1, V2 value2, V3 value3, V4 value4){}

        public override void ApplyToStory(Story story)
        {
            if (!(story is S)) return;

            var castedStory = (S) story;
            Action(castedStory, Value1, Value2, Value3, Value4);
        }
    }

    public class GenericAction<S, V1, V2, V3, V4, V5> : StoryAction where S : Story
    {
        public V1 Value1;
        public V2 Value2;
        public V3 Value3;
        public V4 Value4;
        public V5 Value5;

        public GenericAction() {}

        public virtual void Action(S story, V1 value1, V2 value2, V3 value3, V4 value4, V5 value5){}

        public override void ApplyToStory(Story story)
        {
            if (!(story is S)) return;

            var castedStory = (S) story;
            Action(castedStory, Value1, Value2, Value3, Value4, Value5);
        }
    }

    public class GenericFactory<GA, S> where GA : GenericAction<S>, new() where S : Story
    {
        private StoryActionFactoryHelper<GA> helper = new StoryActionFactoryHelper<GA>();

        public GA Get() 
        {
            var action = helper.GetUnused();

            if (action != null) 
            {
                return action;
            }
            return action != null ? action : helper.CacheAndReturn(new GA());
        }
    }

    public class GenericFactory<GA, S, V> where GA : GenericAction<S, V>, new() where S : Story
    {
        private StoryActionFactoryHelper<GA> helper = new StoryActionFactoryHelper<GA>();

        private GA ConstructGA(V value)
        {
            var action = new GA();
            action.Value = value;
            return action;
        }

        public GA Get(V value) 
        {
            var action = helper.GetUnused();

            if (action != null) 
            {
                action.Value = value;
                return action;
            }
            return action != null ? action : helper.CacheAndReturn(ConstructGA(value));
        }
    }

    public class GenericFactory<GA, S, V1, V2> where GA : GenericAction<S, V1, V2>, new() where S : Story
    {
        private StoryActionFactoryHelper<GA> helper = new StoryActionFactoryHelper<GA>();

        private GA ConstructGA(V1 value1, V2 value2)
        {
            var action = new GA();
            action.Value1 = value1;
            action.Value2 = value2;
            return action;
        }

        public GA Get(V1 value1, V2 value2) 
        {
            var action = helper.GetUnused();

            if (action != null) 
            {
                action.Value1 = value1;
                action.Value2 = value2;
                return action;
            }
            return action != null ? action : helper.CacheAndReturn(ConstructGA(value1, value2));
        }
    }

    public class GenericFactory<GA, S, V1, V2, V3> where GA : GenericAction<S, V1, V2, V3>, new() where S : Story
    {
        private StoryActionFactoryHelper<GA> helper = new StoryActionFactoryHelper<GA>();

        private GA ConstructGA(V1 value1, V2 value2, V3 value3)
        {
            var action = new GA();
            action.Value1 = value1;
            action.Value2 = value2;
            action.Value3 = value3;
            return action;
        }

        public GA Get(V1 value1, V2 value2, V3 value3) 
        {
            var action = helper.GetUnused();

            if (action != null) 
            {
                action.Value1 = value1;
                action.Value2 = value2;
                action.Value3 = value3;
                return action;
            }
            return action != null ? action : helper.CacheAndReturn(ConstructGA(value1, value2, value3));
        }
    }

    public class GenericFactory<GA, S, V1, V2, V3, V4> where GA : GenericAction<S, V1, V2, V3, V4>, new() where S : Story
    {
        private StoryActionFactoryHelper<GA> helper = new StoryActionFactoryHelper<GA>();

        private GA ConstructGA(V1 value1, V2 value2, V3 value3, V4 value4)
        {
            var action = new GA();
            action.Value1 = value1;
            action.Value2 = value2;
            action.Value3 = value3;
            action.Value4 = value4;
            return action;
        }

        public GA Get(V1 value1, V2 value2, V3 value3, V4 value4) 
        {
            var action = helper.GetUnused();

            if (action != null) 
            {
                action.Value1 = value1;
                action.Value2 = value2;
                action.Value3 = value3;
                action.Value4 = value4;
                return action;
            }
            return action != null ? action : helper.CacheAndReturn(ConstructGA(value1, value2, value3, value4));
        }
    }

    public class GenericFactory<GA, S, V1, V2, V3, V4, V5> where GA : GenericAction<S, V1, V2, V3, V4, V5>, new() where S : Story
    {
        private StoryActionFactoryHelper<GA> helper = new StoryActionFactoryHelper<GA>();

        private GA ConstructGA(V1 value1, V2 value2, V3 value3, V4 value4, V5 value5)
        {
            var action = new GA();
            action.Value1 = value1;
            action.Value2 = value2;
            action.Value3 = value3;
            action.Value4 = value4;
            action.Value5 = value5;
            return action;
        }

        public GA Get(V1 value1, V2 value2, V3 value3, V4 value4, V5 value5) 
        {
            var action = helper.GetUnused();

            if (action != null) 
            {
                action.Value1 = value1;
                action.Value2 = value2;
                action.Value3 = value3;
                action.Value4 = value4;
                action.Value5 = value5;
                return action;
            }
            return action != null ? action : helper.CacheAndReturn(ConstructGA(value1, value2, value3, value4, value5));
        }
    }
}