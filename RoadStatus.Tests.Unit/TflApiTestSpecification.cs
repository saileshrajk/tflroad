using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RoadStatus.Configuration;
using RoadStatus.Entity;
using RoadStatus.Repository;
using RoadStatus.Tests.Unit.FakeHandlers;

namespace RoadStatus.Tests.Unit
{
    public class TflApiTestSpecification
    {
        private readonly string _validJson;
        private readonly string _invalidJson;
        private Mock<TflApiSettings> _settingsMock;
        private Mock<IHttpClientFactory> _httpClientFactory;

        private HttpMessageHandler _messageHandler;
        
        private HttpClient httpClient;
        
        public IRoad RoadResult { get; set; }

        public TflApiTestSpecification()
        {
            _validJson = TestHelpers.GetValidJson();
            _invalidJson = TestHelpers.GetNotFoundJson();
            _settingsMock = new Mock<TflApiSettings>();
            _httpClientFactory = new Mock<IHttpClientFactory>();
        }

        public void GiveAValidRoadName()
        {
            GetValidMessageHttpClient();
        }

        public void GivenAnInvalidRoadName()
        {
            GetInvalidMessageHttpClient();
        }

        public void GivenAnUnderterminedRoad()
        {
            GetUnknownMessageHttpClient();
        }

        public async Task WhenTheApiIsCalled(string roadname)
        {
            var tflRoadStatusRepository = new TflRoadStatusRepository(_httpClientFactory.Object,
                _settingsMock.Object);

            RoadResult = await tflRoadStatusRepository.GetRoadStatus(roadname);
        }

        private void GetValidMessageHttpClient()
        {
            _messageHandler = new HttpHandlerStub(async (request, cancellationToken) =>
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_validJson)
                };

                return await Task.FromResult(responseMessage);
            });

            SetHttpClient();

            _httpClientFactory
                .Setup(s => s.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


        }

        private void GetInvalidMessageHttpClient()
        {
            _messageHandler = new HttpHandlerStub(async (request, cancellationToken) =>
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(_invalidJson)
                };

                return await Task.FromResult(responseMessage);
            });

            SetHttpClient();

            _httpClientFactory
                .Setup(s => s.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);
        }

        private void GetUnknownMessageHttpClient()
        {
            _messageHandler = new HttpHandlerStub(async (request, cancellationToken) =>
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Empty)
                };

                return await Task.FromResult(responseMessage);
            });

            SetHttpClient();

            _httpClientFactory
                .Setup(s => s.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);
        }

        private void SetHttpClient()
        {
            httpClient = new HttpClient(_messageHandler) {BaseAddress = new Uri("http://validcom")};

        }
    }
}
