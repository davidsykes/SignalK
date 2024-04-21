using FluentAssertions;
using Moq;
using SignalKLibrary;
using SignalKLibrary.Wrappers;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKEndPointRetrieverTests : TestBase
    {
        SignalKEndPointRetriever _retriever;

        string _endpointsJson = """{ "endpoints":{ "v1":{ "version":"2.6.2","signalk-http":"http://192.168.1.87:3000/signalk/v1/api/","signalk-ws":"ws://192.168.1.87:3000/signalk/v1/stream","signalk-tcp":"tcp://192.168.1.87:8375"} },"server":{ "id":"signalk-server-node","version":"2.6.2"} }""";
        Mock<IHttpClientWrapper> _mockHttpClientWrapper;

        [Test]
        public async Task TheStreamingEndpointCanBeRetrieved()
        {
            var ep = await _retriever.RetrieveStreamingEndpoint("server ip");

            ep.Should().Be("ws://192.168.1.87:3000/signalk/v1/stream");
        }

        [Test]
        public async Task IfTheEndpointsMessageIsMalformedAnExceptionIsThrown()
        {
            _mockHttpClientWrapper.Setup(m => m.GetAsync("http://server ip:3000/signalk"))
                .Returns(Task.FromResult("random { ["));

            Func<Task> action = async () => { await _retriever.RetrieveStreamingEndpoint("server ip"); };

            await action.Should().ThrowAsync<SKLibraryException>().WithMessage("Streaming Endpoint not found.");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _retriever = new SignalKEndPointRetriever(_mockHttpClientWrapper.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();

            _mockHttpClientWrapper.Setup(m => m.GetAsync("http://server ip:3000/signalk")).Returns(Task.FromResult(_endpointsJson));
        }

        #endregion
    }
}


