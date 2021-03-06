﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Advent_of_Code_2020.StatsBuilder {
	public class StarTime {
		public DateTime dateTime {
			get {
				return UnixTimeStampToDateTime(double.Parse(get_star_ts));
			}
		}

		public string get_star_ts { get; set; }

		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
			var timeUtc = DateTime.UtcNow;

			// Unix timestamp is seconds past epoch
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
			dtDateTime = TimeZoneInfo.ConvertTimeFromUtc(dtDateTime, easternZone);
			return dtDateTime;
		}
	}
}
