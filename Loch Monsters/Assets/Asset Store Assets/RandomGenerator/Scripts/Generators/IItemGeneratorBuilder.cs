using System.Collections.Generic;

namespace RandomGenerator.Scripts.Generators
{
    public interface IGeneratorBuilder
    {
        void Teach(IEnumerable<string> examples);
        void Teach(string example);

        IGenerator ToGenerator();
    }
}