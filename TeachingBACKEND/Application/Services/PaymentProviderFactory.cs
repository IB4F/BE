using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class PaymentProviderFactory
    {
        private readonly IEnumerable<IPaymentProvider> _providers;

        public PaymentProviderFactory(IEnumerable<IPaymentProvider> providers)
        {
            _providers = providers;
        }

        public IPaymentProvider GetProvider(PaymentProvider providerType)
        {
            var provider = _providers.FirstOrDefault(p => p.ProviderType == providerType);
            if (provider == null)
                throw new NotSupportedException($"Payment provider '{providerType}' is not registered.");
            return provider;
        }
    }
}