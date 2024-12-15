using DesignPatterns.SOLID;

namespace DesignPatterns.Builder;

public class FluentBuilder
{
    public class Person()
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {
            
        }
        
        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"Name: {Name}, Position: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person Person = new Person();
        public Person Build() => Person;
    }
    public class PersonInfoBuilder<TSelf> : PersonBuilder
    where TSelf : PersonInfoBuilder<TSelf>
    {
        public TSelf Called(string name)
        {
            Person.Name = name;
            return (TSelf)this;
        }
    }

    public class PersonJobBuilder <TSelf>: PersonInfoBuilder<PersonJobBuilder<TSelf>>
    where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorksAsA(string position)
        {
            Person.Position = position;
            return (TSelf)this;
        }

        internal class Program
        {
            public static void Runner()
            {
                var psjb = Person.New
                    .Called("Shahroz")
                    .WorksAsA("Developer")
                    .Build();
            }
        }
    }
}