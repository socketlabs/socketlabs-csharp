using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents MergeData for a single message.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class MergeDataJson
    {
        /// <summary>
        /// Creates a new instance of the MergeDataJson class.
        /// </summary>
        public MergeDataJson()
        {
            PerMessage = new List<List<MergeFieldJson>>();
            Global = new List<MergeFieldJson>();
        }

        /// <summary>
        /// Defines merge field data for each message.
        /// </summary>
        public List<List<MergeFieldJson>> PerMessage { get; set; }

        /// <summary>
        /// Defines merge field data for all messages in the request.
        /// </summary>
        public List<MergeFieldJson> Global { get; set; }

        #region Conditional Property Serialization

        /// <summary>
        /// Check if per message merge fields should be serialized.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializePerMessage()
        {
            return PerMessage == null || PerMessage.Count > 0;
        }

        /// <summary>
        /// Check if global merge fields should be serialized. Don't serialize the collection if they are null or empty.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializeGlobal()
        {
            return Global == null || Global.Count > 0;
        }

        #endregion
    }
}