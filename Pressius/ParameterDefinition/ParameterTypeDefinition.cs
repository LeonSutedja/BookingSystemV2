namespace Pressius
{
    public class ParameterTypeDefinition
    {
        public string Name { get; private set; }

        public ParameterTypeDefinition(string name)
        {
            Name = name;
        }
    }
}