using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Satao.Tests.Fixtures;

namespace Satao.Tests
{
    [TestClass]
    public class When_purging_normal_priorities
    {
        private ICacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
            for (int i = 0; i < 100; i++)
            {
                _provider.Add(i, new DummyClass() { Name = "Bobby" }, TimeSpan.FromSeconds(5), CacheItemPriority.Normal);
            }

            for (int i = 100; i < 150; i++)
            {
                _provider.Add(i, new DummyClass() { Name = "Vanessa" }, TimeSpan.FromSeconds(5), CacheItemPriority.High);
            }
        }

        [TestMethod]
        public void Should_only_get_hight_priority()
        {
            _provider.PurgeNormalPriorities();
            _provider.Keys().Count().Should().Be(50);
        }
    }
}
