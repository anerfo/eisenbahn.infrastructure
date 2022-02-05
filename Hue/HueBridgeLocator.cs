﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hue
{
    /// <summary>
    /// Locates the Philips Hue lights bridge using SSDP 
    /// </summary>
    public static class HueBridgeLocator
    {
        public static HueBridge Locate()
        {
            //https://www.meethue.com/api/nupnp
            //return LocateAsync();
            var result = new HueBridge("192.168.2.120");
            try
            {
                result.InitializeRouter();
            }
            catch(Exception)
            { }
            return result;
        }

        public static HueBridge LocateAsync()
        {
            if (UPnP.NAT.Discover())
            {
                var endpoints = UPnP.NAT.DiscoveredEndpoints
                    .Where(s => s.EndsWith("/description.xml")).ToList();
                foreach (var endpoint in endpoints)
                {
                    if (IsHue(endpoint))
                    {
                        var ip = endpoint.Replace("http://", "").Replace("/description.xml", "");
                        return new HueBridge(ip);
                    }
                }
                return null;
            }
            return null;
        }

        // http://www.nerdblog.com/2012/10/a-day-with-philips-hue.html - description.xml retrieval
        private static bool IsHue(string discoveryUrl)
        {
            var http = new HttpClient {Timeout = TimeSpan.FromMilliseconds(2000)};
            try {
                var res = http.GetStringAsync(discoveryUrl);
                res.Wait();
                var str = res.Result;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (str.Contains("Philips hue bridge"))
                        return true;
                }
            } catch
            {
                return false;
            }
            return false;
        }
    }
}
