using Logic.DataSources;
using Logic.Wrappers;
using Moq;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKDataSourceTests : TestBase
    {
        SignalKDataSource _source;

        string _serverUrl = "Server url";
        Mock<IClientWebSocketWrapper> _mockClientWebSocket;

        [Test]
        public async Task InitialiseOpensUpTheWebSocket()
        {
            await _source.Initialise();

            _mockClientWebSocket.Verify(m => m.ConnectAsync(_serverUrl));
        }

        [Test]
        public async Task InitialiseReceivesTheHelloMessage()
        {
            await _source.Initialise();

            _mockClientWebSocket.Verify(m => m.ReceiveMessage());
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _source = new SignalKDataSource(_serverUrl, _mockClientWebSocket.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockClientWebSocket = new Mock<IClientWebSocketWrapper>();
        }

        //protected override void SetUpExpectations()
        //{
        //    base.SetUpExpectations();

        //    _mockHttpClientWrapper.Setup(m => m.GetAsync("http://server ip:3000/signalk")).Returns(Task.FromResult(_endpointsJson));
        //}

        #endregion
    }
}


