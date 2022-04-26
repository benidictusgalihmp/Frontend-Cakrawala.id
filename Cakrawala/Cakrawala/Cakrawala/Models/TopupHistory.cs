using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class TopupHistory
    {
        [JsonProperty(PropertyName = "id")]
        public string topupId { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime createdAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime updatedAt { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public int value { get; set; }
        [JsonProperty(PropertyName = "method")]
        public string method { get; set; }
        [JsonProperty(PropertyName = "status")]
        public TransferStatus? status { get; set; }

        public TopupHistory(string topupId,
                            DateTime createdAt,
                            DateTime updatedAt,
                            int value,
                            string method,
                            TransferStatus? status)
        { 
            this.topupId = topupId;
            this.createdAt = createdAt.ToLocalTime();
            this.updatedAt = updatedAt.ToLocalTime();
            this.method = method;
            this.status = status != null ? status : TransferStatus.SUCCESS;
            this.value = value;
        }

        public TopupHistory(TopupHistory topupHistory) { 
            this.topupId=topupHistory.topupId;
            this.createdAt=topupHistory.createdAt;
            this.status=topupHistory.status;
            this.value = topupHistory.value;
            this.updatedAt=topupHistory.updatedAt;
            this.method = topupHistory.method;  
        }

        public TopupHistory()
        {
            this.topupId = "";
            this.createdAt = new DateTime().ToLocalTime();
            this.updatedAt = new DateTime().ToLocalTime();
            this.method = "";
            this.status = TransferStatus.PENDING;
            this.value = 0;
        }
    }
}
