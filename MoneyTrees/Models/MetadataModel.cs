using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class MetadataModel
    {
        public partial class Metadata
        {
            [JsonProperty("faster_payment", NullValueHandling = NullValueHandling.Ignore)]
            public bool? FasterPayment { get; set; }

            [JsonProperty("insertion", NullValueHandling = NullValueHandling.Ignore)]
            public string Insertion { get; set; }

            [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
            public string Notes { get; set; }

            [JsonProperty("trn", NullValueHandling = NullValueHandling.Ignore)]
            public string Trn { get; set; }

            [JsonProperty("ledger_insertion_id", NullValueHandling = NullValueHandling.Ignore)]
            public string LedgerInsertionId { get; set; }

            [JsonProperty("mastercard_auth_message_id", NullValueHandling = NullValueHandling.Ignore)]
            public string MastercardAuthMessageId { get; set; }

            [JsonProperty("mastercard_lifecycle_id", NullValueHandling = NullValueHandling.Ignore)]
            public string MastercardLifecycleId { get; set; }

            [JsonProperty("hide_amount", NullValueHandling = NullValueHandling.Ignore)]
            public bool? HideAmount { get; set; }

            [JsonProperty("hide_transaction", NullValueHandling = NullValueHandling.Ignore)]
            public bool? HideTransaction { get; set; }

            [JsonProperty("action_code", NullValueHandling = NullValueHandling.Ignore)]
            public string ActionCode { get; set; }

            [JsonProperty("faster_payment_initiator", NullValueHandling = NullValueHandling.Ignore)]
            public string FasterPaymentInitiator { get; set; }

            [JsonProperty("p2p_transfer_id", NullValueHandling = NullValueHandling.Ignore)]
            public string P2PTransferId { get; set; }

            [JsonProperty("fps_payment_id", NullValueHandling = NullValueHandling.Ignore)]
            public string FpsPaymentId { get; set; }

            [JsonProperty("subscription_id", NullValueHandling = NullValueHandling.Ignore)]
            public string SubscriptionId { get; set; }

            [JsonProperty("is_reversal", NullValueHandling = NullValueHandling.Ignore)]

            public bool? IsReversal { get; set; }

            [JsonProperty("mastercard_internal_message_id", NullValueHandling = NullValueHandling.Ignore)]
            public string MastercardInternalMessageId { get; set; }

            [JsonProperty("reversal_type", NullValueHandling = NullValueHandling.Ignore)]

            public long? ReversalType { get; set; }

            [JsonProperty("mastercard_clearing_message_id", NullValueHandling = NullValueHandling.Ignore)]
            public string MastercardClearingMessageId { get; set; }

            [JsonProperty("mcc", NullValueHandling = NullValueHandling.Ignore)]

            public long? Mcc { get; set; }

            [JsonProperty("mastercard_approval_type", NullValueHandling = NullValueHandling.Ignore)]
            public string MastercardApprovalType { get; set; }

            [JsonProperty("trip_id", NullValueHandling = NullValueHandling.Ignore)]
            public string TripId { get; set; }

            [JsonProperty("bacs_direct_debit_instruction_id", NullValueHandling = NullValueHandling.Ignore)]
            public string BacsDirectDebitInstructionId { get; set; }

            [JsonProperty("bacs_payment_id", NullValueHandling = NullValueHandling.Ignore)]
            public string BacsPaymentId { get; set; }

            [JsonProperty("bacs_record_id", NullValueHandling = NullValueHandling.Ignore)]
            public string BacsRecordId { get; set; }
        }
    }
}