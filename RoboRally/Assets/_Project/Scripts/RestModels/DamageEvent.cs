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
    /// Happens when a entity takes damage
    /// </summary>
    [DataContract]
    public partial class DamageEvent :  IEquatable<DamageEvent>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DamageEvent" /> class.
        /// </summary>
        /// <param name="entity">The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only.</param>
        /// <param name="ammount">The number of damage points.</param>
        public DamageEvent(int entity = default(int), int ammount = default(int))
        {
            this.Entity = entity;
            this.Ammount = ammount;
        }
        
        /// <summary>
        /// The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only
        /// </summary>
        /// <value>The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only</value>
        [DataMember(Name="entity", EmitDefaultValue=false)]
        public int Entity { get; set; }

        /// <summary>
        /// The number of damage points
        /// </summary>
        /// <value>The number of damage points</value>
        [DataMember(Name="ammount", EmitDefaultValue=false)]
        public int Ammount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DamageEvent {\n");
            sb.Append("  Entity: ").Append(Entity).Append("\n");
            sb.Append("  Ammount: ").Append(Ammount).Append("\n");
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
            return this.Equals(input as DamageEvent);
        }

        /// <summary>
        /// Returns true if DamageEvent instances are equal
        /// </summary>
        /// <param name="input">Instance of DamageEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DamageEvent input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Entity == input.Entity ||
                    this.Entity.Equals(input.Entity)
                ) && 
                (
                    this.Ammount == input.Ammount ||
                    this.Ammount.Equals(input.Ammount)
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
                hashCode = hashCode * 59 + this.Entity.GetHashCode();
                hashCode = hashCode * 59 + this.Ammount.GetHashCode();
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
            // Entity (int) minimum
            if(this.Entity < (int)0)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Entity, must be a value greater than or equal to 0.", new [] { "Entity" });
            }

            // Ammount (int) maximum
            if(this.Ammount > (int)5)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Ammount, must be a value less than or equal to 5.", new [] { "Ammount" });
            }

            // Ammount (int) minimum
            if(this.Ammount < (int)0)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Ammount, must be a value greater than or equal to 0.", new [] { "Ammount" });
            }

            yield break;
        }
    }

}