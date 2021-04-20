/*
 * Robot Rally Game logic engine
 *
 * This api controlls the flow of a game and provides it's data. It is desiged to be RESTfull so the structure works simmilar as file system. The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan
 *
 * The version of the OpenAPI document: v2.11.0
 * Contact: nbrugger@student.tgm.ac.at
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;


namespace Tgm.Roborally.Api.Model
{
    /// <summary>
    /// Used to store any event and generalize them into a single type
    /// </summary>
    [DataContract(Name = "GenericEvent")]
    public partial class GenericEvent : IEquatable<GenericEvent>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = false)]
        public EventType Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEvent" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GenericEvent() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEvent" /> class.
        /// </summary>
        /// <param name="type">type (required).</param>
        /// <param name="data">This is the data for the Event. In the case of type beeing &#x60;lazer hit&#x60;, data will be of the type &#x60;LazerHitEvent&#x60;. So the object-type allways matches to the &#x60;type&#x60; field .</param>
        public GenericEvent(EventType type = default(EventType), Object data = default(Object))
        {
            this.Type = type;
            this.Data = data;
        }

        /// <summary>
        /// This is the data for the Event. In the case of type beeing &#x60;lazer hit&#x60;, data will be of the type &#x60;LazerHitEvent&#x60;. So the object-type allways matches to the &#x60;type&#x60; field 
        /// </summary>
        /// <value>This is the data for the Event. In the case of type beeing &#x60;lazer hit&#x60;, data will be of the type &#x60;LazerHitEvent&#x60;. So the object-type allways matches to the &#x60;type&#x60; field </value>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Object Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenericEvent {\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as GenericEvent);
        }

        /// <summary>
        /// Returns true if GenericEvent instances are equal
        /// </summary>
        /// <param name="input">Instance of GenericEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GenericEvent input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.Data == input.Data ||
                    (this.Data != null &&
                    this.Data.Equals(input.Data))
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
                hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Data != null)
                    hashCode = hashCode * 59 + this.Data.GetHashCode();
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
