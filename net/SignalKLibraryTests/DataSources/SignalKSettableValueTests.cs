using FluentAssertions;
using Moq;
using SignalKLibrary.DataSources;
using SignalKLibrary.Wrappers;
using System.Text.Json;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKSettableValueTests : TestBase
    {
        string _messageSent = "";
        JsonSerializerOptions _jsonOptions;
        Mock<IClientWebSocketWrapper> _mockClientWebSocket;

        [Test]
        public async Task SettingAStringValueSendsTheUpdateMessage()
        {
            var value = new SignalKSettableValue<string>("value.name", _mockClientWebSocket.Object); ;

            await value.Set("Hello World");

            _mockClientWebSocket.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);

            _messageSent.Should().Be("""{"updates":[{"values":[{"path":"value.name","value":"Hello World"}]}]}""");
        }

        [Test]
        public async Task SettingADoubleValueSendsTheUpdateMessage()
        {
            var value = new SignalKSettableValue<double>("value.name", _mockClientWebSocket.Object); ;

            await value.Set(3.14);

            _mockClientWebSocket.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);

            _messageSent.Should().Be("""{"updates":[{"values":[{"path":"value.name","value":3.14}]}]}""");
        }

        #region Support Code

        protected override void SetUpData()
        {
            base.SetUpData();
            _jsonOptions = new()
            {
                PropertyNameCaseInsensitive = false
            };
        }
        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockClientWebSocket = new Mock<IClientWebSocketWrapper>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockClientWebSocket.Setup(m => m.SendMessage(It.IsAny<string>()))
                .Callback((string message) => _messageSent = message);
        }

        #endregion
    }
}