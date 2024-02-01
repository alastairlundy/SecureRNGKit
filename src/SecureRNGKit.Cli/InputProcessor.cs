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
    public class InputProcessor
    {
        public void ExecuteCommand(KeyValuePair<string[], SecureRngKitCommand> commandArgPair)
        {
            switch (commandArgPair.Value)
            {
                case SecureRngKitCommand.Generate:
                    GenerateSecureRandom(commandArgPair.Key);
                    break;
                case SecureRngKitCommand.Help:
                    DisplayHelp(commandArgPair.Key);
                    break;
                case SecureRngKitCommand.InvalidCommand:
                    DisplayInvalidCommand(commandArgPair.Key);
                    break;
                case SecureRngKitCommand.UserInitiatedCheckForUpdates:
                    DisplayUpdates(commandArgPair.Key, true);
                    break;
                case SecureRngKitCommand.AppInitiatedCheckForUpdates:
                    DisplayUpdates(commandArgPair.Key, false);
                    break;
                default:
                    DisplayHelp(commandArgPair.Key);
                    break;
            }
        }

        protected void DisplayHelp(string[] args)
        {

        }
        protected void DisplayUpdates(string[] args, bool userRequested)
        {
            if (userRequested)
            {
                
            }
            else if (!userRequested)
            {
                
            }
        }
        protected void DisplayInvalidCommand(string[] args)
        {
            
        }

        protected void GenerateSecureRandom(string[] args)
        {
            
        }
    }
}