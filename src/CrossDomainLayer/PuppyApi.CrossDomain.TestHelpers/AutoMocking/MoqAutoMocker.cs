namespace StructureMap.AutoMocking.Moq
{
    using AutoMocking;

    public class MoqAutoMocker<T> : AutoMocker<T> where T : class
    {
        public MoqAutoMocker() : base(new MoqServiceLocator())
        {
            ServiceLocator = new MoqServiceLocator();
        }
    }
}