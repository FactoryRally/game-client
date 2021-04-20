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
    /// A tile is a square at the Game field&lt;br&gt; **Note:**  * &#x60;direction&#x60; is only aviable for belts * properties with a &#x60;rotator-&#x60; prefix are only aviable for rotator (parts)
    /// </summary>
    [DataContract(Name = "Tile")]
    public partial class Tile : IEquatable<Tile>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = false)]
        public TileType Type { get; set; }
        /// <summary>
        /// Gets or Sets Direction
        /// </summary>
        [DataMember(Name = "direction", EmitDefaultValue = false)]
        public Direction? Direction { get; set; }
        /// <summary>
        /// Gets or Sets RotatorDirection
        /// </summary>
        [DataMember(Name = "rotator-direction", EmitDefaultValue = false)]
        public Rotation? RotatorDirection { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Tile() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        /// <param name="type">type (required).</param>
        /// <param name="empty">If there is no player at the tile this is true.</param>
        /// <param name="direction">direction.</param>
        /// <param name="rotatorDirection">rotatorDirection.</param>
        /// <param name="level">The height of the tile. 1 &#x3D; default (required) (default to 1).</param>
        /// <param name="order">Descibes the interal order of the tile type: eg. if order is 3 it is the 4th of its tile-type (because it is 0 based)  Default -1 means the oder has no use to this tile(type) (default to -1).</param>
        public Tile(TileType type = default(TileType), bool empty = default(bool), Direction? direction = default(Direction?), Rotation? rotatorDirection = default(Rotation?), int level = 1, int order = -1)
        {
            this.Type = type;
            this.Level = level;
            this.Empty = empty;
            this.Direction = direction;
            this.RotatorDirection = rotatorDirection;
            this.Order = order;
        }

        /// <summary>
        /// If there is no player at the tile this is true
        /// </summary>
        /// <value>If there is no player at the tile this is true</value>
        [DataMember(Name = "empty", EmitDefaultValue = false)]
        public bool Empty { get; set; }

        /// <summary>
        /// The height of the tile. 1 &#x3D; default
        /// </summary>
        /// <value>The height of the tile. 1 &#x3D; default</value>
        [DataMember(Name = "level", IsRequired = true, EmitDefaultValue = false)]
        public int Level { get; set; }

        /// <summary>
        /// Descibes the interal order of the tile type: eg. if order is 3 it is the 4th of its tile-type (because it is 0 based)  Default -1 means the oder has no use to this tile(type)
        /// </summary>
        /// <value>Descibes the interal order of the tile type: eg. if order is 3 it is the 4th of its tile-type (because it is 0 based)  Default -1 means the oder has no use to this tile(type)</value>
        [DataMember(Name = "order", EmitDefaultValue = false)]
        public int Order { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Tile {\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Empty: ").Append(Empty).Append("\n");
            sb.Append("  Direction: ").Append(Direction).Append("\n");
            sb.Append("  RotatorDirection: ").Append(RotatorDirection).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
            sb.Append("  Order: ").Append(Order).Append("\n");
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
            return this.Equals(input as Tile);
        }

        /// <summary>
        /// Returns true if Tile instances are equal
        /// </summary>
        /// <param name="input">Instance of Tile to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Tile input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.Empty == input.Empty ||
                    this.Empty.Equals(input.Empty)
                ) && 
                (
                    this.Direction == input.Direction ||
                    this.Direction.Equals(input.Direction)
                ) && 
                (
                    this.RotatorDirection == input.RotatorDirection ||
                    this.RotatorDirection.Equals(input.RotatorDirection)
                ) && 
                (
                    this.Level == input.Level ||
                    this.Level.Equals(input.Level)
                ) && 
                (
                    this.Order == input.Order ||
                    this.Order.Equals(input.Order)
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
                hashCode = hashCode * 59 + this.Empty.GetHashCode();
                hashCode = hashCode * 59 + this.Direction.GetHashCode();
                hashCode = hashCode * 59 + this.RotatorDirection.GetHashCode();
                hashCode = hashCode * 59 + this.Level.GetHashCode();
                hashCode = hashCode * 59 + this.Order.GetHashCode();
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
            // Level (int) maximum
            if(this.Level > (int)3)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Level, must be a value less than or equal to 3.", new [] { "Level" });
            }

            // Level (int) minimum
            if(this.Level < (int)1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Level, must be a value greater than or equal to 1.", new [] { "Level" });
            }

            yield break;
        }
    }

}
