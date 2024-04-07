using FluentAssertions;
using Logic;
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

        readonly string _guid = "12345678";
        readonly string _userName = "user name";
        readonly string _password = "pass word";
        readonly string _logInResponse = "{\"state\":\"COMPLETED\",\"statusCode\":200,\"login\":{\"token\":\"eyJh\"}}";
        string _logInMessage = string.Empty;
        JsonSerializerOptions _jsonOptions;

        Mock<IClientWebSocketWrapper> _mockClientWebSocketWrapper;
        Mock<IGuidWrapper> _mockGuidWrapper;

        [Test]
        public async Task TheLogInMessageIsSentToTheServer()
        {
            await _handler.LogIn();

            _mockClientWebSocketWrapper.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);

            var logIn = DeserialiseLogInFromJsonMessage(_logInMessage);
            logIn.requestid.Should().Be(_guid);
            logIn.login.username.Should().Be(_userName);
            logIn.login.password.Should().Be(_password);
        }

        [Test]
        public async Task IfTheLogInFailsAnExceptionIsThrown()
        {
            SetUpLogInResponse("{\"state\":\"COMPLETED\",\"statusCode\":401,\"login\":{}}");

            Func<Task> action = async () => await _handler.LogIn();
            await action.Should().ThrowAsync<SKLibraryException>().WithMessage("Login failed.");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _handler = new SKLogInHandler(_userName, _password, _mockClientWebSocketWrapper.Object, _mockGuidWrapper.Object);
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

            _mockClientWebSocketWrapper = new Mock<IClientWebSocketWrapper>();
            _mockGuidWrapper = new Mock<IGuidWrapper>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockGuidWrapper.Setup(m => m.NewGuid()).Returns(_guid);
            _mockClientWebSocketWrapper.Setup(m => m.SendMessage(It.IsAny<string>()))
                .Callback((string message) => _logInMessage = message);
            SetUpLogInResponse(_logInResponse);
        }

        void SetUpLogInResponse(string response)
        {
            _mockClientWebSocketWrapper.Setup(m => m.ReceiveMessage())
                .Returns(Task.FromResult(response));
        }

#pragma warning disable IDE1006 // Naming Styles
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
#pragma warning restore IDE1006 // Naming Styles

        private ExpectedLogInMessage DeserialiseLogInFromJsonMessage(string logInMessage)
        {
            return JsonSerializer.Deserialize<ExpectedLogInMessage>(_logInMessage, _jsonOptions);
        }

        #endregion
    }
}


