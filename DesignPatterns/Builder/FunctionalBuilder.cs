namespace DesignPatterns.Builder;

public class FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;
    }

    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions 
            = new List<Action<Person>>();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA
            ( PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }
  
    public class FuncBuilder
    {
        public static void Run(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Dmitri");
        }
    }
}