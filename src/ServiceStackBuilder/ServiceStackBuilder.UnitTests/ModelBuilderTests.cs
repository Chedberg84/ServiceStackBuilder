using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackBuilder.Builders;
using Onion.SolutionParser.Parser.Model;
using Moq;

namespace ServiceStackBuilder.UnitTests
{
    [TestClass]
    public class ModelBuilderTests
    {
        MessageBuilder modelBuilder;
        Mock<ISolution> solution;

        [TestInitialize]
        public void Init()
        {
            solution = new Mock<ISolution>();
            modelBuilder = new MessageBuilder(solution.Object);
        }

        [TestMethod]
        public void Test1()
        {
            //modelBuilder.Go();
        }
    }
}
