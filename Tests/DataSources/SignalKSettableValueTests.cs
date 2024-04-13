using FluentAssertions;
using Logic.DataSources;
using Logic.MessageHandlers;
using Logic.Wrappers;
using Moq;
using System;
using System.Text.Json;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKSettableValueTests : TestBase
    {
        ISignalKSettableValue _value;

        string _messageSent = "";
        JsonSerializerOptions _jsonOptions;
        Mock<IClientWebSocketWrapper> _mockClientWebSocket;

        [Test]
        public async Task SettingTheValueSendsTheUpdateMessage()
        {
            await _value.Set(3.14);

            _mockClientWebSocket.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);

            _messageSent.Should().Be("""{"updates":[{"values":[{"path":"value.name","value":3.14}]}]}""");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _value = new SignalKSettableValue("value.name", _mockClientWebSocket.Object);
        }

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