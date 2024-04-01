using FluentAssertions;
using Logic;

namespace Tests
{
    public class SignalKHelloMessageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var message = new SignalKHelloMessage("""{"name":"signalk-server","version":"2.6.2","self":"vessels.urn:mrn:signalk:uuid:bb9ba4df-0102-41fc-8dbd-348882b5e302","roles":["master","main"],"timestamp":"2024-04-01T06:20:33.222Z"}""");

            message.Name.Should().Be("signalk-server");
            message.Version.Should().Be("2.6.2");
            message.Self.Should().Be("vessels.urn:mrn:signalk:uuid:bb9ba4df-0102-41fc-8dbd-348882b5e302");
        }
    }
}