using StructureMap.AutoMocking.Moq;
using System;

namespace PuppyApi.UnitTests
{
    public class TestsFor<TEntity> where TEntity : class
    {
        public TEntity Instance { get; set; }

        public MoqAutoMocker<TEntity> AutoMock { get; set; }

        public TestsFor()
        {
            AutoMock = new MoqAutoMocker<TEntity>();

            Instance = AutoMock.ClassUnderTest;
        }

    }
}
