/* 
 * Robot Rally Game logic engine
 *
 * This api controlls the flow of a game and provides it's data. It is desiged to be RESTfull so the structure works simmilar as file system. The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan
 *
 * The version of the OpenAPI document: v2.2.0
 * Contact: nbrugger@student.tgm.ac.at
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Tgm.Roborally.Api.Model
{
    /// <summary>
    /// A shop to buy upgrades from. Each element is buyable once and then vanishes from the list
    /// </summary>
    [DataContract]
    public partial class UpgradeShop :  IEquatable<UpgradeShop>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeShop" /> class.
        /// </summary>
        /// <param name="upgrades">upgrades.</param>
        /// <param name="information">information.</param>
        public UpgradeShop(List<int> upgrades = default(List<int>), UpgradeShopInformation information = default(UpgradeShopInformation))
        {
            this.Upgrades = upgrades;
            this.Information = information;
        }
        
        /// <summary>
        /// Gets or Sets Upgrades
        /// </summary>
        [DataMember(Name="upgrades", EmitDefaultValue=false)]
        public List<int> Upgrades { get; set; }

        /// <summary>
        /// Gets or Sets Information
        /// </summary>
        [DataMember(Name="information", EmitDefaultValue=false)]
        public UpgradeShopInformation Information { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UpgradeShop {\n");
            sb.Append("  Upgrades: ").Append(Upgrades).Append("\n");
            sb.Append("  Information: ").Append(Information).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as UpgradeShop);
        }

        /// <summary>
        /// Returns true if UpgradeShop instances are equal
        /// </summary>
        /// <param name="input">Instance of UpgradeShop to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpgradeShop input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Upgrades == input.Upgrades ||
                    this.Upgrades != null &&
                    input.Upgrades != null &&
                    this.Upgrades.SequenceEqual(input.Upgrades)
                ) && 
                (
                    this.Information == input.Information ||
                    (this.Information != null &&
                    this.Information.Equals(input.Information))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Upgrades != null)
                    hashCode = hashCode * 59 + this.Upgrades.GetHashCode();
                if (this.Information != null)
                    hashCode = hashCode * 59 + this.Information.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
