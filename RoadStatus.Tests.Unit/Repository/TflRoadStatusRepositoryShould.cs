using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RoadStatus.Configuration;
using RoadStatus.Repository;
using Xunit;

namespace RoadStatus.Tests.Unit.Repository
{
    public class TflRoadStatusRepositoryShould : TflApiTestSpecification
    {
        private const string VALID_ROAD_NAME = "BLAH";
        private const string VALID_ROAD_STATUS = "Test Severity Status";
        private const string VALID_ROAD_STATUS_DESCRIPTION = "Test Severity Description";

        private const string INVALID_ROAD_NAME = "A233";

        private const string UNDETERMINED_ROAD = "NO-ROAD";

        [Fact]
        public void Throw_ArgumentNullException_Given_HttpClientFactory_Is_Not_Set()
        {
            IHttpClientFactory nullFactory = null;
            var settingsMock = new Mock<TflApiSettings>();

            Action sut = () =>
                new TflRoadStatusRepository(nullFactory, settingsMock.Object);
            
            sut.Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_ArgumentNullException_Given_Settings_Is_Not_Set()
        {
            var factory = new Mock<IHttpClientFactory>();
            TflApiSettings nullSettings = null;

            Action sut = () =>
                new TflRoadStatusRepository(factory.Object, nullSettings);

            sut.Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public void Not_Throw_ArgumentNullException_Given_All_Parameters_Are_Set()
        {
            var factory = new Mock<IHttpClientFactory>();
            var settingsMock = new Mock<TflApiSettings>();

            Action sut = () =>
                new TflRoadStatusRepository(factory.Object, settingsMock.Object);

            sut.Should()
                .NotThrow<ArgumentNullException>();
        }

        [Fact]
        public async Task Return_A_Result_Given_A_Valid_Road_Name()
        {
            GiveAValidRoadName();

            await WhenTheApiIsCalled(VALID_ROAD_NAME);

            RoadResult.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Have_A_Successful_Application_Status_Given_A_Valid_Road_Name()
        {
            GiveAValidRoadName();

            await WhenTheApiIsCalled(VALID_ROAD_NAME);

            RoadResult.GetApplicationStatus()
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Display_Correct_displayName_Given_A_Valid_Road_Name()
        {
            var displayNamemessage = $"The status of the {VALID_ROAD_NAME} is as follows ";

            GiveAValidRoadName();

            await WhenTheApiIsCalled(VALID_ROAD_NAME);

            RoadResult
                .GetDisplayMessage()
                .Should()
                .Contain(displayNamemessage);
        }

        [Fact]
        public async Task Display_Correct_Severity_Status_Given_A_Valid_Road_Name()
        {
            var roadStatusMessage = $"Road Status is {VALID_ROAD_STATUS}";

            GiveAValidRoadName();

            await WhenTheApiIsCalled(VALID_ROAD_NAME);

            RoadResult
                .GetDisplayMessage()
                .Should()
                .Contain(roadStatusMessage);
        }

        [Fact]
        public async Task Display_Correct_statusSeverityDescription_Given_A_Valid_Road_Name()
        {
            var roadStatusMessage = $"Road Status Description is {VALID_ROAD_STATUS_DESCRIPTION}";

            GiveAValidRoadName();

            await WhenTheApiIsCalled(VALID_ROAD_NAME);

            RoadResult
                .GetDisplayMessage()
                .Should()
                .Contain(roadStatusMessage);
        }

        [Fact]
        public async Task Display_Informative_Error_Message_Given_An_Invalid_Road_Name()
        {
            var invalidMessage = $"{INVALID_ROAD_NAME} is not a valid road";

            GivenAnInvalidRoadName();

            await WhenTheApiIsCalled(INVALID_ROAD_NAME);

            RoadResult
                .GetDisplayMessage()
                .Should()
                .Contain(invalidMessage);
        }

        [Fact]
        public async Task Display_Informative_Message_Given_Undetermined_Road_Name()
        {
            var informativeMessage = $"The status of road {UNDETERMINED_ROAD} cannot be determined";

            GivenAnUnderterminedRoad();

            await WhenTheApiIsCalled(UNDETERMINED_ROAD);

            RoadResult
                .GetDisplayMessage()
                .Should()
                .Contain(informativeMessage);
        }


    }
}
