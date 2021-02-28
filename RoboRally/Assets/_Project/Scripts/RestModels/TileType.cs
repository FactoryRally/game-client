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
    /// The type defines the function of a tile and how it is displayed. The behaviour is unknown to the client
    /// </summary>
    /// <value>The type defines the function of a tile and how it is displayed. The behaviour is unknown to the client</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum TileType
    {
        /// <summary>
        /// Enum Normal for value: normal
        /// </summary>
        [EnumMember(Value = "normal")]
        Normal = 1,

        /// <summary>
        /// Enum Rotator for value: rotator
        /// </summary>
        [EnumMember(Value = "rotator")]
        Rotator = 2,

        /// <summary>
        /// Enum Wall for value: wall
        /// </summary>
        [EnumMember(Value = "wall")]
        Wall = 3,

        /// <summary>
        /// Enum Priocore for value: prio_core
        /// </summary>
        [EnumMember(Value = "prio_core")]
        Priocore = 4,

        /// <summary>
        /// Enum Conveyor for value: conveyor
        /// </summary>
        [EnumMember(Value = "conveyor")]
        Conveyor = 5,

        /// <summary>
        /// Enum TrapDoor for value: trap-door
        /// </summary>
        [EnumMember(Value = "trap-door")]
        TrapDoor = 6,

        /// <summary>
        /// Enum Stomper for value: stomper
        /// </summary>
        [EnumMember(Value = "stomper")]
        Stomper = 7,

        /// <summary>
        /// Enum Radioactive for value: radioactive
        /// </summary>
        [EnumMember(Value = "radioactive")]
        Radioactive = 8,

        /// <summary>
        /// Enum Repairsite for value: repair site
        /// </summary>
        [EnumMember(Value = "repair site")]
        Repairsite = 9,

        /// <summary>
        /// Enum Button for value: Button
        /// </summary>
        [EnumMember(Value = "Button")]
        Button = 10,

        /// <summary>
        /// Enum OneWayWall for value: One Way Wall
        /// </summary>
        [EnumMember(Value = "One Way Wall")]
        OneWayWall = 11,

        /// <summary>
        /// Enum Puddle for value: Puddle
        /// </summary>
        [EnumMember(Value = "Puddle")]
        Puddle = 12,

        /// <summary>
        /// Enum Pit for value: pit
        /// </summary>
        [EnumMember(Value = "pit")]
        Pit = 13,

        /// <summary>
        /// Enum Ramp for value: Ramp
        /// </summary>
        [EnumMember(Value = "Ramp")]
        Ramp = 14

    }

}
