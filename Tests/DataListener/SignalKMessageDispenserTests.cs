using FluentAssertions;
using Logic;
using Logic.DataListener;
using Logic.DataListener.Interfaces;
using Logic.DataObjects;
using Moq;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SignalKMessageDispenserTests : TestBase
    {
        ISignalKMessageDispenser _dispenser;

        SignalKDeltaMessage _convertedUpdateMessage;
        SignalKDeltaMessage _dispensedUpdate;
        Mock<IDeltaMessageConverter> _mockDeltaMessageConverter;
        Mock<ISignalKUpdateHandler> _mockSignalKUpdateHandler;

        [Test]
        public void TheMessageIsConvertedAndSentToTheUpdateHandler()
        {
            _dispenser.ConvertAndDispenseMessage("server message");
            _dispensedUpdate.Should().Be(_convertedUpdateMessage);
        }

        [Test]
        public void IfTheConversionFailsTheMessageIsLogged()
        {
            _mockDeltaMessageConverter.Setup(m => m.ConvertMessage("server message"))
                .Throws(new Exception());

            _dispenser.ConvertAndDispenseMessage("server message");

            _mockSignalKUpdateHandler.Verify(m => m.InvalidServerMessage("server message"));
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _dispenser = new SignalKMessageDispenser(_mockDeltaMessageConverter.Object, _mockSignalKUpdateHandler.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();
            _mockDeltaMessageConverter = new Mock<IDeltaMessageConverter>();
            _mockSignalKUpdateHandler = new Mock<ISignalKUpdateHandler>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockDeltaMessageConverter.Setup(m => m.ConvertMessage("server message"))
                .Returns(_convertedUpdateMessage);
            _mockSignalKUpdateHandler.Setup(m => m.Update(It.IsAny<SignalKDeltaMessage>()))
                .Callback((SignalKDeltaMessage message) => _dispensedUpdate = message);
        }

        protected override void SetUpData()
        {
            base.SetUpData();

            _convertedUpdateMessage = new SignalKDeltaMessage("Fred", []);
        }

        #endregion
    }
}


