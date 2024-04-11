using FluentAssertions;
using Logic;
using Logic.DataListener;
using Logic.DataListener.Interfaces;
using Moq;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class UpdateDispenserTests : TestBase
    {
        IUpdateDispenser _dispenser;

        IList<SignalKUpdateValue> _dispensedUpdateValues;
        //readonly string _serverUrl = "Server url";
        //Queue<string> _messagesFromTheServer;
        Mock<ISignalKUpdateHandler> _mockSignalKUpdateHandler;
        //MockMessageProcessor _mockMessageProcessor;

        [Test]
        public void TheIndividualUpdatesAreDispensed()
        {
            var timeStamp1 = new DateTime(2024, 4, 7, 7, 23, 49);
            var timeStamp2 = new DateTime(2024, 4, 8, 7, 23, 49);
            var deltaMessage = new SignalKDeltaMessage();

            _dispenser.DispenseUpdates(deltaMessage);


            _dispensedUpdateValues.Count.Should().Be(3);
            _dispensedUpdateValues[0].Should().BeEquivalentTo(
                new SignalKUpdateValue(timeStamp1, "path1", "value1"));
            _dispensedUpdateValues[1].Should().BeEquivalentTo(
                new SignalKUpdateValue(timeStamp1, "path2", "value2"));
            _dispensedUpdateValues[2].Should().BeEquivalentTo(
                new SignalKUpdateValue(timeStamp2, "path3", "value3"));
            _dispensedUpdateValues[3].Should().BeEquivalentTo(
                new SignalKUpdateValue(timeStamp2, "path4", "value4"));
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _dispenser = new UpdateDispenser(_mockSignalKUpdateHandler.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockSignalKUpdateHandler = new Mock<ISignalKUpdateHandler>();
        }

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


