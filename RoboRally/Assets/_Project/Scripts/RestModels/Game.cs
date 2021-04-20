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
    /// A Game is like a lobby, people can join/leave.&lt;br&gt; A Game is created by a host who does *not* needs to attend the game as a player but in the most cases he will. This is *read-only*
    /// </summary>
    [DataContract(Name = "Game")]
    public partial class Game : IEquatable<Game>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets RuntimeInfo
        /// </summary>
        [DataMember(Name = "runtime_info", EmitDefaultValue = false)]
        public GameState? RuntimeInfo { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Game() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        /// <param name="id">**Unique**&lt;br&gt; This is the parameter a game is identified by.</param>
        /// <param name="name">The name is **unique** but it should ***not*** be used as identifer (it&#39;s not natively supportet) It is used to display the game&#39;s name (required).</param>
        /// <param name="players">The list of players attending the game. (Only contains the name of the players).</param>
        /// <param name="runtimeInfo">runtimeInfo.</param>
        public Game(int id = default(int), string name = default(string), List<int> players = default(List<int>), GameState? runtimeInfo = default(GameState?))
        {
            // to ensure "name" is required (not null)
            this.Name = name ?? throw new ArgumentNullException("name is a required property for Game and cannot be null");
            this.Id = id;
            this.Players = players;
            this.RuntimeInfo = runtimeInfo;
        }

        /// <summary>
        /// **Unique**&lt;br&gt; This is the parameter a game is identified by
        /// </summary>
        /// <value>**Unique**&lt;br&gt; This is the parameter a game is identified by</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// The name is **unique** but it should ***not*** be used as identifer (it&#39;s not natively supportet) It is used to display the game&#39;s name
        /// </summary>
        /// <value>The name is **unique** but it should ***not*** be used as identifer (it&#39;s not natively supportet) It is used to display the game&#39;s name</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The list of players attending the game. (Only contains the name of the players)
        /// </summary>
        /// <value>The list of players attending the game. (Only contains the name of the players)</value>
        [DataMember(Name = "players", EmitDefaultValue = false)]
        public List<int> Players { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Game {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Players: ").Append(Players).Append("\n");
            sb.Append("  RuntimeInfo: ").Append(RuntimeInfo).Append("\n");
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
            return this.Equals(input as Game);
        }

        /// <summary>
        /// Returns true if Game instances are equal
        /// </summary>
        /// <param name="input">Instance of Game to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Game input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Players == input.Players ||
                    this.Players != null &&
                    input.Players != null &&
                    this.Players.SequenceEqual(input.Players)
                ) && 
                (
                    this.RuntimeInfo == input.RuntimeInfo ||
                    this.RuntimeInfo.Equals(input.RuntimeInfo)
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
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Players != null)
                    hashCode = hashCode * 59 + this.Players.GetHashCode();
                hashCode = hashCode * 59 + this.RuntimeInfo.GetHashCode();
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
            // Id (int) maximum
            if(this.Id > (int)2048)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Id, must be a value less than or equal to 2048.", new [] { "Id" });
            }

            // Id (int) minimum
            if(this.Id < (int)0)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Id, must be a value greater than or equal to 0.", new [] { "Id" });
            }

            // Name (string) maxLength
            if(this.Name != null && this.Name.Length > 20)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, length must be less than 20.", new [] { "Name" });
            }

            // Name (string) minLength
            if(this.Name != null && this.Name.Length < 3)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, length must be greater than 3.", new [] { "Name" });
            }

            // Name (string) pattern
            Regex regexName = new Regex(@"[A-Za-z]+[A-Za-z0-9 _-]+", RegexOptions.CultureInvariant);
            if (false == regexName.Match(this.Name).Success)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, must match a pattern of " + regexName, new [] { "Name" });
            }

            yield break;
        }
    }

}
