using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;

namespace AdventOfCode2021Tests
{
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects) : base(() => new Fixture()
                .Customize(new AutoMoqCustomization { ConfigureMembers = true }),
            objects)

        { }
    }
}