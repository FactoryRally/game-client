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
	/// Defines Robots
	/// </summary>

	[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum Robots {

		/// <summary>
		/// Enum Tank for tank
		/// </summary>
		[EnumMember(Value = "tank")]
		Tank = 1,

		/// <summary>
		/// Enum Drone for drone
		/// </summary>
		[EnumMember(Value = "drone")]
		Drone = 2,

		/// <summary>
		/// Enum Bulldozer for bulldozer
		/// </summary>
		[EnumMember(Value = "bulldozer")]
		Bulldozer = 3,

		/// <summary>
		/// Enum Turtle for turtle
		/// </summary>
		[EnumMember(Value = "turtle")]
		Turtle = 4,

		/// <summary>
		/// Enum WallE for wallE
		/// </summary>
		[EnumMember(Value = "wallE")]
		WallE = 5,

		/// <summary>
		/// Enum Mech for mech
		/// </summary>
		[EnumMember(Value = "mech")]
		Mech = 6
	}

}
