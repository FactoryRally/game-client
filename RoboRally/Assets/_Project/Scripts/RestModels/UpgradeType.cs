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
    /// Defines the type (the code/actions) this card will have * &#x60;generator&#x60; : Generates *x* energy every round
    /// </summary>
    /// <value>Defines the type (the code/actions) this card will have * &#x60;generator&#x60; : Generates *x* energy every round</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum UpgradeType
    {
        /// <summary>
        /// Enum Generator for value: generator
        /// </summary>
        [EnumMember(Value = "generator")]
        Generator = 1

    }

}
