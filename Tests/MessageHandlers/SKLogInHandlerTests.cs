using FluentAssertions;
using Logic.Wrappers;
using Moq;
using System.Text.Json;
using TestHelpers;

#nullable disable
namespace Tests
{
    public class SKLogInHandlerTests : TestBase
    {
        SKLogInHandler _handler;

        string _guid = "12345678";
        string _userName = "user name";
        string _password = "pass word";

        Mock<IClientWebSocketWrapper> _mockClientWebSocketWrapper;
        Mock<IGuidWrapper> _mockGuidWrapper;

        [Test]
        public async Task TheLogInMessageIsSentToTheServer()
        {
            string logInMessage = "";
            _mockClientWebSocketWrapper.Setup(m => m.SendMessage(It.IsAny<string>()))
                .Callback((string message) => logInMessage = message);

            await _handler.LogIn();

            _mockClientWebSocketWrapper.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };
            var logIn = JsonSerializer.Deserialize<ExpectedLogInMessage>(logInMessage, options);
            logIn.requestid.Should().Be(_guid);
            logIn.login.username.Should().Be(_userName);
            logIn.login.password.Should().Be(_password);
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _handler = new SKLogInHandler(_userName, _password, _mockClientWebSocketWrapper.Object, _mockGuidWrapper.Object);
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            _mockClientWebSocketWrapper = new Mock<IClientWebSocketWrapper>();
            _mockGuidWrapper = new Mock<IGuidWrapper>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockGuidWrapper.Setup(m => m.NewGuid()).Returns(_guid);
        }

        class ExpectedLogInMessage
        {
            public string requestid { get; set; }
            public LogIn login { get; set; }

            public class LogIn
            {
                public string username { get; set; }
                public string password { get; set; }
            }
        }

        #endregion
    }
}


