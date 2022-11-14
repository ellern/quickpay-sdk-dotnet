using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Primitives;
using QuickPay.SDK.Models.Callbacks;

namespace QuickPay.SDK.Clients
{
    public class CallbacksClient : BaseClient
    {
        private readonly string _privateKey;

        public CallbacksClient(string privateKey) : base(null)
        {
            _privateKey = privateKey;
        }

        /// <summary>
        /// Deserializes a string of JSON to Callback model
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public Callback Deserialize(string requestBody) => requestBody == null ? null : JSON.Deserialize<Callback>(requestBody);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public bool Verify(IDictionary<string, StringValues> headers, string requestBody) => Verify(DeserializeHeaders(headers)?.Checksum, requestBody);

        /// <summary>
        /// Verifies the request body with the checksum and private key
        /// </summary>
        /// <param name="checksum">Checksum from request header: QuickPay-Checksum-Sha256</param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public bool Verify(string checksum, string requestBody) => checksum?.Equals(Sign(requestBody, _privateKey)) ?? false;

        /// <summary>
        /// Verifies the request body and deserializes if verification succeeds
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public Callback VerifyAndDeserialize(IDictionary<string, StringValues> headers, string requestBody) => VerifyAndDeserialize(DeserializeHeaders(headers)?.Checksum, requestBody);

        /// <summary>
        /// Verifies the request body and deserializes if verification succeeds
        /// </summary>
        /// <param name="checksum"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public Callback VerifyAndDeserialize(string checksum, string requestBody)
        {
            if (!Verify(checksum, requestBody))
            {
                return null;
            }

            return Deserialize(requestBody);
        }

        /// <summary>
        /// Parses and deserializes the request headers
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public CallbackHeaders DeserializeHeaders(IDictionary<string, StringValues> headers)
        {
            if (headers == null || headers.Count == 0)
                return null;

            var model = new CallbackHeaders();

            if (headers.ContainsKey("QuickPay-Resource-Type"))
            {
                model.ResourceType = headers["QuickPay-Resource-Type"];
            }

            if (headers.ContainsKey("QuickPay-Account-ID"))
            {
                model.AccountId = headers["QuickPay-Account-ID"];
            }

            if (headers.ContainsKey("QuickPay-API-Version"))
            {
                model.ApiVersion = headers["QuickPay-API-Version"];
            }

            if (headers.ContainsKey("QuickPay-Checksum-Sha256"))
            {
                model.Checksum = headers["QuickPay-Checksum-Sha256"];
            }

            return model;
        }

        private string Sign(string value, string privateKey)
        {
            var e = Encoding.UTF8;

            var hmac = new HMACSHA256(e.GetBytes(privateKey));
            byte[] b = hmac.ComputeHash(e.GetBytes(value));

            var s = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                s.Append(b[i].ToString("x2"));
            }

            return s.ToString();
        }
    }
}
