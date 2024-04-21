using FluentAssertions;
using Moq;
using SignalKLibrary.DataListener;
using SignalKLibrary.DataListener.Interfaces;
using SignalKLibrary.Wrappers;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKMessageHandlerTests : TestBase
    {
        private ISignalKMessageHandler _handler;

        readonly string _serverUrl = "Server url";
        Queue<string> _messagesFromTheServer;
        IList<string> _messagesDispensed;
        Mock<IClientWebSocketWrapper> _mockClientWebSocket;
        Mock<ISignalKMessageDispenser> _mockSignalKMessageDispenser;

        [Test]
        public async Task InitialiseOpensUpTheWebSocket()
        {
            Func<Task> action = () => _handler.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(_mockSignalKMessageDispenser.Object);
            await action.Should().ThrowAsync<EndOfTestMessagesException>();

            _mockClientWebSocket.Verify(m => m.ConnectAsync(_serverUrl));
        }

        [Test]
        public async Task InitialiseReceivesTheHelloMessage()
        {
            Func<Task> action = () => _handler.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(_mockSignalKMessageDispenser.Object);
            await action.Should().ThrowAsync<EndOfTestMessagesException>();

            _mockClientWebSocket.Verify(m => m.ReceiveMessage());
        }

        [Test]
        public async Task AllMessagesReceivedFromTheServerAreSentToTheDeltaMessageConverter()
        {
            _messagesFromTheServer.Enqueue("Message 1");
            _messagesFromTheServer.Enqueue("Message 2");
            _messagesFromTheServer.Enqueue("Message 3");

            Func<Task> action = () => _handler.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(_mockSignalKMessageDispenser.Object);
            await action.Should().ThrowAsync<EndOfTestMessagesException>();

            _messagesDispensed.Count.Should().Be(3);
            _messagesDispensed[0].Should().Be("Message 1");
            _messagesDispensed[1].Should().Be("Message 2");
            _messagesDispensed[2].Should().Be("Message 3");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _handler = new SignalKMessageHandler(_serverUrl, _mockClientWebSocket.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockClientWebSocket = new Mock<IClientWebSocketWrapper>();
            _mockSignalKMessageDispenser = new Mock<ISignalKMessageDispenser>();
        }

        protected override void SetUpData()
        {
            base.SetUpData();
            _messagesFromTheServer = new Queue<string>();
            _messagesFromTheServer.Enqueue("Hello message");
            _messagesDispensed = [];
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockClientWebSocket.Setup(m => m.ReceiveMessage())
                .Returns(() => Task.FromResult(GetNextMessageFromTheMockServer()));
            _mockSignalKMessageDispenser.Setup(m => m.ConvertAndDispenseMessage(It.IsAny<string>()))
                .Callback((string message) => _messagesDispensed.Add(message));
        }

        string GetNextMessageFromTheMockServer()
        {
            if (_messagesFromTheServer.Count > 0)
            {
                return _messagesFromTheServer.Dequeue();
            }
            throw new EndOfTestMessagesException();
        }

        #endregion
    }

    class EndOfTestMessagesException : Exception
    { }
}


