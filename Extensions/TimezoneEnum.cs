
using System;
using System.ComponentModel;

namespace Extenders //namespace the enums will live in
{
	public enum TimezoneEnum
	{
		[Description("Dateline Standard Time")]
		DatelineStandardTime,
		[Description("UTC-11")]
		UTC11,
		[Description("Aleutian Standard Time")]
		AleutianStandardTime,
		[Description("Hawaiian Standard Time")]
		HawaiianStandardTime,
		[Description("Marquesas Standard Time")]
		MarquesasStandardTime,
		[Description("Alaskan Standard Time")]
		AlaskanStandardTime,
		[Description("UTC-09")]
		UTC09,
		[Description("Pacific Standard Time (Mexico)")]
		PacificStandardTimeMexico,
		[Description("UTC-08")]
		UTC08,
		[Description("Pacific Standard Time")]
		PacificStandardTime,
		[Description("US Mountain Standard Time")]
		USMountainStandardTime,
		[Description("Mountain Standard Time (Mexico)")]
		MountainStandardTimeMexico,
		[Description("Mountain Standard Time")]
		MountainStandardTime,
		[Description("Central America Standard Time")]
		CentralAmericaStandardTime,
		[Description("Central Standard Time")]
		CentralStandardTime,
		[Description("Easter Island Standard Time")]
		EasterIslandStandardTime,
		[Description("Central Standard Time (Mexico)")]
		CentralStandardTimeMexico,
		[Description("Canada Central Standard Time")]
		CanadaCentralStandardTime,
		[Description("SA Pacific Standard Time")]
		SAPacificStandardTime,
		[Description("Eastern Standard Time (Mexico)")]
		EasternStandardTimeMexico,
		[Description("Eastern Standard Time")]
		EasternStandardTime,
		[Description("Haiti Standard Time")]
		HaitiStandardTime,
		[Description("Cuba Standard Time")]
		CubaStandardTime,
		[Description("US Eastern Standard Time")]
		USEasternStandardTime,
		[Description("Paraguay Standard Time")]
		ParaguayStandardTime,
		[Description("Atlantic Standard Time")]
		AtlanticStandardTime,
		[Description("Venezuela Standard Time")]
		VenezuelaStandardTime,
		[Description("Central Brazilian Standard Time")]
		CentralBrazilianStandardTime,
		[Description("SA Western Standard Time")]
		SAWesternStandardTime,
		[Description("Pacific SA Standard Time")]
		PacificSAStandardTime,
		[Description("Turks And Caicos Standard Time")]
		TurksandCaicosStandardTime,
		[Description("Newfoundland Standard Time")]
		NewfoundlandStandardTime,
		[Description("Tocantins Standard Time")]
		TocantinsStandardTime,
		[Description("E. South America Standard Time")]
		ESouthAmericaStandardTime,
		[Description("SA Eastern Standard Time")]
		SAEasternStandardTime,
		[Description("Argentina Standard Time")]
		ArgentinaStandardTime,
		[Description("Greenland Standard Time")]
		GreenlandStandardTime,
		[Description("Montevideo Standard Time")]
		MontevideoStandardTime,
		[Description("Saint Pierre Standard Time")]
		SaintPierreStandardTime,
		[Description("Bahia Standard Time")]
		BahiaStandardTime,
		[Description("UTC-02")]
		UTC02,
		[Description("Mid-Atlantic Standard Time")]
		MidAtlanticStandardTime,
		[Description("Azores Standard Time")]
		AzoresStandardTime,
		[Description("Cape Verde Standard Time")]
		CaboVerdeStandardTime,
		[Description("UTC")]
		CoordinatedUniversalTime,
		[Description("Morocco Standard Time")]
		MoroccoStandardTime,
		[Description("GMT Standard Time")]
		GMTStandardTime,
		[Description("Greenwich Standard Time")]
		GreenwichStandardTime,
		[Description("W. Europe Standard Time")]
		WEuropeStandardTime,
		[Description("Central Europe Standard Time")]
		CentralEuropeStandardTime,
		[Description("Romance Standard Time")]
		RomanceStandardTime,
		[Description("Central European Standard Time")]
		CentralEuropeanStandardTime,
		[Description("W. Central Africa Standard Time")]
		WCentralAfricaStandardTime,
		[Description("Namibia Standard Time")]
		NamibiaStandardTime,
		[Description("Jordan Standard Time")]
		JordanStandardTime,
		[Description("GTB Standard Time")]
		GTBStandardTime,
		[Description("Middle East Standard Time")]
		MiddleEastStandardTime,
		[Description("Egypt Standard Time")]
		EgyptStandardTime,
		[Description("E. Europe Standard Time")]
		EEuropeStandardTime,
		[Description("Syria Standard Time")]
		SyriaStandardTime,
		[Description("West Bank Standard Time")]
		WestBankGazaStandardTime,
		[Description("South Africa Standard Time")]
		SouthAfricaStandardTime,
		[Description("FLE Standard Time")]
		FLEStandardTime,
		[Description("Israel Standard Time")]
		JerusalemStandardTime,
		[Description("Kaliningrad Standard Time")]
		RussiaTZ1StandardTime,
		[Description("Libya Standard Time")]
		LibyaStandardTime,
		[Description("Arabic Standard Time")]
		ArabicStandardTime,
		[Description("Turkey Standard Time")]
		TurkeyStandardTime,
		[Description("Arab Standard Time")]
		ArabStandardTime,
		[Description("Belarus Standard Time")]
		BelarusStandardTime,
		[Description("Russian Standard Time")]
		RussiaTZ2StandardTime,
		[Description("E. Africa Standard Time")]
		EAfricaStandardTime,
		[Description("Iran Standard Time")]
		IranStandardTime,
		[Description("Arabian Standard Time")]
		ArabianStandardTime,
		[Description("Astrakhan Standard Time")]
		AstrakhanStandardTime,
		[Description("Azerbaijan Standard Time")]
		AzerbaijanStandardTime,
		[Description("Russia Time Zone 3")]
		RussiaTZ3StandardTime,
		[Description("Mauritius Standard Time")]
		MauritiusStandardTime,
		[Description("Georgian Standard Time")]
		GeorgianStandardTime,
		[Description("Caucasus Standard Time")]
		CaucasusStandardTime,
		[Description("Afghanistan Standard Time")]
		AfghanistanStandardTime,
		[Description("West Asia Standard Time")]
		WestAsiaStandardTime,
		[Description("Ekaterinburg Standard Time")]
		RussiaTZ4StandardTime,
		[Description("Pakistan Standard Time")]
		PakistanStandardTime,
		[Description("India Standard Time")]
		IndiaStandardTime,
		[Description("Sri Lanka Standard Time")]
		SriLankaStandardTime,
		[Description("Nepal Standard Time")]
		NepalStandardTime,
		[Description("Central Asia Standard Time")]
		CentralAsiaStandardTime,
		[Description("Bangladesh Standard Time")]
		BangladeshStandardTime,
		[Description("Omsk Standard Time")]
		OmskStandardTime,
		[Description("Myanmar Standard Time")]
		MyanmarStandardTime,
		[Description("SE Asia Standard Time")]
		SEAsiaStandardTime,
		[Description("Altai Standard Time")]
		AltaiStandardTime,
		[Description("W. Mongolia Standard Time")]
		WMongoliaStandardTime,
		[Description("North Asia Standard Time")]
		RussiaTZ6StandardTime,
		[Description("N. Central Asia Standard Time")]
		NovosibirskStandardTime,
		[Description("Tomsk Standard Time")]
		TomskStandardTime,
		[Description("China Standard Time")]
		ChinaStandardTime,
		[Description("North Asia East Standard Time")]
		RussiaTZ7StandardTime,
		[Description("Singapore Standard Time")]
		MalayPeninsulaStandardTime,
		[Description("W. Australia Standard Time")]
		WAustraliaStandardTime,
		[Description("Taipei Standard Time")]
		TaipeiStandardTime,
		[Description("Ulaanbaatar Standard Time")]
		UlaanbaatarStandardTime,
		[Description("North Korea Standard Time")]
		NorthKoreaStandardTime,
		[Description("Aus Central W. Standard Time")]
		AusCentralWStandardTime,
		[Description("Transbaikal Standard Time")]
		TransbaikalStandardTime,
		[Description("Tokyo Standard Time")]
		TokyoStandardTime,
		[Description("Korea Standard Time")]
		KoreaStandardTime,
		[Description("Yakutsk Standard Time")]
		RussiaTZ8StandardTime,
		[Description("Cen. Australia Standard Time")]
		CenAustraliaStandardTime,
		[Description("AUS Central Standard Time")]
		AUSCentralStandardTime,
		[Description("E. Australia Standard Time")]
		EAustraliaStandardTime,
		[Description("AUS Eastern Standard Time")]
		AUSEasternStandardTime,
		[Description("West Pacific Standard Time")]
		WestPacificStandardTime,
		[Description("Tasmania Standard Time")]
		TasmaniaStandardTime,
		[Description("Vladivostok Standard Time")]
		RussiaTZ9StandardTime,
		[Description("Lord Howe Standard Time")]
		LordHoweStandardTime,
		[Description("Bougainville Standard Time")]
		BougainvilleStandardTime,
		[Description("Russia Time Zone 10")]
		RussiaTZ10StandardTime,
		[Description("Magadan Standard Time")]
		MagadanStandardTime,
		[Description("Norfolk Standard Time")]
		NorfolkStandardTime,
		[Description("Sakhalin Standard Time")]
		SakhalinStandardTime,
		[Description("Central Pacific Standard Time")]
		CentralPacificStandardTime,
		[Description("Russia Time Zone 11")]
		RussiaTZ11StandardTime,
		[Description("New Zealand Standard Time")]
		NewZealandStandardTime,
		[Description("UTC+12")]
		UTC12,
		[Description("Fiji Standard Time")]
		FijiStandardTime,
		[Description("Kamchatka Standard Time")]
		KamchatkaStandardTime,
		[Description("Chatham Islands Standard Time")]
		ChathamIslandsStandardTime,
		[Description("Tonga Standard Time")]
		TongaStandardTime,
		[Description("Samoa Standard Time")]
		SamoaStandardTime,
		[Description("Line Islands Standard Time")]
		LineIslandsStandardTime,
	}
}
