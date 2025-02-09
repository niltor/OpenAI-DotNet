using System.Text.Json.Serialization;

namespace OpenAI.Chat
{
    public sealed class Choice
    {
        [JsonInclude]
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Message Message { get; private set; }

        [JsonInclude]
        [JsonPropertyName("delta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Delta Delta { get; private set; }

        [JsonInclude]
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; private set; }

        [JsonInclude]
        [JsonPropertyName("finish_details")]
        public FinishDetails FinishDetails { get; private set; }

        [JsonInclude]
        [JsonPropertyName("index")]
        public int Index { get; private set; }

        public override string ToString() => Message?.Content?.ToString() ?? Delta?.Content ?? string.Empty;

        public static implicit operator string(Choice choice) => choice.ToString();

        internal void CopyFrom(Choice other)
        {
            if (other?.Message != null)
            {
                Message = other.Message;
            }

            if (other?.Delta != null)
            {
                if (Message == null)
                {
                    Message = new Message(other.Delta);
                }
                else
                {
                    Message.CopyFrom(other.Delta);
                }
            }

            if (!string.IsNullOrWhiteSpace(other?.FinishReason))
            {
                FinishReason = other.FinishReason;
            }

            if (other?.FinishDetails != null)
            {
                FinishDetails = other.FinishDetails;
            }

            Index = other?.Index ?? 0;
        }
    }
}
