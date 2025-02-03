using SamplePOS_ServerSide.Models;

namespace SamplePOS_ServerSide.Core
{
    public class MessageBus
    {
        public static event Action<InvoiceGeneratedMessage>? InvoiceGenerated;

        public static void PublishGeneratedInvoice(InvoiceGeneratedMessage message) => InvoiceGenerated?.Invoke(message);
    }

    public class InvoiceGeneratedMessage
    {
        public Invoice Invoice { get; set; }
        public InvoiceGeneratedMessage(Invoice invoice) => Invoice = invoice;
    }
}
