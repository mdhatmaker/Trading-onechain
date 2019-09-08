using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;

namespace EZAPI.Containers
{
    /// <summary>
    /// An InstrumentDescriptor object contains all the information necessary to construct a
    /// TTOrder object. For example, if you know the 4 strings necessary to construct an order
    /// (i.e. "CME", "FUTURE", "CL", "Nov12"), you can create an InstrumentDescriptor and then
    /// create a TTOrder using that InstrumentDescriptor.
    /// </summary>
    public class InstrumentDescriptor
    {
        /// <summary>
        /// Product key for the instrument we will subscribe to
        /// </summary>
        public ProductKey ProductKey { get; set; }
        /// <summary>
        /// Market for the instrument we will subscribe to (i.e. "CME")
        /// </summary>
        public string MarketName { get; set; }
        /// <summary>
        /// Product type for the instrument we will subscribe to (i.e. "FUTURE")
        /// </summary>
        public string ProductTypeName { get; set; }
        /// <summary>
        /// Product for the instrument we will subscribe to (i.e. "CL")
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Contract for the instrument we will subscribe to (i.e. "Nov12")
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// A compact view of the information used to subscribe to an instrument separated
        /// by the "|" character (i.e. "CME|FUTURE|CL|Nov12")
        /// </summary>
        public string DescriptorString
        {
            get
            {
                return string.Format("{0}|{1}|{2}|{3}", MarketName, ProductTypeName, ProductName, ContractName);
            }
        }

        private InstrumentDescriptor()
        {

        }

        /// <summary>
        /// Construct an InstrumentDescriptor object with the four strings that make up the
        /// necessary information
        /// </summary>
        /// <param name="market">Market for the requested instrument (i.e. "CME")</param>
        /// <param name="productType">Product type for the requested instrument (i.e. "FUTURE")</param>
        /// <param name="product">Product for the requested instrument (i.e. "CL")</param>
        /// <param name="contract">Contract for the requested instrument (i.e. "Nov12")</param>
        public InstrumentDescriptor(string market, string productType, string product, string contract) : this()
        {
            MarketName = market;
            ProductTypeName = productType;
            ProductName = product;
            ContractName = contract;
            
            ProductKey = new ProductKey(MarketName, ProductTypeName, ProductName);
        }

        /// <summary>
        /// Create an InstrumentDescriptor using a single string containing all the required
        /// information (with the "|" char as the separator).
        /// </summary>
        /// <param name="descriptor"></param>
        /// <example>InstrumentDescriptor id = new InstrumentDescriptor("CME|FUTURE|HG|Dec12")</example>
        public InstrumentDescriptor(string descriptor) : this()
        {
            string[] items = descriptor.Split('|');
            MarketName = items[0].Trim();
            ProductTypeName = items[1].Trim();
            ProductName = items[2].Trim();
            ContractName = items[3].Trim();
            
            ProductKey = new ProductKey(MarketName, ProductTypeName, ProductName);
        }

        public override string ToString()
        {
            return DescriptorString;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            InstrumentDescriptor compareObject = obj as InstrumentDescriptor;

            result = compareObject.ProductKey.Equals(this.ProductKey) && compareObject.ContractName.Equals(this.ContractName);

            return result;
        }

        // Two objects that are equal MUST return the same value for GetHashCode. There may be multiple
        // objects with the same GetHashCode value, and this will just cause a "collision" when indexing.
        public override int GetHashCode()
        {
            return ProductKey.GetHashCode();
        }
    } //class
} // namespace