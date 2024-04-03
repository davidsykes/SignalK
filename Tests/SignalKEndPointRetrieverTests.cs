using FluentAssertions;
using Logic;
using Moq;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKEndPointRetrieverTests : TestBase
    {
        SignalKEndPointRetriever _retriever;


        Mock<IHttpClientWrapper> _mockHttpClientWrapper;

        [Test]
        public async Task TheStreamingEndpointCanBeRetrieved()
        {
            var ep = await _retriever.RetrieveStreamingEndpoint("server ip");

            ep.Should().Be("streaming endpoint");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _retriever = new SignalKEndPointRetriever(_mockHttpClientWrapper.Object);
        }

        //protected override void SetUpData()
        //{
        //    base.SetUpData();

        //    _testRequest = new TestRequestObject { Id = 123, Request = "Test Object" };
        //    _testResponse = new TestResponseObject { Id = 999, Response = "Test Payload" };
        //}

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();

            _mockHttpClientWrapper.Setup(m => m.GetAsync("http://server ip:3000/signalk")).Returns(Task.FromResult("get url response"));
        }

        #endregion
    }
}
