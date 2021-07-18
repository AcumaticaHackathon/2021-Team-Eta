
using PX.Data;
using PX.Data.BQL;

namespace JsonConfigurator.Attributes
{
    public class DirectionType : PXStringListAttribute
    {
        public const string OutboundRequest = "ORQ";
        public const string OutboundResponse = "ORS";
        public const string InboundRequest = "IRQ";
        public const string InboundResponse = "IRS";
        
        
        public DirectionType() : base(new []{"ORQ", "ORS", "IRQ", "IRS"}, new []{"Outbound Request", "Outbound Response", "Inbound Request", "Inbound Response"})
        {
            
        }

        public class outboundRequest : BqlString.Constant<outboundRequest>
        {
            public outboundRequest() : base(OutboundRequest)
            {
            }
        }

        public class outboundResponse : BqlString.Constant<outboundResponse>
        {
            public outboundResponse() : base(OutboundResponse)
            {
            }
        }

        public class inboundRequest : BqlString.Constant<inboundRequest>
        {
            public inboundRequest() : base(InboundRequest)
            {
            }
        }

        public class inboundResponse : BqlString.Constant<inboundResponse>
        {
            public inboundResponse() : base(InboundResponse)
            {
            }
        }

        
        
    }
}