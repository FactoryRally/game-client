using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboRally.Utils {
	public enum LobbyCallState {
		NONE,
		SCANNING,
		SCANNED,
		LOADING,
		LOADED,
		NO_GAMES_FOUND
	}
}