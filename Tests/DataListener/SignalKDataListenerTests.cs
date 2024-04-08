using FluentAssertions;
using Logic;
using Logic.DataListener;
using Logic.Wrappers;
using Moq;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKDataListenerTests : TestBase
    {
        ISignalKDataListener _listener;

        readonly string _serverUrl = "Server url";
        Queue<string> _messagesFromTheServer;
        Mock<IClientWebSocketWrapper> _mockClientWebSocket;
        MockMessageProcessor _mockMessageProcessor;

        [Test]
        public async Task InitialiseOpensUpTheWebSocket()
        {
            await _listener.Initialise();

            _mockClientWebSocket.Verify(m => m.ConnectAsync(_serverUrl));
        }

        [Test]
        public async Task InitialiseReceivesTheHelloMessage()
        {
            await _listener.Initialise();

            _mockClientWebSocket.Verify(m => m.ReceiveMessage());
        }

        [Test]
        public async Task AnUpdateMessageCanBeProcessed()
        {
            _messagesFromTheServer.Enqueue("""{"context":"vessels.urn:mrn:signalk:uuid:ee","updates":[{"$source":"ws.AUTO","timestamp":"2024-04-07T07:23:48.989Z","values":[{"path":"home.temperature","value":3.412}]}]}""");
            await _listener.Initialise();

            _listener.ProcessMessages(_mockMessageProcessor);

            _mockMessageProcessor.MessagesProcessed.Count.Should().Be(1);
            var update = _mockMessageProcessor.MessagesProcessed[0];
            update.TimeStamp.Should().Be(new DateTime(2024, 4, 7, 7, 23, 49));
            update.Path.Should().Be("home.temperature");
            update.Value.Should().Be("3.412");

        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _listener = new SignalKDataListener(_serverUrl, _mockClientWebSocket.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockClientWebSocket = new Mock<IClientWebSocketWrapper>();
        }

        protected override void SetUpData()
        {
            base.SetUpData();
            _mockMessageProcessor = new MockMessageProcessor();
            _messagesFromTheServer = new Queue<string>();
            _messagesFromTheServer.Enqueue("Hello message");
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockClientWebSocket.Setup(m => m.ReceiveMessage())
                .Returns(() => GetNextMessageFromTheMockServer());
        }

        Task<string> GetNextMessageFromTheMockServer()
        {
            _messagesFromTheServer.Count.Should().BeGreaterThan(0);
            return Task.FromResult(_messagesFromTheServer.Dequeue());
        }

        class MockMessageProcessor : ISignalKMessageProcessor
        {
            public IList<SignalKUpdateValue> MessagesProcessed = [];
        }

        #endregion
    }
}


