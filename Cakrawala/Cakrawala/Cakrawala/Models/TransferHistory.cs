using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class TransferHistory
    {
        [JsonProperty(PropertyName = "id")]
        public string transactionId { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        [JsonProperty(PropertyName = "sender_user_id")]
        public string from { get; set; }
        [JsonProperty(PropertyName = "receiver_user_id")]
        public string to { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public int baseValue { get; set; }
        public TransferStatus status { get; set; }

        public TransferHistory(
            string transactionId,
            DateTime createdAt,
            DateTime updatedAt,
            int from,
            int to,
            int baseValue,
            TransferStatus status)
        { 
            this.status = status;
            this.transactionId = transactionId;
            this.createdAt = createdAt; 
            this.updatedAt = updatedAt;
            this.from = from.ToString();
            this.to = to.ToString();
            this.baseValue = baseValue;
        }

        public TransferHistory()
        {
            this.status = TransferStatus.PENDING;
            this.transactionId = "";
            this.createdAt = new DateTime();
            this.updatedAt = new DateTime();
            this.from = "";
            this.to = "";
            this.baseValue = 0;
        }
    }
}
