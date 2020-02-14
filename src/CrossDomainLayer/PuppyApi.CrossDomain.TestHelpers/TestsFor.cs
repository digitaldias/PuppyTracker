using Moq;
using StructureMap.AutoMocking.Moq;
using System;

namespace PuppyApi.CrossDomain.TestHelpers
{
    public class TestsFor<TEntity> where TEntity : class
    {
        /// <summary>
        /// This is the instance being tested
        /// </summary>
        public TEntity Instance { get; set; }

        /// <summary>
        /// This is the AutoMocker object that fills in all the dependencies for the Instance under test
        /// </summary>
        public MoqAutoMocker<TEntity> AutoMock { get; set; }

        /// <summary>
        /// Initiates the test instance and populate it with all mocks
        /// </summary>
        public TestsFor()
        {
            AutoMock = new MoqAutoMocker<TEntity>();

            Instance = AutoMock.ClassUnderTest;
        }

        /// <summary>
        /// Return the owner Mock of the TContract
        /// </summary>
        /// <typeparam name="TContract">Type of contract for which the mock exists. Reference types only</typeparam>
        /// <returns></returns>
        public Mock<TContract> GetMockFor<TContract>() where TContract : class
        {
            return Mock.Get(AutoMock.Get<TContract>());
        }
    }
}
