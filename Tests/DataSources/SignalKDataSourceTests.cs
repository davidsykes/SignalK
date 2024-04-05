using Logic.DataSources;
using Logic.MessageHandlers;
using Logic.Wrappers;
using Moq;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKDataSourceTests : TestBase
    {
        SignalKDataSource _source;

        readonly string _serverUrl = "Server url";
        Mock<IClientWebSocketWrapper> _mockClientWebSocket;
        Mock<ISKLogInHandler> _mockSKLogInHandler;

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

        [Test]
        public async Task InitialiseLogsIn()
        {
            await _source.Initialise();

            _mockSKLogInHandler.Verify(m => m.LogIn());
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _source = new SignalKDataSource(_serverUrl, _mockClientWebSocket.Object, _mockSKLogInHandler.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockClientWebSocket = new Mock<IClientWebSocketWrapper>();
            _mockSKLogInHandler = new Mock<ISKLogInHandler>();
        }

        //protected override void SetUpExpectations()
        //{
        //    base.SetUpExpectations();

        //    _mockHttpClientWrapper.Setup(m => m.GetAsync("http://server ip:3000/signalk")).Returns(Task.FromResult(_endpointsJson));
        //}

        #endregion
    }
}


