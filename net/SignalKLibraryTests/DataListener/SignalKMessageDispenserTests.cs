using FluentAssertions;
using Moq;
using SignalKLibrary;
using SignalKLibrary.DataListener;
using SignalKLibrary.DataListener.Interfaces;
using SignalKLibrary.DataObjects;
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

        [Test]
        public void IfTheUpdateHandlerThrowsAnExceptionItIsIgnored()
        {
            _mockSignalKUpdateHandler.Setup(m => m.Update(It.IsAny<SignalKDeltaMessage>()))
            .Throws(new Exception("Oops"));

            Action action = () => _dispenser.ConvertAndDispenseMessage("server message");

            action.Should().NotThrow();
        }

        [Test]
        public void IfTheInvalidServerMessageHandlerThrowsAnExceptionItIsIgnored()
        {
            _mockSignalKUpdateHandler.Setup(m => m.Update(It.IsAny<SignalKDeltaMessage>()))
            .Throws(new Exception("Oops"));
            _mockSignalKUpdateHandler.Setup(m => m.InvalidServerMessage(It.IsAny<string>()))
            .Throws(new Exception("Oops"));

            Action action = () => _dispenser.ConvertAndDispenseMessage("server message");

            action.Should().NotThrow();
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


