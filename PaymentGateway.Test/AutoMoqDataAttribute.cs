using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace PaymentGateway.Test
{
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}