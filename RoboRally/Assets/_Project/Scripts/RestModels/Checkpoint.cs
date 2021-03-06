/* 
 * Robot Rally Game logic engine
 *
 * This api controlls the flow of a game and provides it's data. It is desiged to be RESTfull so the structure works simmilar as file system. The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan
 *
 * The version of the OpenAPI document: v2.7.0
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
    /// Checkpoints are entities
    /// </summary>
    [DataContract]
    public partial class Checkpoint :  IEquatable<Checkpoint>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Direction
        /// </summary>
        [DataMember(Name="direction", EmitDefaultValue=false)]
        public Direction Direction { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Checkpoint" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Checkpoint() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Checkpoint" /> class.
        /// </summary>
        /// <param name="number">The number of the checkpoint defining the order they need to be called in.</param>
        /// <param name="checkedBy">The entities (robots) which allready were at this point.</param>
        /// <param name="direction">direction (required).</param>
        /// <param name="name">The default rule for names in the game (required).</param>
        /// <param name="id">The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only.</param>
        /// <param name="location">location (required).</param>
        public Checkpoint(int number = default(int), List<int> checkedBy = default(List<int>), Direction direction = default(Direction), string name = default(string), int id = default(int), Position location = default(Position))
        {
            this.Direction = direction;
            // to ensure "name" is required (not null)
            this.Name = name ?? throw new ArgumentNullException("name is a required property for Checkpoint and cannot be null");
            // to ensure "location" is required (not null)
            this.Location = location ?? throw new ArgumentNullException("location is a required property for Checkpoint and cannot be null");
            this.Number = number;
            this.CheckedBy = checkedBy;
            this.Id = id;
        }
        
        /// <summary>
        /// The number of the checkpoint defining the order they need to be called in
        /// </summary>
        /// <value>The number of the checkpoint defining the order they need to be called in</value>
        [DataMember(Name="number", EmitDefaultValue=false)]
        public int Number { get; set; }

        /// <summary>
        /// The entities (robots) which allready were at this point
        /// </summary>
        /// <value>The entities (robots) which allready were at this point</value>
        [DataMember(Name="checked-by", EmitDefaultValue=false)]
        public List<int> CheckedBy { get; set; }

        /// <summary>
        /// The default rule for names in the game
        /// </summary>
        /// <value>The default rule for names in the game</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only
        /// </summary>
        /// <value>The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Location
        /// </summary>
        [DataMember(Name="location", EmitDefaultValue=false)]
        public Position Location { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Checkpoint {\n");
            sb.Append("  Number: ").Append(Number).Append("\n");
            sb.Append("  CheckedBy: ").Append(CheckedBy).Append("\n");
            sb.Append("  Direction: ").Append(Direction).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Location: ").Append(Location).Append("\n");
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
            return this.Equals(input as Checkpoint);
        }

        /// <summary>
        /// Returns true if Checkpoint instances are equal
        /// </summary>
        /// <param name="input">Instance of Checkpoint to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Checkpoint input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Number == input.Number ||
                    this.Number.Equals(input.Number)
                ) && 
                (
                    this.CheckedBy == input.CheckedBy ||
                    this.CheckedBy != null &&
                    input.CheckedBy != null &&
                    this.CheckedBy.SequenceEqual(input.CheckedBy)
                ) && 
                (
                    this.Direction == input.Direction ||
                    this.Direction.Equals(input.Direction)
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.Location == input.Location ||
                    (this.Location != null &&
                    this.Location.Equals(input.Location))
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
                hashCode = hashCode * 59 + this.Number.GetHashCode();
                if (this.CheckedBy != null)
                    hashCode = hashCode * 59 + this.CheckedBy.GetHashCode();
                hashCode = hashCode * 59 + this.Direction.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Location != null)
                    hashCode = hashCode * 59 + this.Location.GetHashCode();
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
            // Number (int) minimum
            if(this.Number < (int)0)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Number, must be a value greater than or equal to 0.", new [] { "Number" });
            }

            // Name (string) maxLength
            if(this.Name != null && this.Name.Length > 13)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, length must be less than 13.", new [] { "Name" });
            }

            // Name (string) minLength
            if(this.Name != null && this.Name.Length < 3)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, length must be greater than 3.", new [] { "Name" });
            }

            // Name (string) pattern
            Regex regexName = new Regex(@"[A-Za-z]+[A-Za-z0-9_ -]+[A-Za-z0-9]{1}", RegexOptions.CultureInvariant);
            if (false == regexName.Match(this.Name).Success)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, must match a pattern of " + regexName, new [] { "Name" });
            }

            // Id (int) minimum
            if(this.Id < (int)1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Id, must be a value greater than or equal to 1.", new [] { "Id" });
            }

            yield break;
        }
    }

}
