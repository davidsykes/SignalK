using FluentAssertions;
using SignalKLibrary.DataListener;
using SignalKLibrary.DataListener.Interfaces;
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
            var deltaMessage = @"{""context"":""vessels.urn:mrn:signalk:uuid:ee"",
            ""updates"":[
                {""$source"":""ws.AUTO"",""timestamp"":""2024-02-07T07:23:49Z"",
                    ""values"":[{ ""path"":""home.temperature1"",""value"":""value1""}]},
                {""$source"":""ws.AUTO"",""timestamp"":""2024-02-08T07:23:48.49Z"",
                    ""values"":[{ ""path"":""home.temperature2"",""value"":""value2""},
                                { ""path"":""home.temperature2b"",""value"":""value2b""}]},
                {""$source"":""ws.AUTO"",""timestamp"":""2024-02-09T07:23:48.49Z"",
                    ""values"":[{ ""path"":""home.temperature3"",""value"":""value3""}]}]}
";

            var message = _converter.ConvertMessage(deltaMessage);

            message.Context.Should().Be("vessels.urn:mrn:signalk:uuid:ee");
            message.Updates.Count.Should().Be(3);
            message.Updates[0].Source.Should().Be("ws.AUTO");
            message.Updates[0].TimeStamp.Should().Be(new DateTime(2024, 2, 7, 7, 23, 49));
            message.Updates[0].Values.Count.Should().Be(1);
            message.Updates[0].Values[0].Path.Should().Be("home.temperature1");
            message.Updates[0].Values[0].Value.ToString().Should().Be("value1");
            message.Updates[1].Values.Count.Should().Be(2);
            message.Updates[1].Values[0].Path.Should().Be("home.temperature2");
            message.Updates[1].Values[0].Value.ToString().Should().Be("value2");
            message.Updates[1].Values[1].Path.Should().Be("home.temperature2b");
            message.Updates[1].Values[1].Value.ToString().Should().Be("value2b");
            message.Updates[2].Values[0].Path.Should().Be("home.temperature3");
            message.Updates[2].Values[0].Value.ToString().Should().Be("value3");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();

            _converter = new DeltaMessageConverter();
        }

        #endregion
    }
}


