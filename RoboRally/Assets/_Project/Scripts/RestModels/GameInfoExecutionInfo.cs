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
    /// Information abou the executon cycle
    /// </summary>
    [DataContract]
    public partial class GameInfoExecutionInfo :  IEquatable<GameInfoExecutionInfo>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfoExecutionInfo" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GameInfoExecutionInfo() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfoExecutionInfo" /> class.
        /// </summary>
        /// <param name="currentRegister">The index of the register currently executed (required).</param>
        /// <param name="currentRobot">The index of the robot currently executing (required).</param>
        public GameInfoExecutionInfo(int currentRegister = default(int), int currentRobot = default(int))
        {
            this.CurrentRegister = currentRegister;
            this.CurrentRobot = currentRobot;
        }
        
        /// <summary>
        /// The index of the register currently executed
        /// </summary>
        /// <value>The index of the register currently executed</value>
        [DataMember(Name="currentRegister", EmitDefaultValue=false)]
        public int CurrentRegister { get; set; }

        /// <summary>
        /// The index of the robot currently executing
        /// </summary>
        /// <value>The index of the robot currently executing</value>
        [DataMember(Name="currentRobot", EmitDefaultValue=false)]
        public int CurrentRobot { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GameInfoExecutionInfo {\n");
            sb.Append("  CurrentRegister: ").Append(CurrentRegister).Append("\n");
            sb.Append("  CurrentRobot: ").Append(CurrentRobot).Append("\n");
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
            return this.Equals(input as GameInfoExecutionInfo);
        }

        /// <summary>
        /// Returns true if GameInfoExecutionInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of GameInfoExecutionInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GameInfoExecutionInfo input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CurrentRegister == input.CurrentRegister ||
                    this.CurrentRegister.Equals(input.CurrentRegister)
                ) && 
                (
                    this.CurrentRobot == input.CurrentRobot ||
                    this.CurrentRobot.Equals(input.CurrentRobot)
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
                hashCode = hashCode * 59 + this.CurrentRegister.GetHashCode();
                hashCode = hashCode * 59 + this.CurrentRobot.GetHashCode();
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