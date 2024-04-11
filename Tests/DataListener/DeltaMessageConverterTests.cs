using FluentAssertions;
using Logic;
using Logic.DataListener;
using Logic.DataListener.Interfaces;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class DeltaMessageConverterTests : TestBase
    {
        IDeltaMessageConverter _converter;

        [Test]
        public void DeltaMessagesAreConvertedAndSentToTheUpdateDispenser()
        {
            var deltaMessage = """{"context":"vessels.urn:mrn:signalk:uuid:ee","updates":[{"$source":"ws.AUTO","timestamp":"2024-04-07T07:23:48.989Z","values":[{ "path":"home.temperature1","value":"value1"}]},{"$source":"ws.AUTO","timestamp":"2024-04-08T07:23:48.989Z","values":[{ "path":"home.temperature2","value":"value2"}]},{"$source":"ws.AUTO","timestamp":"2024-04-09T07:23:48.989Z","values":[{ "path":"home.temperature3","value":"value3"}]}]}""";

            var message = _converter.ConvertMessage(deltaMessage);

            message.Should().BeEquivalentTo(
                new SignalKDeltaMessage
                {
                    Values = [
                new SignalKUpdateValue(new DateTime(2024, 4, 7, 7, 23, 49), "home.temperature1", "value1"),
                new SignalKUpdateValue(new DateTime(2024, 4, 8, 7, 23, 49), "home.temperature2", "value2"),
                new SignalKUpdateValue(new DateTime(2024, 4, 9, 7, 23, 49), "home.temperature3", "value3"),
                    ]
                });
        }

        [Test]
        public void IfTheDeltaMessageIsMalformedTheMessageIsLogged()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void IfTheTimeStampConversionFailsTheMessageIsLogged()
        {
            throw new NotImplementedException();
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _converter = new DeltaMessageConverter();
        }

        //protected override void SetUpMocks()
        //{
        //    base.SetUpMocks();

        //    _mockUpdateDispenser = new Mock<IUpdateDispenser>();
        //}

        //protected override void SetUpData()
        //{
        //    base.SetUpData();
        //    _mockMessageProcessor = new MockMessageProcessor();
        //    _messagesFromTheServer = new Queue<string>();
        //    _messagesFromTheServer.Enqueue("Hello message");
        //}

        //protected override void SetUpExpectations()
        //{
        //    base.SetUpExpectations();
        //    _mockClientWebSocket.Setup(m => m.ReceiveMessage())
        //        .Returns(() => GetNextMessageFromTheMockServer());
        //}

        //Task<string> GetNextMessageFromTheMockServer()
        //{
        //    _messagesFromTheServer.Count.Should().BeGreaterThan(0);
        //    return Task.FromResult(_messagesFromTheServer.Dequeue());
        //}

        //class MockMessageProcessor : ISignalKMessageProcessor
        //{
        //    public IList<SignalKUpdateValue> MessagesProcessed = [];
        //}

        #endregion
    }
}


