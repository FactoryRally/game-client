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
    /// When the next game phase started
    /// </summary>
    [DataContract]
    public partial class GamePhaseChangedEvent :  IEquatable<GamePhaseChangedEvent>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Phase
        /// </summary>
        [DataMember(Name="phase", EmitDefaultValue=false)]
        public RoundPhase Phase { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePhaseChangedEvent" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GamePhaseChangedEvent() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePhaseChangedEvent" /> class.
        /// </summary>
        /// <param name="phase">phase (required).</param>
        /// <param name="step">WIP! Currently class names. Enum later on  Describes the game phase more deeply (required).</param>
        /// <param name="information">Unspecified information about the game phase (you can also obtain this information in a typesave way using the GameAPI).</param>
        public GamePhaseChangedEvent(RoundPhase phase = default(RoundPhase), string step = default(string), Object information = default(Object))
        {
            this.Phase = phase;
            // to ensure "step" is required (not null)
            this.Step = step ?? throw new ArgumentNullException("step is a required property for GamePhaseChangedEvent and cannot be null");
            this.Information = information;
        }
        
        /// <summary>
        /// WIP! Currently class names. Enum later on  Describes the game phase more deeply
        /// </summary>
        /// <value>WIP! Currently class names. Enum later on  Describes the game phase more deeply</value>
        [DataMember(Name="step", EmitDefaultValue=false)]
        public string Step { get; set; }

        /// <summary>
        /// Unspecified information about the game phase (you can also obtain this information in a typesave way using the GameAPI)
        /// </summary>
        /// <value>Unspecified information about the game phase (you can also obtain this information in a typesave way using the GameAPI)</value>
        [DataMember(Name="information", EmitDefaultValue=false)]
        public Object Information { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GamePhaseChangedEvent {\n");
            sb.Append("  Phase: ").Append(Phase).Append("\n");
            sb.Append("  Step: ").Append(Step).Append("\n");
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
            return this.Equals(input as GamePhaseChangedEvent);
        }

        /// <summary>
        /// Returns true if GamePhaseChangedEvent instances are equal
        /// </summary>
        /// <param name="input">Instance of GamePhaseChangedEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GamePhaseChangedEvent input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Phase == input.Phase ||
                    this.Phase.Equals(input.Phase)
                ) && 
                (
                    this.Step == input.Step ||
                    (this.Step != null &&
                    this.Step.Equals(input.Step))
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
                hashCode = hashCode * 59 + this.Phase.GetHashCode();
                if (this.Step != null)
                    hashCode = hashCode * 59 + this.Step.GetHashCode();
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
