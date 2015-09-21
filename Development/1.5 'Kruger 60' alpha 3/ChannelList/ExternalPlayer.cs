// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using Project.DvbIpTv.UiServices.Configuration.Settings.TvPlayers;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public static class ExternalPlayer
    {
        private static string[] LaunchParamKeys;

        public static void Launch(TvPlayer player, UiBroadcastService service, bool throughShortcut)
        {
            if (!File.Exists(player.Path))
            {
                var ex = new FileNotFoundException();
                throw new FileNotFoundException(ex.Message + "\r\n" + player.Path);
            } // if

            if (LaunchParamKeys == null)
            {
                LaunchParamKeys = new string[]
                {
                    "Channel.Url",
                    "Channel.Name",
                    "Channel.Description",
                    "Channel.Icon.Path",
                };
            } // if

            var paramValues = new string[]
            {
                service.LocationUrl,
                service.DisplayName,
                service.DisplayDescription,
                service.Logo.GetLogoIconPath(),
            };

            var parameters = ArgumentsManager.CreateParameters(LaunchParamKeys, paramValues, false);
            var arguments = ArgumentsManager.ExpandArguments(player.Arguments, parameters, TvPlayer.ParameterOpenBrace, TvPlayer.ParameterCloseBrace, StringComparison.CurrentCultureIgnoreCase);
            var launchArguments = ArgumentsManager.JoinArguments(arguments);

            if (throughShortcut)
            {
                LaunchShortcut(player, service, launchArguments);
            }
            else
            {
                LaunchProcess(player, launchArguments);
            } // if-else
        } // Launch

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void LaunchShortcut(TvPlayer player, UiBroadcastService service, string arguments)
        {
            var shortcutPath = Path.Combine(AppUiConfiguration.Current.Folders.Cache, service.FullServiceName) + ".lnk";

            // delete exising shortcut
            if (File.Exists(shortcutPath))
            {
                File.SetAttributes(shortcutPath, FileAttributes.Normal);
                File.Delete(shortcutPath);
            } // if

            var shortcut = new ShellLink.ShellLink();
            shortcut.TargetPath = player.Path;
            shortcut.Arguments = arguments;
            shortcut.Description = string.Format(Properties.Texts.ExternalPlayerShortcutDescription, player.Name, service.DisplayName);
            shortcut.IconLocation = service.Logo.GetLogoIconPath();
            shortcutPath = shortcut.CreateShortcut(shortcutPath);

            var start = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = shortcutPath,
                ErrorDialog = true,
            };
            using (var process = Process.Start(start))
            {
                // no op
            } // using process
        } // LaunchShortcut

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void LaunchProcess(TvPlayer player, string arguments)
        {
            var start = new ProcessStartInfo()
            {
                UseShellExecute = false,
                ErrorDialog = true,
                FileName = player.Path,
                Arguments = arguments,
            };
            using (var process = Process.Start(start))
            {
                // no op
            } // using process
        } // LaunchProcess
    } // ExternalPlayer
} // namespace
