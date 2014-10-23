namespace CodeSmells.DataSeeder.DataGenerators
{
    using CodeSmells.Models;
    using CodeSmells.Data;

    public abstract class DataGenerator:IDataGenerator
    {
        protected ICodeSmellsData data;

        public DataGenerator(ICodeSmellsData data)
        {
            this.data = data;    
        }

        public abstract void Generate();
    }
}
