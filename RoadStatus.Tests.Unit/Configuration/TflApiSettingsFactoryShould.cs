using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using RoadStatus.Configuration;
using Xunit;

namespace RoadStatus.Tests.Unit.Configuration
{
    public class TflApiSettingsFactoryShould
    {
        [Fact]
        public void Throw_InvalidApiConfigurationException_When_Config_Section_Is_Missing()
        {
            var invalidConfigurationMock = new Mock<IConfiguration>();
            IConfigurationSection invalidSection = null;
            invalidConfigurationMock.Setup(g => g.GetSection(It.IsAny<string>()))
                .Returns(invalidSection);

            Action action = () => TflApiSettingsFactory
                .GetTflApiSettings(invalidConfigurationMock.Object);

            action
                .Should()
                .Throw<InvalidApiConfigurationException>();
        }
    }
}
