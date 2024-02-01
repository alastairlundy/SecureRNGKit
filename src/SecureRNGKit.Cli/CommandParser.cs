/*
SecureRNGKit
Copyright (C) 2019-2024  Alastair Lundy
   
This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
USA
*/

using System.Collections.Generic;
using SecureRNGKit.Cli.enums;

namespace SecureRNGKit.Cli
{
    public static class CommandParser
    {
        public static KeyValuePair<string[], SecureRngKitCommand> ToSecureRngKitCommand(string[] args)
        {
            if (args[0].ToString().ToLower().Contains("generate") ||
                args[0].ToString().ToLower().Contains("gen") ||
                args[0].ToString().ToLower().Contains("-g") ||
                args[0].ToString().ToLower().Contains("--gen"))
            {
                return new KeyValuePair<string[], SecureRngKitCommand>(args, SecureRngKitCommand.Generate);
            }
            else if (args[0].ToString().ToLower().Contains("help") || 
                args[0].ToString().ToLower().Contains("-h") ||
                args[0].ToString().ToLower().Contains("--help"))
            {
                return new KeyValuePair<string[], SecureRngKitCommand>(args, SecureRngKitCommand.Help);
            }
            else if (args[0].ToString().ToLower().Contains("update") ||
                args[0].ToString().ToLower().Contains("--update") ||
                args[0].ToString().ToLower().Contains("-update") ||
                args[0].ToString().ToLower().Contains("check") && args[0].ToString().ToLower().Contains("update"))
            {
                return new KeyValuePair<string[], SecureRngKitCommand>(args, SecureRngKitCommand.UserInitiatedCheckForUpdates);
            }
            else
            {
                return new KeyValuePair<string[], SecureRngKitCommand>(args, SecureRngKitCommand.InvalidCommand);
            }
        }
    }
}